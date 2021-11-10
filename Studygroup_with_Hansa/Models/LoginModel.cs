using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class LoginModel
    {
        [DeserializeAs(Name = "token")] public string Token { get; set; }
    }
}