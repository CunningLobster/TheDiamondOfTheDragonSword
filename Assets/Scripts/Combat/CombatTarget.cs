using RPG.Control;
using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(PlayerController callingController)
        {
            if (this.GetComponent<Health>().IsDead) { return false; }
            if (Input.GetMouseButtonDown(0))
            {
                if (!callingController.GetComponent<Fighter>().CanAttack(this.gameObject)) { return false; }

                if (Input.GetMouseButtonDown(0))
                {
                    callingController.GetComponent<Fighter>().Attack(this.gameObject);
                }
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }
    }
}

