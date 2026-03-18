using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;  // Drag points here
    public float speed = 3f;
    public float waitTime = 1f;    // Pause at each point

    private int currentWaypoint = 0;
    private float waitTimer = 0f;

    void Update()
    {
        if (waypoints.Length == 0) return;

        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        Transform target = waypoints[currentWaypoint];
        Vector3 direction = (target.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 0.1f)
        {
            // Reached waypoint
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            waitTimer = waitTime;
        }
        else
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Visualize waypoints
    void OnDrawGizmos()
    {
        if (waypoints == null) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                Gizmos.DrawSphere(waypoints[i].position, 0.2f);
                if (i < waypoints.Length - 1 && waypoints[i + 1] != null)
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                else if (i == waypoints.Length - 1 && waypoints[0] != null)
                    Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
            }
        }
    }
}