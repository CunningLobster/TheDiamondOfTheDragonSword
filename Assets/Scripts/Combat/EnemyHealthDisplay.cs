using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Resources;
using System;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        TMP_Text health;
        void Awake()
        {
            health = GetComponent<TMP_Text>();
        }


        void Update()
        {
            DisplayHealthPercent();
        }

        private void DisplayHealthPercent()
        {
            Health targetHealthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>().GetTarget();
            if (targetHealthComponent != null)
            {
                health.text = String.Format("{0:f1}%", targetHealthComponent.GetHealthPercent());
            }
            else { health.text = "N/A"; }

        }
    }
}
