using UnityEngine;

namespace Routines.Instructions
{
    public sealed class WaitForSecondsInstruction : RoutineInstruction
    {
        private float timer;
        private readonly float delay;

        private bool isInitialized;
        
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
            if (!isInitialized)
            {
                ResetTimer();
                isInitialized = true;
            }

            IsDone = Time.realtimeSinceStartup >= timer;
        }


        public override void Reset()
        {
            IsDone = false;
            isInitialized = false;
        }

        private void ResetTimer()
        {
            timer = Time.realtimeSinceStartup + delay;
        }
    }
}
