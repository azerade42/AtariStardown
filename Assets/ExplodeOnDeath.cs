using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExplodeOnDeath : MonoBehaviour
{
    [SerializeField] GameObject ExplosionVFX;

    private SpriteRenderer spriteRenderer;
    private Light2D glowLight;
    [SerializeField] private GameObject engineGO;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        glowLight = GetComponent<Light2D>();
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
    private void Toggle(bool enabled)
    {
        spriteRenderer.enabled = enabled;
        glowLight.enabled = enabled;
        engineGO.SetActive(enabled);
        ExplosionVFX.SetActive(!enabled);
    }
}
