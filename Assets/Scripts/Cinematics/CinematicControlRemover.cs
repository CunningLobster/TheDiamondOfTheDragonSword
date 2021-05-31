using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        PlayableDirector playableDirector;
        GameObject player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playableDirector = GetComponent<PlayableDirector>();
        }

        private void OnEnable()
        {
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
        }

        private void OnDisable()
        {
            playableDirector.played -= DisableControl;
            playableDirector.stopped -= EnableControl;
        }

        private void Update()
        {
            StopCutscene();
        }

        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

        void StopCutscene()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GetComponent<PlayableDirector>().time > 0)
                {
                    GetComponent<PlayableDirector>().Stop();
                }
            }
        }
    }
}
