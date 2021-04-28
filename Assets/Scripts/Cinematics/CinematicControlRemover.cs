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
        void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        private void Update()
        {
            StopCutscene();
        }

        void DisableControl(PlayableDirector pd)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void EnableControl(PlayableDirector pd)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
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
