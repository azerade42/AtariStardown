using System;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public static event Action<int> OnDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*BallController ball = collision.gameObject.GetComponent<BallController>();
        if (ball != null)
        {
            ball.Damage(1);
        }*/

        if (collision.gameObject.GetComponent<BallController>() != null)
        {
            OnDamage(1);
        }
    }
}
