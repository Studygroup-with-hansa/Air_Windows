using GalaSoft.MvvmLight.Messaging;

namespace Studygroup_with_Hansa.Messages
{
    internal class IsBlurChangedMessage : MessageBase
    {
        public IsBlurChangedMessage(bool isBlur)
        {
            IsBlur = isBlur;
        }

        public bool IsBlur { get; }
    }
}