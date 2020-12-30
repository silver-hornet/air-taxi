using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    Rigidbody2D rb;
    [SerializeField] float thrustForce;
    [SerializeField] float moveSpeed;
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
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            hasLanded = true;
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