using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class LoginModel
    {
        [DeserializeAs(Name = "isEmailExist")] public bool IsEmailExist { get; set; }

        [DeserializeAs(Name = "emailSent")] public bool EmailSent { get; set; }
    }
}