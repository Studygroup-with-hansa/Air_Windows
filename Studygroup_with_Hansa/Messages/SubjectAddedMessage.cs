using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studygroup_with_Hansa.Messages
{
    class SubjectAddedMessage : MessageBase
    {
        public SubjectModel Subject { get; set; }

        public SubjectAddedMessage(SubjectModel subject)
        {
            Subject = subject;
        }
    }
}
