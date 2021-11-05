using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class UserList
    {
        [DeserializeAs(Name = "name")] public string Name { get; set; }

        [DeserializeAs(Name = "email")] public string Email { get; set; }

        [DeserializeAs(Name = "profileImg")] public string ProfileImg { get; set; }

        [DeserializeAs(Name = "todayStudyTime")] public string TodayStudyTime { get; set; }

        public string TodayStudyTimeString
        {
            get
            {
                var t = TimeSpan.FromSeconds(int.Parse(TodayStudyTime));
                return string.Format($"{t:hh}H {t:mm}M {t:ss}S");
            }
        }

        [DeserializeAs(Name = "isItOwner")] public bool IsItOwner { get; set; }

        [DeserializeAs(Name = "rank")] public int Rank { get; set; }
    }

    public class GroupModel
    {
        [DeserializeAs(Name = "code")] public string Code { get; set; }

        public string Title { get; set; }

        [DeserializeAs(Name = "userList")] public List<UserList> UserList { get; set; }

        public int UserCount => UserList.Count;
    }
}