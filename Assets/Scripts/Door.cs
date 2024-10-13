using UnityEngine;
using UnityEngine.UI;  // Required for UI elements
using UnityEngine.SceneManagement; // For loading the next map (map 2)

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    [SerializeField] private GameObject winNotification;  // Reference to the notification UI
    [SerializeField] private Button continueButton;       // Reference to the continue button
    [SerializeField] private PlayerMovement playerMovement; // Reference to player's movement script
    [SerializeField] private AudioSource winMusic;

    private bool hasWon = false;
    private void Start()
    {
        // Ensure notification and button are hidden at start
        winNotification.SetActive(false);
        continueButton.gameObject.SetActive(false);
        winMusic.gameObject.SetActive(false);
        // Attach the ContinueToNextMap method to the button's onClick event
        continueButton.onClick.AddListener(ContinueToNextMap);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!hasWon)
            {
                if (collision.transform.position.x < transform.position.x)
                    cam.MoveToNewRoom(nextRoom);
                else
                    cam.MoveToNewRoom(previousRoom);

                // If the player enters the final room of map 1 (assuming nextRoom is the last)
                if (nextRoom.CompareTag("FinalRoom"))
                {
                    ShowWinNotification();
                }
            }
        }
    }

    private void ShowWinNotification()
    {
        hasWon = true;  // Prevent multiple triggers
        winNotification.SetActive(true);  // Show the win message
        continueButton.gameObject.SetActive(true);  // Show the continue button

        playerMovement.enabled = false;
        winMusic.gameObject.SetActive(true);
        if (winMusic != null)
        {
            winMusic.Play();
        }
    }

    private void ContinueToNextMap()
    {
        winNotification.SetActive(false);
        continueButton.gameObject.SetActive(false);
        playerMovement.enabled = true;
    }


}
