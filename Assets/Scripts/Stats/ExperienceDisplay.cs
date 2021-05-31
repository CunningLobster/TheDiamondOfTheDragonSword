using System.Collections;
using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        TMP_Text experience;

        private void Awake()
        {
            experience = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            experience.text = player.GetComponent<Experience>().GetExperience().ToString();
        }
    }
}