using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameCanvasController : MonoBehaviour
{
    public static Action<int> OnRestartPressed;
    public static Action OnScreenEnabled;
    bool isLastLevel;
    bool levelLost;

    [SerializeField] private RectTransform _contentHolder;
    [SerializeField] private TextMeshProUGUI _labelText;
    [SerializeField] private TMP_ColorGradient _loseGradient;
    [SerializeField] private TMP_ColorGradient _winGradient;

    private void OnEnable()
    {
        isLastLevel = SceneManager.GetActiveScene().buildIndex == 2;
        if (isLastLevel)
            StarControllerManager.OnConstellationFinished += EnableWinScreen;
        
        BallController.OnDied += EnableLoseScreen;
    }

    private void OnDisable()
    {
        if (isLastLevel)
            StarControllerManager.OnConstellationFinished -= EnableWinScreen;
        
        BallController.OnDied -= EnableLoseScreen;
    }

    private void EnableWinScreen()
    {
        levelLost = false;
        _labelText.text = "winner";
        _labelText.colorGradientPreset = _winGradient;
        EnableEndGameScreen();
    }

    private void EnableLoseScreen()
    {
        levelLost = true;
        _labelText.text = "Game over";
        _labelText.colorGradientPreset = _loseGradient;
        EnableEndGameScreen();
    }

    private void EnableEndGameScreen()
    {
        float newY = -Screen.height * 0.5f - _contentHolder.sizeDelta.y;
        _contentHolder.anchoredPosition = new Vector2(_contentHolder.anchoredPosition.x, newY);

        _contentHolder.gameObject.SetActive(true);
        OnScreenEnabled?.Invoke();

        StartCoroutine(MoveIntoCenterFrame(1f));
    }

    private IEnumerator MoveIntoCenterFrame(float timeToMove)
    {
        float curTime = 0;
        float startX = _contentHolder.anchoredPosition.x;
        float startY = _contentHolder.anchoredPosition.y;

        while (curTime < timeToMove)
        {
            curTime += Time.deltaTime;
            float yPos = Mathf.Lerp(startY, 0, curTime / timeToMove);
            _contentHolder.anchoredPosition = new Vector2(startX, yPos);
            yield return null;
        }
    }

    public void RestartGame()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (isLastLevel && !levelLost)
            OnRestartPressed?.Invoke(0);
        else
            OnRestartPressed?.Invoke(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
