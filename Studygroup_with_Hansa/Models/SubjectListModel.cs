using System.Collections.Generic;
using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class SubjectListModel
    {
        [DeserializeAs(Name = "subject")] public List<Subject> Subjects { get; set; }
        [DeserializeAs(Name = "goal")] public int Goal { get; set; }

        public class Subject
        {
            [DeserializeAs(Name = "title")] public string Title { get; set; }

            [DeserializeAs(Name = "time")] public int Time { get; set; }

            [DeserializeAs(Name = "color")] public string Color { get; set; }
        }
    }
}