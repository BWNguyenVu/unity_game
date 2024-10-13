using UnityEngine;

public class Trammel : MonoBehaviour
{
    [SerializeField] private int health = 2;  // Trammel can take 2 hits

    // This method reduces health by 1 when called
    public void TakeDamage()
    {
        health--;  // Decrease health by 1

        // If health reaches 0, the object is destroyed or disabled
        if (health <= 0)
        {
            Destroy(gameObject);  // Destroy the Trammel object
            // Alternatively, you can disable it instead of destroying:
            // gameObject.SetActive(false);
        }
    }
}
