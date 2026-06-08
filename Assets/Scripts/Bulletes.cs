using UnityEngine;

public class Bulletes : MonoBehaviour
{
    float speed = 7f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.y > 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayEnemyExplosion();
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
            GameManager.Instance.AddScore(1);
        }
    }
}
