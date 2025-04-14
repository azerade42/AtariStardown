using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExplodeOnDeath : MonoBehaviour
{
    [SerializeField] GameObject ExplosionVFX;
    [SerializeField] AudioClip ExplosionSFX;

    private SpriteRenderer spriteRenderer;
    private Light2D glowLight;
    //private AudioSource audioSource;
    public static event Action<AudioClip> OnExplode;
    [SerializeField] private GameObject engineGO;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        glowLight = GetComponent<Light2D>();
        //audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        BallController.OnDied += ToggleOff;
        BallController.OnDeathToggled += Toggle;
    }

    private void OnDisable()
    {
        BallController.OnDied -= ToggleOff;
        BallController.OnDeathToggled -= Toggle;
    }

    private void ToggleOff() => Toggle(false);
    private void Toggle(bool enableShip)
    {
        spriteRenderer.enabled = enableShip;
        glowLight.enabled = enableShip;
        engineGO.SetActive(enableShip);
        ExplosionVFX.SetActive(!enableShip);

        if (!enableShip)
            //audioSource.Play();
            OnExplode(ExplosionSFX);
    }
}
