using System;

namespace Game.Plugins.ZUtils.Routines
{
    public class RoutineInstruction
    {
        public bool IsDone { get; protected set; }

        protected readonly Action callback;

        public RoutineInstruction()
        {
            IsDone = false;
        }
        
        public RoutineInstruction(Action value)
        {
            callback = value;
            IsDone = true;
        }

        public virtual void Invoke()
        {
            callback?.Invoke();
        }

        public virtual void MoveNext()
        {
            
        }

        public virtual void Reset()
        {
            IsDone = true;
        }
    }
}
