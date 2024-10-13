using System.Collections;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    public enum MovementDirection { Vertical, Horizontal }   // Enum to choose the movement direction
    [SerializeField] private MovementDirection movementDirection = MovementDirection.Vertical;  // Default is vertical

    [SerializeField] private float movementSpeed = 5f;       // Speed at which the spike head moves
    [SerializeField] private float damageAmount = 1f;        // Damage amount dealt to the player
    [SerializeField] private float movementDelay = 2f;       // Time before the spike head starts moving
    private bool isMoving = false;                           // Whether the spike head is currently moving

    private void Start()
    {
        // Start the movement after a delay
        StartCoroutine(StartMovementAfterDelay());
    }

    // Coroutine to delay the movement by a few seconds
    private IEnumerator StartMovementAfterDelay()
    {
        // Wait for the specified delay before triggering the movement
        yield return new WaitForSeconds(movementDelay);
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (movementDirection == MovementDirection.Vertical)
            {
                // Spike head falls down when triggered
                transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
            }
            else if (movementDirection == MovementDirection.Horizontal)
            {
                // Spike head moves horizontally when triggered
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            }
        }
    }

    // Detect collisions with the player or ground
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // If the spike hits the player, apply damage using the player's existing Health component
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
            // Optionally, destroy or deactivate the spike head after it hits the player
        }
        else if (collision.CompareTag("Ground"))
        {
            // If the spike hits the ground, destroy it or stop it from moving
            Destroy(gameObject);  // Remove the spike head after it hits the ground
        }
    }
}
