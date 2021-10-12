using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Models;

namespace Studygroup_with_Hansa.Messages
{
    internal class SubjectEditedMessage : MessageBase
    {
        public string OldName;

        public SubjectModel Subject;

        public SubjectEditedMessage(string name, SubjectModel subject)
        {
            OldName = name;
            Subject = subject;
        }
    }
}