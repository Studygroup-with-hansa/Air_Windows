using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace Studygroup_with_Hansa.Models
{
    public class Group
    {
        [DeserializeAs(Name = "code")] public string Code { get; set; }

        [DeserializeAs(Name = "userCount")] public int UserCount { get; set; }

        public List<object> UserIcon
        {
            get
            {
                var list = new List<object>();
                for (var i = 0; i < 5 && i < UserCount; i++)
                    list.Add(new object());
                return list;
            }
        }

        public int Excess => UserCount > 5 ? UserCount - 5 : 0;

        public bool IsExcess => UserCount > 5;

        [DeserializeAs(Name = "leader")] public string Leader { get; set; }

        [DeserializeAs(Name = "firstPlace")] public string FirstPlace { get; set; }

        [DeserializeAs(Name = "firstPlaceStudyTime")] public string FirstPlaceStudyTime { get; set; }

        public string FirstPlaceStudyTimeString
        {
            get
            {
                var t = TimeSpan.FromSeconds(int.Parse(FirstPlaceStudyTime));
                return string.Format($"{t:hh}H {t:mm}M {t:ss}S");
            }
        }
    }

    public class GroupListModel
    {
        [DeserializeAs(Name = "groupList")] public List<Group> GroupList { get; set; }
    }
}