using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using Studygroup_with_Hansa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studygroup_with_Hansa.Messages
{
    class SubjectEditedMessage : ValueChangedMessage<SubjectModel>
    {
        public SubjectEditedMessage(SubjectModel subject) : base(subject) { }
    }
}
