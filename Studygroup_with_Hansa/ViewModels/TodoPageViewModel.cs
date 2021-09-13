using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Studygroup_with_Hansa.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace Studygroup_with_Hansa.ViewModels
{
    public class TodoPageViewModel : ViewModelBase
    {
        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { Set(ref _selectedDate, value); }
        }

        private string _inputMemo;
        public string InputMemo
        {
            get { return _inputMemo; }
            set { Set(ref _inputMemo, value); }
        }

        public ObservableCollection<TodoModel> TodoList { get; set; }

        public RelayCommand YesterdayCommand { get; private set; }

        public RelayCommand TomorrowCommand { get; private set; }

        public TodoPageViewModel()
        {
            TodoList = new ObservableCollection<TodoModel>();

            var todos = new List<TodoItem>();
            todos.Add(new TodoItem("할 일"));
            todos.Add(new TodoItem("할 일"));
            todos.Add(new TodoItem("할 일"));

            TodoList.Add(new TodoModel("과목", todos));

            YesterdayCommand = new RelayCommand(ExecuteYesterdayCommand);
            TomorrowCommand = new RelayCommand(ExecuteTomorrowCommand);
        }

        private void ExecuteYesterdayCommand()
        {
            SelectedDate = SelectedDate.AddDays(-1);
        }

        private void ExecuteTomorrowCommand()
        {
            SelectedDate = SelectedDate.AddDays(1);
        }
    }
}
