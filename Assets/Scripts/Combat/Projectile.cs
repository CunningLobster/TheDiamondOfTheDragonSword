using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        [SerializeField] float damage = 0;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject impactEffect = null;
        [SerializeField] float lifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;

        GameObject instigator = null;
        Health target = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        private void Update()
        {
            if (isHoming && !target.IsDead)
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(GameObject instigator ,Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, lifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.gameObject.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height/2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) { return; }
            if (target.IsDead) { return; }
            target.TakeDamage(instigator ,damage);
            speed = 0;

            if (impactEffect != null)
            {
                GameObject impactFX = Instantiate(impactEffect, GetAimLocation(), Quaternion.identity);
            }

            foreach (GameObject projectile in destroyOnHit)
            {
                Destroy(projectile);
            }

            Destroy(gameObject);
        }
    }
}
