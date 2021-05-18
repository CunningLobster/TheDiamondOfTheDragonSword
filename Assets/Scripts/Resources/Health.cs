using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;

        public bool IsDead{get{ return isDead; } }

        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public float GetHealthPercent()
        {
            float healthPercent = (healthPoints / GetComponent<BaseStats>().GetHealth()) * 100;
            return Mathf.RoundToInt(healthPercent);
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);

            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) { return; }            

            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            float currentHealthPoints = (float)state;
            healthPoints = currentHealthPoints;
            if (currentHealthPoints == 0)
            {
                Die();
            }
        }

    }
}
