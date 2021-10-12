using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using GalaSoft.MvvmLight;

namespace Studygroup_with_Hansa.Models
{
    public class TodoItem : ObservableObject
    {
        private bool _isOver;

        public TodoItem(string todo)
        {
            Todo = todo;
        }

        public bool IsOver
        {
            get => _isOver;
            set => Set(ref _isOver, value);
        }

        public string Todo { get; set; }
    }

    public class TodoModel : ObservableObject
    {
        private string _inputTodo;

        private bool _isOpen;

        private string _name;

        public TodoModel(string name, List<TodoItem> todos)
        {
            Name = name;

            Todos = new ObservableCollection<TodoItem>();
            Todos.CollectionChanged += TodoCollectionChanged;

            todos?.ForEach(e => Todos.Add(e));
        }

        public bool IsOpen
        {
            get => _isOpen;
            set => Set(ref _isOpen, value);
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
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

        public ObservableCollection<TodoItem> Todos { get; set; }

        public void TodoCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Remove)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                    foreach (TodoItem item in e.NewItems)
                        //Added items
                        item.PropertyChanged += TodoItemPropertyChanged;
            }
            else
                foreach (TodoItem item in e.OldItems)
                    //Removed items
                    item.PropertyChanged -= TodoItemPropertyChanged;

            RaisePropertyChanged("Percentage");
        }

        public void TodoItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOver") RaisePropertyChanged("Percentage");
        }
    }
}