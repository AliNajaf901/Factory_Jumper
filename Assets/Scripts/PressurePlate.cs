using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Door door;
    public float pressDepth = 0.1f;
    public float checkRadius = 0.4f;

    private Vector3 startPos;
    private bool isPressed = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        
        bool playerAbove = false;
        Collider[] hits = Physics.OverlapSphere(transform.position + Vector3.up * 0.5f, checkRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                
                RaycastHit downHit;
                if (Physics.Raycast(hit.transform.position, Vector3.down, out downHit, 2f))
                {
                    if (downHit.collider.gameObject == gameObject)
                    {
                        playerAbove = true;
                        break;
                    }
                }
            }
        }

        
        if (playerAbove && !isPressed)
        {
            isPressed = true;
            transform.position = startPos - Vector3.up * pressDepth;
            door.Open();
        }
        else if (!playerAbove && isPressed)
        {
            isPressed = false;
            transform.position = startPos;
            door.Close();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f, checkRadius);
    }
}