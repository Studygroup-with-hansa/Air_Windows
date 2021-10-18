using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class LoginData
    {
        [DeserializeAs(Name = "isEmailExist")] public bool IsEmailExist { get; set; }

        [DeserializeAs(Name = "emailSent")] public bool EmailSent { get; set; }
    }

    public class LoginModel
    {
        [DeserializeAs(Name = "data")] public LoginData Data { get; set; }

        [DeserializeAs(Name = "detail")] public string Detail { get; set; }

        [DeserializeAs(Name = "status")] public int Status { get; set; }
    }
}