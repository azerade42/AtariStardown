using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Image fadeSprite;
    private void OnEnable()
    {
        StarControllerManager.OnConstellationFinished += LoadNextLevel;
        BallController.OnDied += WaitForReload;
    }

    private void OnDisable()
    {
        StarControllerManager.OnConstellationFinished -= LoadNextLevel;
        BallController.OnDied -= WaitForReload;
    }

    private void Start()
    {
        StartCoroutine(FadeLevel(true, 2f, 0));   
    }

    private void WaitForReload()
    {
        Invoke(nameof(ReloadLevel), 3f);
    }

    private void ReloadLevel()
    {
        StartCoroutine(FadeLevel(false, 2f, SceneManager.GetActiveScene().buildIndex));
    }

    private void RestartGame()
    {
        StartCoroutine(FadeLevel(false, 2f, 0));
    }

    private void LoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevel < 3)
        {
            StartCoroutine(FadeLevel(false, 2f, nextLevel));
        }
        else
        {
            Invoke(nameof(RestartGame), 5f);
        }
    }

    private IEnumerator FadeLevel(bool fadeIn, float fadeTime, int buildIndex)
    {
        float curTime = 0;
        fadeSprite.gameObject.SetActive(true);
        Color spriteColor = fadeSprite.color;
        
        while (curTime < fadeTime)
        {
            curTime += Time.deltaTime;
            float delta = fadeIn ? 1 - curTime/fadeTime : curTime/fadeTime;
            fadeSprite.color = spriteColor * new Color(1, 1, 1, delta);
            yield return null;
        }

        if (!fadeIn)
            SceneManager.LoadScene(buildIndex);
        else
            fadeSprite.gameObject.SetActive(false);
    }
}
