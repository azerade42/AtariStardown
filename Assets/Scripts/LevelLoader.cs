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
        EndGameCanvasController.OnRestartPressed += RestartGame;
    }

    private void OnDisable()
    {
        StarControllerManager.OnConstellationFinished -= LoadNextLevel;
        EndGameCanvasController.OnRestartPressed -= RestartGame;
    }

    private void Start()
    {
        StartCoroutine(FadeLevel(true, 2f, 0));   
    }

    public void RestartGame(int index)
    {
        StartCoroutine(FadeLevel(false, 2f, index));
    }

    private void LoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevel < 3)
        {
            StartCoroutine(FadeLevel(false, 2f, nextLevel));
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
            fadeSprite.color =  new Color(spriteColor.r, spriteColor.g, spriteColor.b, delta);
            yield return null;
        }

        if (!fadeIn)
            SceneManager.LoadScene(buildIndex);
        
        fadeSprite.gameObject.SetActive(false);
    }
}
