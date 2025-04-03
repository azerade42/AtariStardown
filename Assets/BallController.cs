using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BallController : MonoBehaviour
{
    public static Action OnDied;
    [SerializeField] private float startBallSpeed = 10f;
    private Rigidbody2D rb;
    private Light2D glowLight;
    [SerializeField] Transform player;
    [SerializeField] private int startLives;
    [SerializeField] private EndGameCanvas endGameCanvas;
    private int lives;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        glowLight = GetComponent<Light2D>();
        lives = startLives;
        transform.position = player.transform.localPosition + player.transform.up;

        Invoke(nameof(StartMoving), 1f);
    }

    private void StartMoving()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        glowLight.enabled = true;

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

    public void Damage(int damage)
    {
        lives -= damage;
        if (lives > 0)
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = player.transform.localPosition + player.transform.up;
            GetComponent<SpriteRenderer>().enabled = false;
            glowLight.enabled = false;
            
            Invoke(nameof(StartMoving), 3f);
        }
        else
        {
            OnDied?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
