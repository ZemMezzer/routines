using System;
using System.Collections;
using System.Collections.Generic;

namespace Routines
{
    public class RoutineSequence : IEnumerator
    {
        public object Current => throw new NotSupportedException();

        private readonly List<RoutineInstruction> instructionsList;
        private int current;

        public bool IsRepeating { get; private set; }

        public RoutineSequence(bool isRepeating = false)
        {
            instructionsList = new List<RoutineInstruction>();
            IsRepeating = isRepeating;
        }
        
        public RoutineSequence(int instructionsCount, bool isRepeating = false)
        {
            instructionsList = new List<RoutineInstruction>(instructionsCount);
            IsRepeating = isRepeating;
        }
        
        public bool MoveNext()
        {
            if (instructionsList[current]!=null && !instructionsList[current].IsDone)
            {
                instructionsList[current].MoveNext();
            }
            else
            {
                instructionsList[current]?.Invoke();
                instructionsList[current]?.Reset();
                current++;
            }

            return current < instructionsList.Count;
        }

        public void Reset()
        {
            current = 0;
            
            foreach (var instruction in instructionsList)
            {
                instruction?.Reset();
            }
        }

        /// <summary>
        /// Works Like WaitForFixedUpdate if used in Fixed Update method and like null if used in Default Update method 
        /// </summary>
        public void AddEmptyInstruction()
        {
            instructionsList.Add(null);
        }
        
        public void AddInstruction(RoutineInstruction instruction)
        {
            instructionsList.Add(instruction);
        }

        public void AddInstruction(Action value)
        {
            instructionsList.Add(new RoutineInstruction(value));
        }
    }
}
