using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
    bool hasDied;

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
        if (!hasDied)
        {
            ProcessInput();
            PlayerBoundaries();
            CheckSpeed();
        }
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
            HasDied();

        if (currentSpeedY < -dangerousSpeed || currentSpeedY > dangerousSpeed)
            HasDied();
    }

    void HasDied()
    {
        CameraShake.instance.Shake();
        hasDied = true;
        rb.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
        rb.SetRotation(45f);
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(ResetGameCo());
    }

    IEnumerator ResetGameCo()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
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