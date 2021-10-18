using System.Collections.Generic;
using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class TodoListModel
    {
        [DeserializeAs(Name = "dateList")] public List<Day> DateList { get; set; }

        public class TodoList
        {
            [DeserializeAs(Name = "isitDone")] public bool IsitDone { get; set; }
            [DeserializeAs(Name = "todo")] public string Todo { get; set; }
        }

        public class Subject
        {
            [DeserializeAs(Name = "subjects")] public string Name { get; set; }
            [DeserializeAs(Name = "todoList")] public List<TodoList> TodoList { get; set; }
        }

        public class Day
        {
            [DeserializeAs(Name = "date")] public string Date { get; set; }
            [DeserializeAs(Name = "memo")] public string Memo { get; set; }
            [DeserializeAs(Name = "subjects")] public List<Subject> Subjects { get; set; }
        }
    }
}