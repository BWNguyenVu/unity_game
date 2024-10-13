using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Make sure to include this for UI elements

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    // Reference to the UI Panel
    [SerializeField] private GameObject gameOverPanel; // Drag your panel here in the Inspector
    [SerializeField] private Button continueButton; // Reference to the continue button

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        gameOverPanel.SetActive(false); // Make sure it's hidden at the start
        continueButton.gameObject.SetActive(false); // Ensure the continue button is hidden at the start
        continueButton.onClick.AddListener(PlayAgain); // Set up the button to call PlayAgain
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            // iframes
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                ShowGameOverPanel(); // Call to show the panel
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private void ShowGameOverPanel()
    {
        continueButton.gameObject.SetActive(true); // Show the continue button
        gameOverPanel.SetActive(true); // Show the panel when player dies
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }
}
