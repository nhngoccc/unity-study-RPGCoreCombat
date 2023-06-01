using UnityEngine;
using System.Collections;
namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float timeToFadeOut = 2f;
        CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(FadeOut(timeToFadeOut));
        }
        public IEnumerator FadeOut(float timeToFadeOut)
        {
            while (canvasGroup.alpha != 1)
            {
                canvasGroup.alpha += (Time.deltaTime) / timeToFadeOut;
                yield return null;
            }

        }

    }
}
