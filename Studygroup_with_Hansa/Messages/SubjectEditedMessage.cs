using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Models;

namespace Studygroup_with_Hansa.Messages
{
    internal class SubjectEditedMessage : MessageBase
    {
        public SubjectEditedMessage(Subject oldSubject, string color, string title)
        {
            Color = color;
            OldSubject = oldSubject;
            Title = title;
        }

        public string Color { get; }

        public Subject OldSubject { get; }

        public string Title { get; }
    }
}