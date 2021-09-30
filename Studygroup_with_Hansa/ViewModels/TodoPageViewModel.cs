using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public RelayCommand<object> AddTodoCommand { get; private set; }

        public RelayCommand<List<object>> DelTodoCommand { get; private set; }

        public TodoPageViewModel()
        {
            ViewModelLocator locator = (ViewModelLocator)Application.Current.Resources["Locator"];

            TodoList = new ObservableCollection<TodoModel>();
            locator.HomePage.Subjects.ToList().ForEach(e =>
            {
                TodoList.Add(new TodoModel(e.Name, null));
            });

            YesterdayCommand = new RelayCommand(ExecuteYesterdayCommand);
            TomorrowCommand = new RelayCommand(ExecuteTomorrowCommand);
            AddTodoCommand = new RelayCommand<object>(ExecuteAddTodoCommand);
            DelTodoCommand = new RelayCommand<List<object>>(ExecuteDelTodoCommand);

            Messenger.Default.Register<SubjectAddedMessage>(this, m =>
            {
                TodoList.Add(new TodoModel(m.Subject.Name, null));
            });

            Messenger.Default.Register<SubjectEditedMessage>(this, m =>
            {
                for(int i = 0; i < TodoList.Count(); i++)
                {
                    if(TodoList[i].Name == m.OldName)
                        TodoList[i].Name = m.Subject.Name;
                }
            });

            Messenger.Default.Register<SubjectDeletedMessage>(this, m =>
            {
                for (int i = 0; i < TodoList.Count(); i++)
                {
                    if (TodoList[i].Name == m.Subject.Name)
                        TodoList.Remove(TodoList[i]);
                }
            });
        }

        private void ExecuteYesterdayCommand()
        {
            SelectedDate = SelectedDate.AddDays(-1);
        }

        private void ExecuteTomorrowCommand()
        {
            SelectedDate = SelectedDate.AddDays(1);
        }

        private void ExecuteAddTodoCommand(object obj)
        {
            TodoModel todo = obj as TodoModel;

            if (!string.IsNullOrWhiteSpace(todo.InputTodo))
                todo.Todos.Add(new TodoItem(todo.InputTodo.Trim()));

            todo.InputTodo = string.Empty;
        }

        private void ExecuteDelTodoCommand(List<object> objs)
        {
            TodoModel todo = objs[0] as TodoModel;
            TodoItem todoItem = objs[1] as TodoItem;
            todo.Todos.Remove(todoItem);
        }
    }
}
