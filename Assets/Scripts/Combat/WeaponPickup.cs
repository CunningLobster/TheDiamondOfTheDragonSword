using RPG.Attributes;
using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weapon = null;
        [SerializeField] float secondsForHide = 5f;
        [SerializeField] float healthToRestore = 0f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player") { return; }
            Pickup(other.gameObject);
        }

        private void Pickup(GameObject subject)
        {
            if (weapon != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(weapon);
            }
            if (healthToRestore > 0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore);
            }
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
                Pickup(callingController.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}