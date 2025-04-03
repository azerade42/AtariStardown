using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallController ball = collision.gameObject.GetComponent<BallController>();
        if (ball != null)
        {
            ball.Damage(1);
        }
    }
}
