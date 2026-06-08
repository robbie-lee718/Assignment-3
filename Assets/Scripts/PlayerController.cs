using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private float minX = -2.5f;
    [SerializeField] private float maxX = 2.5f;
    [SerializeField] private float minY = -4.5f;
    [SerializeField] private float maxY = 4.5f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public AudioSource audioSource;
    public AudioClip shootClip;
    public int maxHealth = 3;

    private Vector2 moveInput;
    private float nextFire;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnAttack()
    {
        if (Time.time > nextFire)
        {
            if (audioSource != null && shootClip != null)
                audioSource.PlayOneShot(shootClip);

            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            nextFire = Time.time + fireRate;
        }
    }

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.InitializeHealth(maxHealth);
        }
    }

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.position += move * moveSpeed * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
