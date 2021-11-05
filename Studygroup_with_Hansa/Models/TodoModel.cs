using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using GalaSoft.MvvmLight;
using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class TodoItem : ObservableObject
    {
        private bool _isOver;

        [DeserializeAs(Name = "isitDone")]
        public bool IsOver
        {
            get => _isOver;
            set => Set(ref _isOver, value);
        }

        [DeserializeAs(Name = "todo")] public string Todo { get; set; }
    }

    public class TodoSubject : ObservableObject
    {
        private string _inputTodo;

        private bool _isOpen;

        private string _title;

        private List<TodoItem> _todos;

        public bool IsOpen
        {
            get => _isOpen;
            set => Set(ref _isOpen, value);
        }

        [DeserializeAs(Name = "subject")]
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public double Percentage
        {
            get
            {
                if (Todos.Count <= 0) return 0;
                var count = 0;
                Todos.ToList().ForEach(e => count += Convert.ToInt32(e.IsOver));
                return (double) count / Todos.Count;
            }
        }

        public string InputTodo
        {
            get => _inputTodo;
            set => Set(ref _inputTodo, value);
        }

        [DeserializeAs(Name = "todoList")]
        public List<TodoItem> Todos
        {
            get => _todos;
            set => Set(ref _todos, value);
        }
    }

    public class TodoModel : ObservableObject
    {
        [DeserializeAs(Name = "date")] public string Date { get; set; }

        [DeserializeAs(Name = "memo")] public string InputMemo { get; set; }

        [DeserializeAs(Name = "subjects")] public List<TodoSubject> Subjects { get; set; }
    }
}