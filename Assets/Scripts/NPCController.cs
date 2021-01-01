using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject destination;
    SpriteRenderer NPCSR;
    SpriteRenderer destinationColor;

    [Header("Config")]
    [SerializeField] float distanceToPlayer;    
    [SerializeField] float moveSpeed;
    [SerializeField] float distanceToDestination;

    [Header("Bools")]
    bool hasBoarded;
    bool hasArrived;

    void Start()
    {
        NPCSR = GetComponent<SpriteRenderer>();
        NPCSR.color = new Color(1, 0, 0, 1);
    }

    void Update()
    {
        Boarding();
        Arriving();
    }

    private void Boarding()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < distanceToPlayer && PlayerController.instance.HasLanded() && hasBoarded == false)
        {
            destination.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeed * Time.deltaTime);
        }

        if (hasBoarded)
            transform.position = PlayerController.instance.transform.position;
    }

    private void Arriving()
    {
        if (Vector3.Distance(transform.position, destination.transform.position) < distanceToDestination && PlayerController.instance.HasLanded())
        { // todo bug: when crashing sideways into a nearby vertical platform
            hasArrived = true;
            hasBoarded = false;
        }

        if (hasArrived)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, moveSpeed * Time.deltaTime);
            //todo bug: colliders are colliding together, which means the NPC does not move until player has flown away
        }

        if (transform.position == destination.transform.position && hasArrived)
        {
            destination.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && hasArrived == false)
            hasBoarded = true;
    }
}