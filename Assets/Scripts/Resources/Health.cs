using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField]float healthPoints = -1f;
        bool isDead = false;

        public bool IsDead{get{ return isDead; } }

        private void Start()
        {
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        public float GetHealthPercent()
        {
            float healthPercent = (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health)) * 100;
            return healthPercent;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);

            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        private void Die()
        {
            if (isDead) { return; }

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null)
            {
                return;
            }

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints <= 0)
            {
                Die();
            }
        }

    }
}
