using UnityEngine;

public class Breakable : MonoBehaviour
{
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
            Destroy(gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = lifeColors[currentLives - 1];
        }
    }


}
