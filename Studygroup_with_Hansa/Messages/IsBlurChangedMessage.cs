using GalaSoft.MvvmLight.Messaging;

namespace Studygroup_with_Hansa.Messages
{
    internal sealed class IsBlurChangedMessage : MessageBase
    {
        public bool IsBlur;

        public IsBlurChangedMessage(bool isBlur)
        {
            IsBlur = isBlur;
        }
    }
}