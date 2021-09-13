using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studygroup_with_Hansa.Messages
{
    sealed class IsBlurChangedMessage : MessageBase
    {
        public bool IsBlur { get; set; }

        public IsBlurChangedMessage(bool isBlur)
        {
            IsBlur = isBlur;
        }
    }
}
