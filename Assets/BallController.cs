using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float startBallSpeed = 10f;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke(nameof(StartMoving), 1f);
    }

    private void StartMoving()
    {
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
}
