using System.Collections;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenHits = 1f;

        float timeFromLastHit = 0f;
        bool isAbleToHit = true;
        Transform target;


        private void Update()
        {
            timeFromLastHit += Time.deltaTime;

            if (target == null) { return; }
            
            if (!IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (isAbleToHit)
            {
                GetComponent<Animator>().SetTrigger("Attack");
                isAbleToHit = false;
                timeFromLastHit = 0f;
            }
            SetIsAbleToHit();
        }

        private void SetIsAbleToHit()
        {
            if (!isAbleToHit)
            {
                if (timeBetweenHits < timeFromLastHit)
                {
                    isAbleToHit = true;
                }
            }
        }

        bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) <= weaponRange;
        }


        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        void Hit()
        { }
    }
}
