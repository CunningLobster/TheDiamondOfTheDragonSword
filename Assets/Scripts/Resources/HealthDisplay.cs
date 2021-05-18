using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Resources;

public class HealthDisplay : MonoBehaviour
{
    TMP_Text health;
    void Awake()
    {
        health = GetComponent<TMP_Text>();
        DisplayHealthPercent();
    }


    void Update()
    {
        DisplayHealthPercent();
    }

    private void DisplayHealthPercent()
    {
        Health playerHealthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        health.text = playerHealthComponent.GetHealthPercent().ToString() + "%";
    }

}
