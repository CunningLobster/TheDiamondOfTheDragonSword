using System.Collections;
using UnityEngine;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experiencePoints = 0;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
        }
    }
}