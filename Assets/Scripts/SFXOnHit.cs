using System;
using UnityEngine;

public class SFXOnHit : MonoBehaviour
{
    //AudioSource SFX;
    [SerializeField] AudioClip hitSFX;
    public static event Action<AudioClip> OnHit;
    Collision2D ballCollider;

    /*private void Start()
    {
        //SFX = GetComponent<AudioSource>();   
    }*/
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (ballCollider == null && other.gameObject.GetComponent<BallController>())
        {
            ballCollider = other;
        }

        if (other == ballCollider)
            //SFX.Play();
            OnHit(hitSFX);
    }
}
