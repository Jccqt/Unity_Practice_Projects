using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player
    private Rigidbody rigidBody;

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Movement of the player along X and Y axes
    private float movementX;
    private float movementY;

    // Movement speed of the player
    public float speed = 0;

    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;

    // UI object to display current enemy speed
    public GameObject enemySpeedText;

    // UI object to display winning text.
    public GameObject winTextObject;

    public GameObject enemy;
    public float enemySpeed = 2.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get and store the Rigidbody component attached to the player
        rigidBody = GetComponent<Rigidbody>();

        // Initialize count to zero.
        count = 0;

        // Update the count display
        SetCountText();

        // Initially set the win text to be inactive (Not visible)
        winTextObject.SetActive(false);

        // Initially set the enemy speed text to be inactive (Not visible)
        enemySpeedText.SetActive(false);
    }

    // FixedUpdate is called once per fixed frame-rate frame
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rigidBody.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate or disappear the object
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    // OnMove is called when a move input is detected
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        // Increases the enemy speed every time the player score increases
        NavMeshAgent enemyAgent = enemy.GetComponent<NavMeshAgent>();
        enemySpeed += 1.0f;
        enemyAgent.speed = enemySpeed;

        countText.text = "Count: " + count.ToString();
        enemySpeedText.SetActive(true);
        enemySpeedText.GetComponent<TextMeshProUGUI>().text = "Enemy is getting faster! Enemy speed: " + enemySpeed.ToString();

        if(count >= 11)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            winTextObject.SetActive(true);
            enemySpeedText.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && count != 11)
        {
            // Destroy the player
            Destroy(gameObject);

            // Update the winText to display "You Lose!";
            winTextObject.gameObject.SetActive(true);
            enemySpeedText.SetActive(false);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

}
