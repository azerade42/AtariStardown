using System;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public Action OnDestroy;
    [SerializeField] private Color[] lifeColors;
    private int currentLives;

    private void Start()
    {
        currentLives = lifeColors.Length;

        if (currentLives <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = lifeColors[currentLives - 1];
        }
    }
    
    public void RemoveLife()
    {
        if (--currentLives <= 0)
        {
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = lifeColors[currentLives - 1];
        }
    }


}
