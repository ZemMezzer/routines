using System.Collections;
using Routines.Instructions;
using UnityEngine;

namespace Routines.Demo
{
    public class DemoRoutines : MonoBehaviour
    {
        private RoutineSequence sequence;
        
        private void Start()
        {
            StartCoroutine(Coroutine());
            CustomRoutine();
        }

        private void CustomRoutine()
        {
            sequence = new RoutineSequence();
            sequence.AddInstruction(new WaitForSecondsInstruction(5));
            sequence.AddInstruction(() => { Debug.Log($"Custom Routine Time : {Time.realtimeSinceStartup}"); });
            sequence.AddInstruction(new WaitForSecondsInstruction(5));
            sequence.AddInstruction(() => { Debug.Log($"Custom Routine Time : {Time.realtimeSinceStartup}"); });
                
            Routine.StartCoroutine(sequence, UpdateType.FixedUpdate);
        }

        private IEnumerator Coroutine()
        {
            yield return new WaitForSecondsRealtime(5);
            Debug.Log($"Default Routine Time : {Time.realtimeSinceStartup}");
            yield return new WaitForSecondsRealtime(5);
            Debug.Log($"Default Routine Time : {Time.realtimeSinceStartup}");
        }
    }
}
