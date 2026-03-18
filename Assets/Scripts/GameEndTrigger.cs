using UnityEngine;

public class GameEndTrigger : MonoBehaviour
{
    public static float startTime;
    public GameObject winScreen;
    public GameObject player;  

    void Start()
    {
        startTime = Time.time;
        winScreen?.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float totalTime = Time.time - startTime;
            int minutes = Mathf.FloorToInt(totalTime / 60f);
            int seconds = Mathf.FloorToInt(totalTime % 60f);
            int milliseconds = Mathf.FloorToInt((totalTime * 1000f) % 1000f);

            WinScreenUI ui = winScreen?.GetComponent<WinScreenUI>();
            if (ui != null)
            {
                ui.SetTime(minutes, seconds, milliseconds);
            }

            winScreen?.SetActive(true);

           
            if (player != null)
                player.GetComponent<FPSController>().enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}