using RPG.Core;
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
        Projectile projectile = null;

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
            Transform handTransform = GetTransform(rightHand, leftHand);

            if (weaponPrefab != null)
            {
                Instantiate(weaponPrefab, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) { handTransform = rightHand; }
            else { handTransform = leftHand; }

            return handTransform;
        }

        public void LaunchProjectile(Transform leftHand, Transform rightHand, Health target)
        {
            Projectile projectileInstanse =  Instantiate(projectile, GetTransform(leftHand, rightHand).position, Quaternion.identity);
            projectileInstanse.SetTarget(target);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }
    }
}
