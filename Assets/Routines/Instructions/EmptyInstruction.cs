namespace Routines.Instructions
{
    public class EmptyInstruction : RoutineInstruction
    {
        public EmptyInstruction()
        {
            IsDone = true;
        }

        public override void Reset()
        {
            IsDone = true;
        }
    }
}
