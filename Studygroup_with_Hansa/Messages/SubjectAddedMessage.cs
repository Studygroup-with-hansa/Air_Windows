using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Models;

namespace Studygroup_with_Hansa.Messages
{
    internal class SubjectAddedMessage : MessageBase
    {
        public SubjectAddedMessage(Subject subject)
        {
            Subject = subject;
        }

        public Subject Subject { get; }
    }
}