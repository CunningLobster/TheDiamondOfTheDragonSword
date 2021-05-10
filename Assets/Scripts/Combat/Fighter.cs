using System.Collections;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenHits = 1f;
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;

        float timeFromLastHit = Mathf.Infinity;
        Health target;

        private void Start()
        {
            SpawnWeapon();
        }

        private void Update()
        {
            timeFromLastHit += Time.deltaTime;

            if (target == null) { return; }
            if (target.IsDead) { Cancel();  return; }

            if (!IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
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

            if (timeBetweenHits <= timeFromLastHit)
            {
                GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("Attack");
                timeFromLastHit = 0f;
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }
            Health testTarget = combatTarget.GetComponent<Health>();
            return testTarget != null && testTarget.IsDead == false;
        }

        bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= weapon.GetWeaponRange();
        }

        private void SpawnWeapon()
        {
            if (weapon == null) { return; }
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform, animator);
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
            GetComponent<Mover>().Cancel();
            target = null;
        }

        void Hit()
        {
            if (target == null) { return; }
            target.TakeDamage(weapon.GetWeaponDamage());
        }
    }
}
