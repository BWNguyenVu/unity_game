using System.Collections;
using UnityEngine;

public class Enemy_Sideway : MonoBehaviour
{
    public enum MovementDirection { Horizontal, Vertical }  // Enum to choose movement direction

    [SerializeField] private MovementDirection movementDirection = MovementDirection.Horizontal;  // Default to Horizontal
    [SerializeField] private float movementDistance;  // Distance for the movement
    [SerializeField] private float speed;             // Speed of movement
    [SerializeField] private float damage;            // Damage dealt to the player
    private bool movingNegative;                      // Whether the enemy is moving left (horizontal) or down (vertical)

    private float negativeEdge;  // Left or bottom edge based on movement direction
    private float positiveEdge;  // Right or top edge based on movement direction

    private void Awake()
    {
        // Set the edges depending on the movement direction
        if (movementDirection == MovementDirection.Horizontal)
        {
            negativeEdge = transform.position.x - movementDistance;  // Left edge
            positiveEdge = transform.position.x + movementDistance;  // Right edge
        }
        else if (movementDirection == MovementDirection.Vertical)
        {
            negativeEdge = transform.position.y - movementDistance;  // Bottom edge
            positiveEdge = transform.position.y + movementDistance;  // Top edge
        }
    }

    private void Update()
    {
        if (movementDirection == MovementDirection.Horizontal)
        {
            MoveHorizontally();
        }
        else if (movementDirection == MovementDirection.Vertical)
        {
            MoveVertically();
        }
    }

    private void MoveHorizontally()
    {
        if (movingNegative)
        {
            if (transform.position.x > negativeEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingNegative = false;  // Switch to move right
            }
        }
        else
        {
            if (transform.position.x < positiveEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingNegative = true;  // Switch to move left
            }
        }
    }

    private void MoveVertically()
    {
        if (movingNegative)
        {
            if (transform.position.y > negativeEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingNegative = false;  // Switch to move up
            }
        }
        else
        {
            if (transform.position.y < positiveEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingNegative = true;  // Switch to move down
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
