using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    private Vector3 offset; // The distance between the camera and the player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Calculate the initial offset between the camera's position and player's position
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called once per frame after all Update functions have been completed
    void LateUpdate()
    {
        // Maintain the same offset between the camera and player throughout the game
        transform.position = player.transform.position + offset;
    }

}
