using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade2 : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    public string targetSceneName;

    private bool isFading = false;

    private void Start()
    {
        fadeImage.enabled = false;
    }

    public void LoadSceneWithFade()
    {
        if (!isFading)
        {
            StartCoroutine(DoSceneTransition());
        }
    }

    IEnumerator DoSceneTransition()
    {
        isFading = true;
        fadeImage.enabled = true;
        float timer = 0;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1);
        SceneManager.LoadScene(targetSceneName);
    }
}