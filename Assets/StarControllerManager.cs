using System;
using System.Collections;
using UnityEngine;

public class StarControllerManager : MonoBehaviour
{
    public static Action OnAllStarsSaved;
    public static Action OnConstellationFinished;
    int numStarsRemaining;
    StarController[] stars;
    private AudioSource audioSource;
    [SerializeField] private SpriteRenderer constellation;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stars = transform.GetComponentsInChildren<StarController>();
        numStarsRemaining = stars.Length;
        foreach (StarController star in stars)
        {
            star.OnSaved += SaveStar;
        }
    }

    private void SaveStar()
    {
        if (--numStarsRemaining == 0)
        {
            audioSource.Play();
            OnAllStarsSaved?.Invoke();
            StartConstellationSequence();
        }
    }

    private void StartConstellationSequence()
    {
        StartCoroutine(FadeInConstellationImage(3f));
        foreach (StarController star in stars)
        {
            if (star.GetComponent<LineRenderer>())
            {
                star.GetComponent<LineRenderer>().enabled = true;
                star.FadeInLine(2f);
            }
        }
    }

    private IEnumerator FadeInConstellationImage(float fadeTime)
    {
        float curTime = 0;
        constellation.gameObject.SetActive(true);
        float maxAlpha = constellation.color.a;
        constellation.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(3f);

        while (curTime < fadeTime)
        {
            curTime += Time.deltaTime;
            constellation.color = new Color(1, 1, 1, curTime/fadeTime * maxAlpha);
            yield return null;
        }

        OnConstellationFinished?.Invoke();
    }


}
