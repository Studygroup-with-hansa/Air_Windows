using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class UserModel
    {
        [DeserializeAs(Name = "name")] public string Name { get; set; }

        [DeserializeAs(Name = "email")] public string Email { get; set; }
    }
}