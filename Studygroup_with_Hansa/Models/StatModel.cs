using System.Collections.Generic;
using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class Subject
    {
        [DeserializeAs(Name = "title")] public string Title { get; set; }

        [DeserializeAs(Name = "time")] public int Time { get; set; }

        [DeserializeAs(Name = "color")] public string Color { get; set; }
    }

    public class Stat
    {
        [DeserializeAs(Name = "date")] public string Date { get; set; }

        [DeserializeAs(Name = "totalStudyTime")]
        public int TotalStudyTime { get; set; }

        [DeserializeAs(Name = "subject")] public List<Subject> Subject { get; set; }

        [DeserializeAs(Name = "goal")] public int Goal { get; set; }
    }

    public class StatData
    {
        [DeserializeAs(Name = "totalTime")] public int TotalTime { get; set; }

        [DeserializeAs(Name = "goals")] public int Goals { get; set; }

        [DeserializeAs(Name = "stats")] public List<Stat> Stats { get; set; }
    }

    public class StatModel
    {
        [DeserializeAs(Name = "status")] public int Status { get; set; }

        [DeserializeAs(Name = "detail")] public string Detail { get; set; }

        [DeserializeAs(Name = "data")] public StatData Data { get; set; }
    }
}