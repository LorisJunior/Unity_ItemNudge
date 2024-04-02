using UnityEngine;

public class ItemNudge : MonoBehaviour
{
    // Speed item rotate
    public float rotationSpeed = 5f;

    //Item rotation direction
    private enum RotationDirection
    {
        right,
        left
    }

    // Default item rotation
    private Quaternion defaultRotation;

    // Get Player

    private PlayerController player = null;
    private float playerXPosition = 0f;


    private void Awake()
    {
        defaultRotation = transform.GetChild(0).rotation;
    }

    private void Update()
    {
        // Executes when player enter in the collision

        if (player)
        {
            float rotationRate = 20f * Time.deltaTime / (1 / rotationSpeed);

            if (playerXPosition < transform.position.x)
            {
                Rotate(rotationRate, RotationDirection.right);
            }
            else
            {
                Rotate(rotationRate, RotationDirection.left);
            }
        }

        // Executes when player exits
        // Check if the current rotation is different from the default rotation

        if (!player && !Mathf.Approximately(transform.GetChild(0).rotation.z, defaultRotation.z))
        {
            float rotationRate = 20f * Time.deltaTime / (1 / rotationSpeed);

            Quaternion currentRotation = transform.GetChild(0).rotation;
            transform.GetChild(0).rotation = Quaternion.RotateTowards(currentRotation, defaultRotation, rotationRate);
        }
    }

    // Rotate the item towards the player movement

    private void Rotate(float rotationRate, RotationDirection direction)
    {
        float z = 0f;

        switch (direction)
        {
            case RotationDirection.right:
                z = -20f;
                break;
            case RotationDirection.left:
                z = 20f;
                break;
        }

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, z);
        Quaternion currentRotation = transform.GetChild(0).rotation;

        // Check if the current rotation is different from the target rotation

        if (!Mathf.Approximately(currentRotation.z, targetRotation.z))
        {
            transform.GetChild(0).rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<PlayerController>();
        playerXPosition = player.transform.position.x;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        player = null;
    }

}
