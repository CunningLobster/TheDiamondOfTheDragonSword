using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        Coroutine currentActiveFade;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        public IEnumerator FadeOut(float time)
        {
            if (currentActiveFade != null)
            {
                currentActiveFade = null;
            }
            currentActiveFade = StartCoroutine(FadeOutRoutine(time));
            yield return currentActiveFade;
        }

        private IEnumerator FadeOutRoutine(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += (Time.deltaTime / time);
                yield return null;
            }

        }

        public IEnumerator FadeIn(float time)
        {
            if (currentActiveFade != null)
            {
                currentActiveFade = null;
            }
            currentActiveFade = StartCoroutine(FadeInRoutine(time));
            yield return currentActiveFade;
        }

        private IEnumerator FadeInRoutine(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= (Time.deltaTime / time);
                yield return null;
            }

        }
    }
}