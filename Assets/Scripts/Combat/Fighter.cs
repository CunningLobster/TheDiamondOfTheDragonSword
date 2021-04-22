using System.Collections;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 20f;
        [SerializeField] float timeBetweenHits = 1f;

        float timeFromLastHit = 0f;
        bool isAbleToHit = true;
        Health target;


        private void Update()
        {
            timeFromLastHit += Time.deltaTime;

            if (target == null) { return; }
            if (target.IsDead) { return; }

            if (!IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (isAbleToHit)
            {
                GetComponent<Animator>().SetTrigger("Attack");
                isAbleToHit = false;
                timeFromLastHit = 0f;
            }
            SetIsAbleToHit();
        }

        //Make it more elegant
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
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }


        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

        void Hit()
        {
            if (target == null) { return; }
            target.TakeDamage(weaponDamage);
        }
    }
}
