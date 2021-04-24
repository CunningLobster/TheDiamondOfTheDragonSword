using System.Collections;
using UnityEngine;

namespace RPG.Control
{ 
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float sphereRadius = .1f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(GetWaypointPosition(i), sphereRadius);
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypointPosition(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}