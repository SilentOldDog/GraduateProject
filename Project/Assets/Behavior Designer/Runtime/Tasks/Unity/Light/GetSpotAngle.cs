using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
    [TaskCategory("Unity/Light")]
    [TaskDescription("Stores the spot angle of the light.")]
    public class GetSpotAngle : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [RequiredField]
        [Tooltip("The spot angle to store")]
        public SharedFloat storeValue;

        // cache the light component
        private UnityEngine.Light light;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                light = currentGameObject.GetComponent<UnityEngine.Light>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (light == null) {
                Debug.LogWarning("Light is null");
                return TaskStatus.Failure;
            }

            storeValue = light.spotAngle;
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            storeValue = 0;
        }
    }
}