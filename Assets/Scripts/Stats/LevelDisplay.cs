using System.Collections;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        TMP_Text level;

        private void Start()
        {
            level = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            level.text = player.GetComponent<BaseStats>().CalculateLevel().ToString();
        }
    }
}