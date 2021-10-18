using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class ResultModel
    {
        [DeserializeAs(Name = "status")] public int Status { get; set; }

        [DeserializeAs(Name = "detail")] public string Detail { get; set; }

        [DeserializeAs(Name = "data")] public object Data { get; set; }
    }

    public class ResultModel<T>
    {
        [DeserializeAs(Name = "status")] public int Status { get; set; }

        [DeserializeAs(Name = "detail")] public string Detail { get; set; }

        [DeserializeAs(Name = "data")] public T Data { get; set; }
    }
}