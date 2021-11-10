using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

        private ObservableCollection<TodoSubject> _subjects;

        private string _inputMemo;

        public TodoPageViewModel()
        {
            SetTodos();

            YesterdayCommand = new RelayCommand(ExecuteYesterdayCommand);
            TomorrowCommand = new RelayCommand(ExecuteTomorrowCommand);
            AddTodoCommand = new RelayCommand<object>(ExecuteAddTodoCommand);
            DelTodoCommand = new RelayCommand<List<object>>(ExecuteDelTodoCommand);
            ToggleTodoCommand = new RelayCommand<object>(ExecuteToggleTodoCommand);

            Messenger.Default.Register<SubjectAddedMessage>(this,
                m => { Subjects.Add(new TodoSubject {Title = m.Subject.Title}); });
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                var old = _selectedDate;
                _ = Set(ref _selectedDate, value);

                if(!old.ToString("MM/dd/yyyy").Equals(_selectedDate.ToString("MM/dd/yyyy")))
                    SaveMemo(old.ToString("yyyy-MM-dd"));
                SetTodos();
            }
        }

        public ObservableCollection<TodoSubject> Subjects
        {
            get => _subjects;
            set => Set(ref _subjects, value);
        }

        public string InputMemo
        {
            get => _inputMemo;
            set => Set(ref _inputMemo, value);
        }

        public RelayCommand YesterdayCommand { get; }

        public RelayCommand TomorrowCommand { get; }

        public RelayCommand<object> AddTodoCommand { get; }

        public RelayCommand<List<object>> DelTodoCommand { get; }

        public RelayCommand<object> ToggleTodoCommand { get; }

        private async void SetTodos()
        {
            var result = await RestManager.RestRequest<TodoModel>("/v1/user/data/subject/checklist/", Method.GET,
                new List<ParamModel> {new ParamModel("date", SelectedDate.ToString("yyyy-MM-dd"))});
            InputMemo = result.Data.InputMemo;
            Subjects = new ObservableCollection<TodoSubject>(result.Data.Subjects);
        }

        private async Task<int> AddTodo(TodoSubject subject)
        {
            var requestParams = new List<ParamModel>
            {
                new ParamModel("subject", subject.Title),
                new ParamModel("date", SelectedDate.ToString("yyyy-MM-dd")),
                new ParamModel("todo", subject.InputTodo)
            };

            var result = await RestManager.RestRequest<AddTodoModel>("/v1/user/data/subject/checklist/", Method.POST, requestParams);
            return result.Data.Key;
        }

        private async void DelTodo(int pk)
        {
            _ = await RestManager.RestRequest<string>("/v1/user/data/subject/checklist/", Method.DELETE,
                new List<ParamModel> { new ParamModel("pk", pk.ToString()) });
        }

        private async void ToggleTodo(int pk)
        {
            _ = await RestManager.RestRequest<string>("/v1/user/data/subject/checklist/status/", Method.PUT,
                new List<ParamModel> {new ParamModel("pk", pk.ToString())});
        }

        public async void SaveMemo(string date)
        {
            var requestParams = new List<ParamModel>
            {
                new ParamModel("date", date),
                new ParamModel("memo", InputMemo)
            };

            _ = await RestManager.RestRequest<string>("/v1/user/data/subject/checklist/memo/", Method.POST, requestParams);
        }

        private void ExecuteYesterdayCommand()
        {
            SelectedDate = SelectedDate.AddDays(-1);
        }

        private void ExecuteTomorrowCommand()
        {
            SelectedDate = SelectedDate.AddDays(1);
        }

        private async void ExecuteAddTodoCommand(object obj)
        {
            var subject = obj as TodoSubject;
            var addTodo = subject?.Todos.ToList();
            var key = await AddTodo(subject);

            if (subject != null && !string.IsNullOrWhiteSpace(subject.InputTodo))
            {
                addTodo.Insert(0, new TodoItem {Key = key, Todo = subject.InputTodo.Trim()});
                subject.Todos = addTodo;
            }

            if (subject != null) subject.InputTodo = string.Empty;
        }

        private void ExecuteDelTodoCommand(List<object> objs)
        {
            var subject = objs[0] as TodoSubject;
            var delTodo = subject?.Todos.ToList();

            if (objs[1] is TodoItem todoItem)
            {
                DelTodo(todoItem.Key);
                _ = delTodo?.Remove(todoItem);
            }

            if (subject != null) subject.Todos = delTodo;
        }

        private void ExecuteToggleTodoCommand(object obj)
        {
            if (obj is TodoItem todoItem) ToggleTodo(todoItem.Key);
        }
    }
}