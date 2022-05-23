using System;
using System.Collections.Generic;
using UnityEngine;

namespace Routines
{
    public class RoutineHandler : MonoBehaviour
    {
        private readonly List<RoutineSequence> fixedUpdateRoutineSequences = new List<RoutineSequence>();
        private readonly List<RoutineSequence> updateRoutineSequences = new List<RoutineSequence>();
        private readonly List<RoutineSequence> lateUpdateRoutineSequences = new List<RoutineSequence>();

        private readonly Queue<RoutineSequence> fixedUpdateRoutineSequencesToRemove = new Queue<RoutineSequence>();
        private readonly Queue<RoutineSequence> updateRoutineSequencesToRemove = new Queue<RoutineSequence>();
        private readonly Queue<RoutineSequence> lateUpdateRoutineSequencesToRemove = new Queue<RoutineSequence>();

        public void AddSequence(RoutineSequence sequence, UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.Update:
                    updateRoutineSequences.Add(sequence);
                    break;
                case UpdateType.FixedUpdate:
                    fixedUpdateRoutineSequences.Add(sequence);
                    break;
                case UpdateType.LateUpdate:
                    lateUpdateRoutineSequences.Add(sequence);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
            }
        }

        public void RemoveSequence(RoutineSequence sequence, UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.Update:
                    updateRoutineSequences.Remove(sequence);
                    break;
                case UpdateType.FixedUpdate:
                    fixedUpdateRoutineSequences.Remove(sequence);
                    break;
                case UpdateType.LateUpdate:
                    lateUpdateRoutineSequences.Remove(sequence);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
            }
        }

        public void ClearSequences()
        {
            updateRoutineSequences.Clear();
            fixedUpdateRoutineSequences.Clear();
            lateUpdateRoutineSequences.Clear();
        }
        
        private void FixedUpdate()
        {
            MoveNext(fixedUpdateRoutineSequences, fixedUpdateRoutineSequencesToRemove);
            RemoveSequences(fixedUpdateRoutineSequences, fixedUpdateRoutineSequencesToRemove);
        }

        private void Update()
        {
            MoveNext(updateRoutineSequences, updateRoutineSequencesToRemove);
            RemoveSequences(updateRoutineSequences, updateRoutineSequencesToRemove);
        }

        private void LateUpdate()
        {
            MoveNext(lateUpdateRoutineSequences, lateUpdateRoutineSequencesToRemove);
            RemoveSequences(lateUpdateRoutineSequences, lateUpdateRoutineSequencesToRemove);
        }

        private void MoveNext(List<RoutineSequence> instructionsList, Queue<RoutineSequence> instructionsToRemove)
        {
            foreach (var instructionsSequence in instructionsList)
            {
                if (!instructionsSequence.MoveNext())
                {
                    if (instructionsSequence.IsRepeating)
                    {
                        instructionsSequence.Reset();
                    }
                    else
                    {
                        instructionsToRemove.Enqueue(instructionsSequence);
                    }
                }
            }
        }


        private void RemoveSequences(List<RoutineSequence> instructionsList, Queue<RoutineSequence> instructionsToRemove)
        {
            int count = instructionsToRemove.Count;

            for (int i = 0; i < count; i++)
            {
                instructionsList.Remove(instructionsToRemove.Dequeue());
            }
        }
    }
}
