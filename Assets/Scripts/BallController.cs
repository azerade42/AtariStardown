using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BallController : MonoBehaviour
{
    public static Action OnDied;
    public static Action<bool> OnDeathToggled;
    
    [SerializeField] private float startBallSpeed = 10f;
    [SerializeField] Transform player;
    [SerializeField] private int startLives;
    [SerializeField] private GameObject explosion;

    private Rigidbody2D rb;
    private Light2D glowLight;
    private SpriteRenderer spriteRenderer;
    private int lives;

    private void OnEnable()
    {
        StarControllerManager.OnAllStarsSaved += Explode;
        GameManager.OnLifeLost += LifeLost;
        GameManager.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        StarControllerManager.OnAllStarsSaved -= Explode;
        GameManager.OnLifeLost -= LifeLost;
        GameManager.OnGameOver -= GameOver;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        glowLight = GetComponent<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        lives = startLives;

        transform.position = player.transform.localPosition + player.transform.up;
        transform.parent = player;
        Invoke(nameof(StartMoving), 1f);
    }

    private void StartMoving()
    {
        transform.position = player.transform.localPosition + player.transform.up;
        transform.parent = player;
        spriteRenderer.enabled = true;
        glowLight.enabled = true;
        OnDeathToggled?.Invoke(true);

        Invoke(nameof(LaunchBall), 1f);
    }

    private void LaunchBall()
    {
        transform.parent = null;
        Vector2 forceDir = Vector2.right + Vector2.up;
        rb.AddForce(forceDir * startBallSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        StarController hittable = collision.gameObject.GetComponent<StarController>();
        if (hittable != null)
        {
            hittable.AddHit();
        }
    }

    /*public void Damage(int damage)
    {
        lives -= damage;
        if (lives > 0)
        {
            rb.linearVelocity = Vector2.zero;
            spriteRenderer.enabled = false;
            glowLight.enabled = false;
            OnDeathToggled?.Invoke(false);
            
            Invoke(nameof(StartMoving), 3f);
        }
        else
        {
            OnDeathToggled?.Invoke(false);
            OnDied?.Invoke();
            gameObject.SetActive(false);
        }
    }*/

    private void LifeLost()
    {
        rb.linearVelocity = Vector2.zero;
        spriteRenderer.enabled = false;
        glowLight.enabled = false;
        OnDeathToggled?.Invoke(false);
        
        Invoke(nameof(StartMoving), 3f);
    }

    private void GameOver()
    {
        OnDeathToggled?.Invoke(false);
        OnDied?.Invoke();
        gameObject.SetActive(false);
    }

    private void Explode()
    {
        GetComponent<Collider2D>().enabled = false;
        rb.linearVelocity = Vector2.zero;

        explosion.SetActive(true);
        glowLight.enabled = false;
        spriteRenderer.enabled = false;
    }
}
