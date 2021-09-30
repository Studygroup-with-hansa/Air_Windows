using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studygroup_with_Hansa.ViewModels
{
    public class SetProfileViewModel : ViewModelBase
    {
        private bool? _result = false;
        public bool? Result
        {
            get { return _result; }
            set { Set(ref _result, value); }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { Set(ref _filePath, value); }
        }

        public string InputNick { get; set; }

        public RelayCommand SelectProfileCommand { get; private set; }

        public SetProfileViewModel()
        {
            SelectProfileCommand = new RelayCommand(ExecuteSelectProfileCommand);
        }

        private void ExecuteSelectProfileCommand()
        {
            OpenFileDialog selectImageDialog = new OpenFileDialog
            {
                Title = "이미지를 선택해주세요",
                Filter = "이미지 파일|*.jpg;*.jpeg;*.png"
            };

            if (selectImageDialog.ShowDialog() == true)
            {
                Result = true;
                FilePath = selectImageDialog.FileName;
            }
        }
    }
}
