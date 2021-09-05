using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studygroup_with_Hansa.Messages
{
    class GoalChangedMessage : ValueChangedMessage<int>
    {
        public GoalChangedMessage(int goal) : base(goal) { }
    }
}
