using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Combat;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        float maxSpeed;

        NavMeshAgent navMeshAgent;
        Health health;

        void Start()
        {
            health = GetComponent<Health>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            maxSpeed = navMeshAgent.speed;
        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead;
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Animator animator = GetComponent<Animator>();
            float velocity = navMeshAgent.velocity.magnitude;
            animator.SetFloat("forwardSpeed", velocity);
        }       

        public void StartMovingAction(Vector3 destination, float speedFraction)
        {           
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position =  position.ToVector();

            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
