using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float startBallSpeed = 10f;
    private Rigidbody2D rb;
    [SerializeField] Transform player;
    [SerializeField] private int startLives;
    [SerializeField] private EndGameCanvas endGameCanvas;
    private int lives;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lives = startLives;
        transform.position = player.transform.localPosition + player.transform.up;

        Invoke(nameof(StartMoving), 1f);
    }

    private void StartMoving()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        Vector2 forceDir = Vector2.right + Vector2.up;
        rb.AddForce(forceDir * startBallSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Breakable breakable = collision.gameObject.GetComponent<Breakable>();
        if (breakable != null)
        {
            breakable.RemoveLife();
        }
    }

    public void Damage(int damage)
    {
        lives -= damage;
        if (lives > 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            rb.linearVelocity = Vector2.zero;
            transform.position = player.transform.localPosition + player.transform.up;
            Invoke(nameof(StartMoving), 3f);
        }
        else
        {
            endGameCanvas.EndGame(false);
            Destroy(gameObject);
        }
    }
}
