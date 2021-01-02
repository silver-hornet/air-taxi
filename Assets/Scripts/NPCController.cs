using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject destination;
    [SerializeField] GameObject nextNPC;
    SpriteRenderer npcSpriteRenderer;

    [Header("Config")]
    [SerializeField] float distanceToPlayer;
    [SerializeField] float moveSpeed;
    [SerializeField] float distanceToDestination;

    [Header("Bools")]
    bool hasBoarded;
    bool hasArrived;

    void Start()
    {
        npcSpriteRenderer = GetComponent<SpriteRenderer>();
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
            destination.GetComponent<SpriteRenderer>().color = npcSpriteRenderer.color;
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
            GameManager.instance.UpdateDeliveriesLeft();

            if (nextNPC != null)
                nextNPC.SetActive(true);
            else
                GameManager.instance.UpdateDeliveriesLeft();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && hasArrived == false)
            hasBoarded = true;

        //if (hasArrived)
        //    Physics2D.IgnoreCollision(PlayerController.instance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}