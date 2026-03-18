using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreenUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public GameObject restartButton;
    public GameObject quitButton;

    void Start()
    {
        restartButton?.GetComponent<Button>()?.onClick.AddListener(Restart);
        quitButton?.GetComponent<Button>()?.onClick.AddListener(Quit);
    }

    public void SetTime(int minutes, int seconds, int milliseconds)
    {
        timeText?.SetText(string.Format("Time: {0}:{1:D2}.{2:D3}", minutes, seconds, milliseconds));
    }

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Factory_Jumper");
    }

    void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}