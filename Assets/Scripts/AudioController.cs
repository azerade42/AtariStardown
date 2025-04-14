using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
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
        ExplodeOnDeath.OnExplode += PlaySFX;
        SFXOnHit.OnHit += PlaySFX;
        StarControllerManager.OnWin += PlaySFX;
    }

    private void OnDisable()
    {
        StarControllerManager.OnAllStarsSaved -= FadeOutMusic;
        BallController.OnDied -= FadeOutMusic;
        ExplodeOnDeath.OnExplode -= PlaySFX;
        SFXOnHit.OnHit -= PlaySFX;
        StarControllerManager.OnWin -= PlaySFX;
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

    private void PlaySFX(AudioClip sfx)
    {
        if (sfx != null)
        {
            audioSource.PlayOneShot(sfx);
        }
    }
}
