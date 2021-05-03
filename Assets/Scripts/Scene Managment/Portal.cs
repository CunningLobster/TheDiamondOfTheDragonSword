using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum Destinations { A, B, C, D }
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Destinations destination;

        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float timeBetweenFades = .2f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogWarning("Invalid scene index");
                yield break;
            }
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);
            FindObjectOfType<SavingWrapper>().Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            FindObjectOfType<SavingWrapper>().Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            FindObjectOfType<SavingWrapper>().Save();

            yield return new WaitForSeconds(timeBetweenFades);
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this || portal.destination != this.destination)
                {
                    continue;
                }
                return portal;
            }
            return null;
        }
    }
}