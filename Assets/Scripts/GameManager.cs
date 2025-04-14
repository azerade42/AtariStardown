using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject audioManagerObject;
    [SerializeField] int startLives = 3;
    [SerializeField] Image[] livesIcons;
    public static event Action OnLifeLost;
    public static event Action OnGameOver;
    private int lives;
    private AudioSource audioSource;

    private void OnEnable()
    {
        KillZone.OnDamage += Damage;
    }

    private void OnDisable()
    {
        KillZone.OnDamage -= Damage;
    }

    private void Awake()
    {
        audioSource = audioManagerObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        lives = startLives;
    }

    private void Damage(int livesLost)
    {
        lives -= livesLost;
        UpdateLivesUI(lives);
        if (lives > 0)
        {
            OnLifeLost();
        }
        else
        {
            OnGameOver();
        }
    }

    private void UpdateLivesUI(int livesCount)
    {
        livesIcons[livesCount].enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                audioSource.Play();
                Time.timeScale = 1;
            }
            else
            {
                audioSource.Pause();
                Time.timeScale = 0;
            }
        }
    }
}
