using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class ProveModel
    {
        [DeserializeAs(Name = "isEmailExist")] public bool IsEmailExist { get; set; }

        [DeserializeAs(Name = "emailSent")] public bool EmailSent { get; set; }
    }
}