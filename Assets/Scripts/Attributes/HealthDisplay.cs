using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
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
            Health playerHealthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            health.text = String.Format("{0:0}/{1:0}", playerHealthComponent.GetHealth(), playerHealthComponent.GetMaximumHealth());
        }

    }
}

