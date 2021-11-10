using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class AddTodoModel
    {
        [DeserializeAs(Name = "pk")] public int Key { get; set; }
    }
}