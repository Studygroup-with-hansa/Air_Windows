using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studygroup_with_Hansa.Messages
{
    class GoalChangedMessage : MessageBase
    {
        public int Goal { get; set; }

        public GoalChangedMessage(int goal)
        {
            Goal = goal;
        }
    }
}
