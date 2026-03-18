using UnityEngine;

public class Coin : MonoBehaviour
{
    public float spinSpeed = 100f;
    public float bobHeight = 0.2f;
    public float bobSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
        float bob = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = startPos + Vector3.up * bob;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<CoinManager>()?.CollectCoin();
            Destroy(gameObject);
        }
    }
}