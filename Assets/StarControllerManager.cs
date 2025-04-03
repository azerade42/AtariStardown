using System;
using UnityEngine;

public class StarControllerManager : MonoBehaviour
{
    public static Action OnAllStarsSaved;
    int numStarsRemaining;
    StarController[] stars;
    private void Start()
    {
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
            OnAllStarsSaved?.Invoke();
        }
    }
}
