using GalaSoft.MvvmLight;
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

        public string Name { get; set; }

        private double _percentage;
        public double Percentage
        {
            get
            {
                _percentage = 0;
                Todos.ToList().ForEach(e => _percentage += Convert.ToInt32(e.IsOver));
                _percentage = (double)(_percentage / Todos.Count) * 100;
                return _percentage;
            }
        }

        public ObservableCollection<TodoItem> Todos { get; set; }

        public TodoModel(string name, List<TodoItem> todos)
        {
            Name = name;

            Todos = new ObservableCollection<TodoItem>();
            Todos.CollectionChanged += TodoCollectionChanged;

            todos.ForEach(e => Todos.Add(e));
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
        }

        public void TodoItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOver") RaisePropertyChanged("Percentage");
        }
    }
}
