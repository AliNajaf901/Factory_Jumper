using UnityEngine;

public class Door : MonoBehaviour
{
    public float openHeight = 3f;
    public float openSpeed = 4f;
    public float closeDelay = 1f;

    private Vector3 closedPos;
    private Vector3 openPos;
    private float targetY;
    private bool isOpen = false;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + Vector3.up * openHeight;
        targetY = closedPos.y;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = Mathf.MoveTowards(pos.y, targetY, openSpeed * Time.deltaTime);
        transform.position = pos;
    }

    public void Open()
    {
        CancelInvoke(nameof(StartClosing));
        targetY = openPos.y;
        isOpen = true;
    }

    public void Close()
    {
        if (isOpen)
        {
            Invoke(nameof(StartClosing), closeDelay);
        }
    }

    void StartClosing()
    {
        targetY = closedPos.y;
        isOpen = false;
    }
}