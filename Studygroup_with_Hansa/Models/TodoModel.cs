using GalaSoft.MvvmLight;
using Studygroup_with_Hansa.Models.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studygroup_with_Hansa.Models
{
    public class TodoItem : ObservableObject
    {
        private bool _isOver;
        public bool IsOver
        {
            get { return _isOver; }
            set { Set(ref _isOver, value); }
        }

        public string Todo { get; set; }

        public TodoItem(string todo)
        {
            Todo = todo;
        }
    }

    public class TodoModel : ObservableObject
    {
        private bool _isOpen = false;
        public bool IsOpen
        {
            get { return _isOpen; }
            set { Set(ref _isOpen, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public double Percentage
        {
            get
            {
                if (Todos.Count > 0)
                {
                    int count = 0;
                    Todos.ToList().ForEach(e => count += Convert.ToInt32(e.IsOver));
                    return (double)count / Todos.Count;
                }

                return 0;
            }
        }

        private string _inputTodo;
        public string InputTodo
        {
            get { return _inputTodo; }
            set { Set(ref _inputTodo, value); }
        }

        public ObservableCollection<TodoItem> Todos { get; set; }

        public TodoModel(string name, List<TodoItem> todos)
        {
            Name = name;

            Todos = new ObservableCollection<TodoItem>();
            Todos.CollectionChanged += TodoCollectionChanged;

            if (todos != null) todos.ForEach(e => Todos.Add(e));
        }

        public void TodoCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (TodoItem item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= TodoItemPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (TodoItem item in e.NewItems)
                {
                    //Added items
                    item.PropertyChanged += TodoItemPropertyChanged;
                }
            }

            RaisePropertyChanged("Percentage");
        }

        public void TodoItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOver") RaisePropertyChanged("Percentage");
        }
    }
}
