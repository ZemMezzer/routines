using UnityEngine;

namespace Game.Plugins.ZUtils.Routines
{
    public static class Routine
    {
        private static readonly RoutineHandler handler;
        
        static Routine()
        {
            var handlerGameObject = new GameObject
            {
                name = "[Routine Object]",
                hideFlags = HideFlags.HideInHierarchy
            };

            handler = handlerGameObject.AddComponent<RoutineHandler>();
        }
        
        public static void StartCoroutine(RoutineSequence sequence, UpdateType updateType)
        {
            handler.AddSequence(sequence, updateType);
        }

        public static void StopCoroutine(RoutineSequence sequence, UpdateType updateType)
        {
            handler.RemoveSequence(sequence, updateType);
        }

        public static void StopAllCoroutines()
        {
            handler.ClearSequences();
        }
    }
}
