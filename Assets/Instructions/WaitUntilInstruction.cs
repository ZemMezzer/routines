using System;

namespace Routines.Instructions
{
    public sealed class WaitUntilInstruction : RoutineInstruction
    {
        private readonly Func<bool> onUpdateCondition;
        private readonly RoutineInstruction waitInstruction;
        
        /// <summary>
        /// Execute as long as predicate returns false
        /// </summary>
        /// <param name="predicate">Returns predicate conditions</param>
        public WaitUntilInstruction(Func<bool> predicate)
        {
            onUpdateCondition = predicate;
            Reset();
        }

        /// <summary>
        /// Execute as long as predicate returns false
        /// </summary>
        /// <param name="predicate">Returns predicate conditions</param>
        /// <param name="updateInstruction">Update loop instruction</param>
        public WaitUntilInstruction(Func<bool> predicate, RoutineInstruction updateInstruction) : this(predicate)
        {
            waitInstruction = updateInstruction;
        }

        public override void MoveNext()
        {
            if (waitInstruction == null)
            {
                IsDone = onUpdateCondition();
                return;
            }
            
            if (waitInstruction.IsDone)
            {
                IsDone = onUpdateCondition();
                waitInstruction.Reset();
            }
            else
            {
                waitInstruction.MoveNext();
            }
        }

        public override void Reset()
        {
            IsDone = false;
            waitInstruction?.Reset();
        }
    }
}
