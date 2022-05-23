using UnityEngine;

namespace Game.Plugins.ZUtils.Routines.Instructions
{
    public sealed class WaitForSecondsInstruction : RoutineInstruction
    {
        private float timer;
        private readonly float delay;
        
        /// <summary>
        /// Works Like WaitForSecondsRealtime if used in Fixed Update method and like WaitForSeconds if used in Default Update method 
        /// </summary>
        public WaitForSecondsInstruction(float t)
        {
            delay = t;
            Reset();
        }

        public override void MoveNext()
        {
            IsDone = Time.realtimeSinceStartup >= timer;
        }


        public override void Reset()
        {
            timer = Time.realtimeSinceStartup + delay;
            IsDone = false;
        }
    }
}
