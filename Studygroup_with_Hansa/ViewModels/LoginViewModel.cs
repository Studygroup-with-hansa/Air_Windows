using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RestSharp;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Services;

namespace Studygroup_with_Hansa.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private static DispatcherTimer timer;

        private string _inputMail;

        private bool? _isEmailSent;

        private TimeSpan _leftTime = new TimeSpan(0, 5, 0);

        public LoginViewModel()
        {
            ProveCommand = new RelayCommand(ExecuteProveCommand, CanExecuteProveCommand);
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

        private void ExecuteProveCommand()
        {
            var requestParams = new List<ParamModel> {new ParamModel("email", InputMail)};

            var result = RestManager.RestRequest<LoginModel>("v1/user/manage/signin/", Method.POST, requestParams);

            IsEmailSent = result != null && result.Result.Data.Data.EmailSent;

            if (!(bool) IsEmailSent) return;
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            timer.Tick += (sender, args) =>
            {
                if (LeftTime.TotalSeconds > 0)
                    LeftTime = LeftTime.Subtract(new TimeSpan(0, 0, 1));
                else
                    timer.Stop();
            };
            timer.Start();
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
    }
}