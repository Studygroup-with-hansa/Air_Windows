using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models;

namespace Studygroup_with_Hansa.ViewModels
{
    public class TodoPageViewModel : ViewModelBase
    {
        private string _inputMemo;
        private DateTime _selectedDate = DateTime.Now;

        public TodoPageViewModel()
        {
            var locator = (ViewModelLocator) Application.Current.Resources["Locator"];

            TodoList = new ObservableCollection<TodoModel>();
            locator.HomePage.Subjects.ToList().ForEach(e => { TodoList.Add(new TodoModel(e.Name, null)); });

            YesterdayCommand = new RelayCommand(ExecuteYesterdayCommand);
            TomorrowCommand = new RelayCommand(ExecuteTomorrowCommand);
            AddTodoCommand = new RelayCommand<object>(ExecuteAddTodoCommand);
            DelTodoCommand = new RelayCommand<List<object>>(ExecuteDelTodoCommand);

            Messenger.Default.Register<SubjectAddedMessage>(this,
                m => { TodoList.Add(new TodoModel(m.Subject.Name, null)); });

            Messenger.Default.Register<SubjectEditedMessage>(this, m =>
            {
                for (var i = 0; i < TodoList.Count(); i++)
                    if (TodoList[i].Name == m.OldName)
                        TodoList[i].Name = m.Subject.Name;
            });

            Messenger.Default.Register<SubjectDeletedMessage>(this, m =>
            {
                for (var i = 0; i < TodoList.Count(); i++)
                    if (TodoList[i].Name == m.Subject.Name)
                        _ = TodoList.Remove(TodoList[i]);
            });
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => Set(ref _selectedDate, value);
        }

        public string InputMemo
        {
            get => _inputMemo;
            set => Set(ref _inputMemo, value);
        }

        public ObservableCollection<TodoModel> TodoList { get; set; }

        public RelayCommand YesterdayCommand { get; }

        public RelayCommand TomorrowCommand { get; }

        public RelayCommand<object> AddTodoCommand { get; }

        public RelayCommand<List<object>> DelTodoCommand { get; }

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
            var todo = obj as TodoModel;

            if (todo != null && !string.IsNullOrWhiteSpace(todo.InputTodo))
                todo.Todos.Insert(0, new TodoItem(todo.InputTodo.Trim()));

            if (todo != null) todo.InputTodo = string.Empty;
        }

        private void ExecuteDelTodoCommand(List<object> objs)
        {
            var todo = objs[0] as TodoModel;
            var todoItem = objs[1] as TodoItem;
            _ = todo?.Todos.Remove(todoItem);
        }
    }
}