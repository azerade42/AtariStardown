using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameCanvas : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _loseText;
    [SerializeField] private TextMeshProUGUI _winText;

    public void EndGame(bool win)
    {
        if (win)
        {
            _winText.gameObject.SetActive(true);
        }
        else
        {
            _loseText.gameObject.SetActive(true);
        }

        _restartButton.gameObject.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
