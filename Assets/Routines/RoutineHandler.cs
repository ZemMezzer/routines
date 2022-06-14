using System;
using System.Collections.Generic;
using UnityEngine;

namespace Routines
{
    public class RoutineHandler : MonoBehaviour
    {
        private readonly Dictionary<UpdateType, List<RoutineSequence>> sequences = new Dictionary<UpdateType, List<RoutineSequence>>();

        public void AddSequence(RoutineSequence sequence, UpdateType updateType)
        {
            if (!sequences.ContainsKey(updateType))
            {
                sequences.Add(updateType, new List<RoutineSequence>());
            }

            sequences[updateType].Add(sequence);
        }

        public void RemoveSequence(RoutineSequence sequence, UpdateType updateType)
        {
            sequences[updateType].Remove(sequence);
        }

        public void ClearSequences()
        {
            foreach (var sequenceKeyValuePair in sequences)
            {
                sequenceKeyValuePair.Value.Clear();
            }
        }
        
        private void FixedUpdate()
        {
            if (sequences.ContainsKey(UpdateType.FixedUpdate))
            {
                MoveNext(sequences[UpdateType.FixedUpdate]);
            }
        }

        private void Update()
        {
            if (sequences.ContainsKey(UpdateType.Update))
            {
                MoveNext(sequences[UpdateType.Update]);
            }
        }

        private void LateUpdate()
        {
            if (sequences.ContainsKey(UpdateType.LateUpdate))
            {
                MoveNext(sequences[UpdateType.LateUpdate]);
            }
        }

        private void MoveNext(List<RoutineSequence> instructionsList)
        {
            for (int i = instructionsList.Count - 1; i >= 0; i--)
            {
                var instructionsSequence = instructionsList[i];
                
                if (!instructionsSequence.MoveNext())
                {
                    if (instructionsSequence.IsRepeating)
                    {
                        instructionsSequence.Reset();
                    }
                    else
                    {
                        instructionsList.RemoveAt(i);
                    }
                }
            }
        }
    }
}
