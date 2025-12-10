using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    public CanvasGroup logo;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn(logo, 3.0f));
    }

    public IEnumerator FadeIn(CanvasGroup cg, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, t / duration);
            yield return null;
        }

        cg.alpha = 1f;

        yield return new WaitForSeconds(2.0f);
        StartCoroutine(FadeOut(cg, duration));
    }

    public IEnumerator FadeOut(CanvasGroup cg, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(1f, 0f, t / duration);
            yield return null;
        }

        cg.alpha = 0f;

        yield return new WaitForSeconds(2.50f);
        SceneManager.LoadScene("MainMenu");
    }
}
