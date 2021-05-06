using RPG.Core;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string uniqueIdentifier = "";

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 serializablePosition = (SerializableVector3)state;
            transform.position = serializablePosition.ToVector();
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.isPlaying) { return; }
            if (string.IsNullOrEmpty(gameObject.scene.path)) { return; }

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}