using GalaSoft.MvvmLight.Messaging;

namespace Studygroup_with_Hansa.Messages
{
    internal class GoalChangedMessage : MessageBase
    {
        public int Goal;

        public GoalChangedMessage(int goal)
        {
            Goal = goal;
        }
    }
}