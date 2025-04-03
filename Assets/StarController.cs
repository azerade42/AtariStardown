using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StarController : MonoBehaviour
{
    public Action OnSaved;
    [SerializeField] private Color[] hitColors;
    private int currentHits = 0;
    private SpriteRenderer spriteRenderer;
    private Light2D glowLight;
    private SpinController spinController;

    private bool saved;

    private void OnEnable()
    {
        StarControllerManager.OnAllStarsSaved += SpinFasterAndGrow;
        BallController.OnDied += FallFromSky;
    }

    private void OnDisable()
    {
        StarControllerManager.OnAllStarsSaved -= SpinFasterAndGrow;
        BallController.OnDied -= FallFromSky;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        glowLight = GetComponent<Light2D>();
        spinController = GetComponent<SpinController>();
        spriteRenderer.color = hitColors[currentHits];
    }
    
    public void AddHit()
    {
        if (saved) return;

        spriteRenderer.color = hitColors[++currentHits];
        glowLight.color = hitColors[currentHits];

        if (currentHits >= hitColors.Length - 1)
        {
            saved = true;
            OnSaved?.Invoke();
        }
    }

    public void SpinFasterAndGrow()
    {
        StartCoroutine(SpinAndGrowGradually(1f));
    }

    private IEnumerator SpinAndGrowGradually(float endTime)
    {
        float curTime = 0;
        float startSpeed = spinController.SpinSpeed;
        float goalSpeed = startSpeed * 1.75f;
        float startSize = transform.localScale.x;
        float goalSize = startSize * 1.75f;

        while (curTime < endTime)
        {
            curTime += Time.deltaTime;
            float step = curTime / endTime;
            spinController.SpinSpeed = Mathf.Lerp(startSpeed, goalSpeed, step);
            Vector2 nextScale = Mathf.Lerp(startSize, goalSize, step) * Vector2.one;
            transform.localScale = (Vector3)nextScale + Vector3.forward;
            yield return null;
        }
    }

    private void FallFromSky()
    {
        GetComponent<Collider2D>().enabled = false;
        gameObject.AddComponent<Rigidbody2D>();
    }


}
