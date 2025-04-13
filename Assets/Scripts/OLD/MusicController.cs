using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource audioSource;
    float maxVolume;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxVolume = audioSource.volume;
        StartCoroutine(CrossFadeVolume(true, 2f));
    }
    private void OnEnable()
    {
        StarControllerManager.OnAllStarsSaved += FadeOutMusic;
        BallController.OnDied += FadeOutMusic;
    }

    private void OnDisable()
    {
        StarControllerManager.OnAllStarsSaved -= FadeOutMusic;
        BallController.OnDied -= FadeOutMusic;
    }

    private void FadeOutMusic() => StartCoroutine(CrossFadeVolume(false, 3f));

    private IEnumerator CrossFadeVolume(bool turnUp, float fadeTime)
    {
        audioSource.Play();
        float curTime = 0;
        while (curTime < fadeTime)
        {
            float step = turnUp ? curTime/fadeTime : 1 - curTime / fadeTime;
            audioSource.volume = Mathf.Lerp(0, maxVolume, step);
            curTime += Time.deltaTime;
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                audioSource.Play();
                Time.timeScale = 1;
            }
            else
            {
                audioSource.Pause();
                Time.timeScale = 0;
            }
        }
    }
}
