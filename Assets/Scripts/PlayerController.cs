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
    float boundaryX = 9f;
    float boundaryY = 5f;

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
        ProcessInput();
        PlayerBoundaries();
        CheckSpeed();
    }

    void ProcessInput()
    {
        rb.AddForce(transform.up * thrustForce * Input.GetAxis("Vertical"));
        if (!hasLanded)
            rb.AddForce(transform.right * moveSpeed * Input.GetAxis("Horizontal"));
    }

    void PlayerBoundaries()
    {
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        playerPos.x = Mathf.Clamp(transform.position.x, -boundaryX, boundaryX);
        playerPos.y = Mathf.Clamp(transform.position.y, -boundaryY, boundaryY);
        transform.position = playerPos;
    }

    void CheckSpeed()
    {
        currentSpeedX = rb.velocity.x;
        currentSpeedY = rb.velocity.y;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            hasLanded = true;

        if (currentSpeedX < -dangerousSpeed || currentSpeedX > dangerousSpeed)
            Debug.Log("Camera Shake!");

        if (currentSpeedY < -dangerousSpeed || currentSpeedY > dangerousSpeed)
            Debug.Log("Camera Shake!");
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