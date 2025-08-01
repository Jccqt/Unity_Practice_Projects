using UnityEngine;

public class Rotator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Rotate the object on X, Y, Z axes by specified amounts, adjusted per frame rate.
        transform.Rotate(new Vector3 (15, 30, 45) * Time.deltaTime);
    }
}
