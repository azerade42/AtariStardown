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
        float maxAlpha = constellation.color.a;
        constellation.color = new Color(1, 1, 1, 0);
        
        constellation.gameObject.SetActive(true);
        SpriteRenderer constellationBG = constellation.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Color bgColor = constellationBG.color;
        float maxAlphaBG = 0.98f;
        constellationBG.gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);
        
        constellationBG.gameObject.SetActive(true);

        while (curTime < fadeTime)
        {
            curTime += Time.deltaTime;
            constellation.color = new Color(1, 1, 1, maxAlpha * curTime/fadeTime);
            constellationBG.color = new Color(bgColor.r, bgColor.g, bgColor.b, maxAlphaBG * curTime/fadeTime);
            yield return null;
        }

        OnConstellationFinished?.Invoke();
    }


}
