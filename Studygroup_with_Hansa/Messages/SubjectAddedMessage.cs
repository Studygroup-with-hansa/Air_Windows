using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Models;

namespace Studygroup_with_Hansa.Messages
{
    internal class SubjectAddedMessage : MessageBase
    {
        public SubjectModel Subject;

        public SubjectAddedMessage(SubjectModel subject)
        {
            Subject = subject;
        }
    }
}