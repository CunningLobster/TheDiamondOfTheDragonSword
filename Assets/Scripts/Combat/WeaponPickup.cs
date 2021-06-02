using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float secondsForHide = 5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player") { return; }
            Pickup(other.GetComponent<Fighter>());
        }

        private void Pickup(Fighter fighter)
        {
            fighter.EquipWeapon(weapon);
            StartCoroutine(HidePickupForSeconds(secondsForHide));
        }

        IEnumerator HidePickupForSeconds(float seconds)
        {
            HidePickup();
            yield return new WaitForSeconds(seconds);
            UnhidePickup();
        }

        private void HidePickup()
        {
            GetComponent<Collider>().enabled = false;
            foreach (Transform child in this.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        private void UnhidePickup()
        {
            GetComponent<Collider>().enabled = true;
            foreach (Transform child in this.transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(callingController.GetComponent<Fighter>());
            }
            return true;
        }
    }
}