using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RestSharp;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Properties;
using Studygroup_with_Hansa.Services;

namespace Studygroup_with_Hansa.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private static DispatcherTimer timer;

        private bool? _loginState;

        private string _inputCode;

        private string _inputMail;

        private bool _isAccExist;

        private bool? _isEmailSent;

        private TimeSpan _leftTime;

        public LoginViewModel()
        {
            ProveCommand = new RelayCommand(ExecuteProveCommand, CanExecuteProveCommand);
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        public bool? LoginState
        {
            get => _loginState;
            set => Set(ref _loginState, value);
        }

        public bool IsAccExist
        {
            get => _isAccExist;
            set => Set(ref _isAccExist, value);
        }

        public string InputMail
        {
            get => _inputMail;
            set
            {
                _inputMail = value;
                ProveCommand.RaiseCanExecuteChanged();
            }
        }

        public string InputCode
        {
            get => _inputCode;
            set
            {
                _ = Set(ref _inputCode, value.ToUpper());
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public bool? IsEmailSent
        {
            get => _isEmailSent;
            set => Set(ref _isEmailSent, value);
        }

        public TimeSpan LeftTime
        {
            get => _leftTime;
            set => Set(ref _leftTime, value);
        }

        public RelayCommand ProveCommand { get; }

        public RelayCommand LoginCommand { get; }

        private async void ExecuteProveCommand()
        {
            var result = await RestManager.RestRequest<ProveModel>("v1/user/manage/signin/", Method.POST,
                new List<ParamModel> {new ParamModel("email", InputMail)});
            IsEmailSent = result.Data.EmailSent;
            IsAccExist = result.Data.IsEmailExist;

            if (!(bool) IsEmailSent) return;

            LeftTime = new TimeSpan(0, 5, 0);
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            timer.Tick += (sender, args) =>
            {
                if (LeftTime.TotalSeconds > 0)
                {
                    LeftTime = LeftTime.Subtract(new TimeSpan(0, 0, 1));
                }
                else
                {
                    timer.Stop();
                    IsEmailSent = null;
                }
            };
            timer.Start();
        }

        private async void ExecuteLoginCommand()
        {
            var requestParams = new List<ParamModel>
            {
                new ParamModel("auth", InputCode),
                new ParamModel("email", InputMail)
            };

            var result = await RestManager.RestRequest<LoginModel>("v1/user/manage/signin/", Method.PUT, requestParams);

            if (!string.IsNullOrEmpty(result.Data.Token))
            {
                Settings.Default.Token = result.Data.Token;
                Settings.Default.Save();
                LoginState = true;
            }
            else LoginState = false;
        }

        private bool CanExecuteProveCommand()
        {
            if (string.IsNullOrWhiteSpace(InputMail)) return false;

            string regularedMail;
            try
            {
                // Normalize the domain
                regularedMail = Regex.Replace(InputMail, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(regularedMail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private bool CanExecuteLoginCommand()
        {
            return IsEmailSent == true && InputCode?.Length == 8;
        }
    }
}