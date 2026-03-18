using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float disappearDelay = 0.5f;
    public float respawnTime = 2f;
    public float checkRadius = 0.4f;  

    private Renderer rend;
    private Collider col;
    private bool isActive = true;
    private bool timerStarted = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        if (!isActive || timerStarted) return;

        
        Collider[] hits = Physics.OverlapSphere(transform.position + Vector3.up * 0.5f, checkRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
               
                RaycastHit downHit;
                if (Physics.Raycast(hit.transform.position, Vector3.down, out downHit, 2f))
                {
                    if (downHit.collider == col)  
                    {
                        timerStarted = true;
                        Invoke(nameof(Disappear), disappearDelay);
                        break;
                    }
                }
            }
        }
    }

    void Disappear()
    {
        isActive = false;
        rend.enabled = false;
        col.enabled = false;
        Invoke(nameof(Respawn), respawnTime);
    }

    void Respawn()
    {
        isActive = true;
        rend.enabled = true;
        col.enabled = true;
        timerStarted = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f, checkRadius);
    }
}