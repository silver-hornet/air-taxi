using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public static PlayerController instance;
    Rigidbody2D rb;

    [Header("Config")]
    [SerializeField] float thrustForce;
    [SerializeField] float moveSpeed;
    [SerializeField] float dangerousSpeed;
    float currentSpeedX;
    float currentSpeedY;

    [Header("Bools")]
    bool hasLanded;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.AddForce(transform.up * thrustForce * Input.GetAxis("Vertical"));
        if (!hasLanded)
            rb.AddForce(transform.right * moveSpeed * Input.GetAxis("Horizontal"));
        currentSpeedX = rb.velocity.x;
        currentSpeedY = rb.velocity.y;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            hasLanded = true;

        if (currentSpeedX < -dangerousSpeed || currentSpeedX > dangerousSpeed)
            Debug.Log("Crash!");

        if (currentSpeedY < -dangerousSpeed || currentSpeedY > dangerousSpeed)
            Debug.Log("Crash!");
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            hasLanded = false;
    }

    public bool HasLanded()
    {
        return hasLanded;
    }
}