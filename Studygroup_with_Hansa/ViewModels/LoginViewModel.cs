using System;
using System.Globalization;
using System.Text.RegularExpressions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Studygroup_with_Hansa.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _inputMail;

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

        public RelayCommand ProveCommand { get; }

        private void ExecuteProveCommand()
        {
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