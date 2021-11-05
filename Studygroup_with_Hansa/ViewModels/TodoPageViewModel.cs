using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using RestSharp;
using Studygroup_with_Hansa.Messages;
using Studygroup_with_Hansa.Models;
using Studygroup_with_Hansa.Services;

namespace Studygroup_with_Hansa.ViewModels
{
    public class TodoPageViewModel : ViewModelBase
    {
        private DateTime _selectedDate = DateTime.Now;

        private string _inputMemo;

        private ObservableCollection<TodoSubject> _subjects;

        public TodoPageViewModel()
        {
            SetTodos();

            YesterdayCommand = new RelayCommand(ExecuteYesterdayCommand);
            TomorrowCommand = new RelayCommand(ExecuteTomorrowCommand);
            AddTodoCommand = new RelayCommand<object>(ExecuteAddTodoCommand);
            DelTodoCommand = new RelayCommand<List<object>>(ExecuteDelTodoCommand);

            Messenger.Default.Register<SubjectAddedMessage>(this,
                m => { Subjects.Add(new TodoSubject {Title = m.Subject.Title}); });
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _ = Set(ref _selectedDate, value);
                SetTodos();
            }
        }

        public string InputMemo
        {
            get => _inputMemo;
            set => Set(ref _inputMemo, value);
        }

        public ObservableCollection<TodoSubject> Subjects
        {
            get => _subjects;
            set => Set(ref _subjects, value);
        }

        public RelayCommand YesterdayCommand { get; }

        public RelayCommand TomorrowCommand { get; }

        public RelayCommand<object> AddTodoCommand { get; }

        public RelayCommand<List<object>> DelTodoCommand { get; }

        private async void SetTodos()
        {
            var result = (await RestManager.RestRequest<TodoModel>("/v1/user/data/subject/checklist/", Method.POST,
                new List<ParamModel> {new ParamModel("date", SelectedDate.ToString("yyyy-MM-dd"))})).Data;
            InputMemo = result.InputMemo;
            Subjects = new ObservableCollection<TodoSubject>(result.Subjects);
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
            var todo = obj as TodoSubject;
            var addTodo = todo?.Todos.ToList();

            if (todo != null && !string.IsNullOrWhiteSpace(todo.InputTodo))
            {
                addTodo.Insert(0, new TodoItem {Todo = todo.InputTodo.Trim()});
                todo.Todos = addTodo;
            }

            if (todo != null) todo.InputTodo = string.Empty;
        }

        private void ExecuteDelTodoCommand(List<object> objs)
        {
            var todo = objs[0] as TodoSubject;
            var delTodo = todo?.Todos.ToList();
            var todoItem = objs[1] as TodoItem;

            _ = delTodo?.Remove(todoItem);
            if (todo != null) todo.Todos = delTodo;
        }
    }
}