using GalaSoft.MvvmLight.Messaging;
using Studygroup_with_Hansa.Models;

namespace Studygroup_with_Hansa.Messages
{
    internal class SubjectDeletedMessage : MessageBase
    {
        public SubjectModel Subject;

        public SubjectDeletedMessage(SubjectModel subject)
        {
            Subject = subject;
        }
    }
}