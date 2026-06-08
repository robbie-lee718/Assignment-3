using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 4f;
    public Transform player;
    public float despawnMargin = 0.1f;

    private Vector3 moveDirection;
    private bool hasTarget;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        if (player != null)
        {
            Vector3 targetPosition = player.position;
            moveDirection = (targetPosition - transform.position).normalized;
            hasTarget = moveDirection.sqrMagnitude > 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTarget)
        {
            return;
        }

        transform.position += moveDirection * speed * Time.deltaTime;

        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            return;
        }

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        if (viewportPos.x < -despawnMargin || viewportPos.x > 1 + despawnMargin ||
            viewportPos.y < -despawnMargin || viewportPos.y > 1 + despawnMargin)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject); // Destroy player
            GameManager.Instance.GameOver(); // End and restart game
        }
    }
}
