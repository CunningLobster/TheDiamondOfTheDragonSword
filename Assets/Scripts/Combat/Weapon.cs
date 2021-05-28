using RPG.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 20f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        string weaponName = "weapon";

        public float GetWeaponRange()
        {
            return weaponRange;
        }

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);


            if (weaponPrefab != null)
            {
                Transform handTransform = GetTransform(leftHand, rightHand);
                GameObject weapon = Instantiate(weaponPrefab, handTransform);
                weapon.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) { return; }

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform leftHand, Transform rightHand)
        {
            Transform handTransform;
            if (isRightHanded) { handTransform = rightHand; }
            else { handTransform = leftHand; }

            return handTransform;
        }

        public void LaunchProjectile(Transform leftHand, Transform rightHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstanse =  Instantiate(projectile, GetTransform(leftHand, rightHand).position, Quaternion.identity);
            projectileInstanse.SetTarget(instigator, target, calculatedDamage);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }
    }
}
