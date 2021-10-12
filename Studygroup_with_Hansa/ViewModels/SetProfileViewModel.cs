using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

namespace Studygroup_with_Hansa.ViewModels
{
    public class SetProfileViewModel : ViewModelBase
    {
        private string _filePath;
        private bool? _result = false;

        public SetProfileViewModel()
        {
            SelectProfileCommand = new RelayCommand(ExecuteSelectProfileCommand);
        }

        public bool? Result
        {
            get => _result;
            set => Set(ref _result, value);
        }

        public string FilePath
        {
            get => _filePath;
            set => Set(ref _filePath, value);
        }

        public string InputNick { get; set; }

        public RelayCommand SelectProfileCommand { get; }

        private void ExecuteSelectProfileCommand()
        {
            var selectImageDialog = new OpenFileDialog
            {
                Title = "이미지를 선택해주세요",
                Filter = "이미지 파일|*.jpg;*.jpeg;*.png"
            };

            if (selectImageDialog.ShowDialog() != true) return;
            Result = true;
            FilePath = selectImageDialog.FileName;
        }
    }
}