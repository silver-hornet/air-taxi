using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] float distanceToPlayer;    
    [SerializeField] float moveSpeed;
    bool hasBoarded;
    [SerializeField] GameObject destination;
    [SerializeField] float distanceToDestination;
    bool hasArrived;
    Collider2D collider;
    [SerializeField] Material black;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < distanceToPlayer && PlayerController.instance.HasLanded() && hasBoarded == false)
        {
            destination.GetComponent<MeshRenderer>().material = black;
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, moveSpeed * Time.deltaTime);
        }

        if (hasBoarded)
            transform.position = PlayerController.instance.transform.position;

        if (Vector3.Distance(transform.position, destination.transform.position) < distanceToDestination && PlayerController.instance.HasLanded())
        {
            hasArrived = true;
            hasBoarded = false;
        }

        if (hasArrived)
        {
            collider.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, moveSpeed * Time.deltaTime);
            //todo colliders are colliding together, which means the NPC does not move until player has flown away
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && hasArrived == false)
            hasBoarded = true;
    }
}