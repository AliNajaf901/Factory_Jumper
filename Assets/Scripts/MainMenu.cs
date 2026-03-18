using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject howToPanel;

    void Start()
    {
        ShowMenu();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play()
    {
        SceneManager.LoadScene("Factory_Jumper");
    }

    public void ShowHowTo()
    {
        menuPanel.SetActive(false);
        howToPanel.SetActive(true);
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        howToPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}