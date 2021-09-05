using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studygroup_with_Hansa.Messages
{
    sealed class IsBlurChangedMessage : ValueChangedMessage<bool>
    {
        public IsBlurChangedMessage(bool IsBlur) : base(IsBlur) { }
    }
}
