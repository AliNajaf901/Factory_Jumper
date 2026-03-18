using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject player;  
    private bool isPaused = false;

    void Start()
    {
        pausePanel?.SetActive(false);
    }

    void Update()
    {
        bool pausePressed = Keyboard.current.escapeKey.wasPressedThisFrame
                         || Keyboard.current.pKey.wasPressedThisFrame;

        if (pausePressed)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        pausePanel?.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

       
        if (player != null)
            player.GetComponent<FPSController>().enabled = false;
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel?.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

      
        if (player != null)
            player.GetComponent<FPSController>().enabled = true;
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}