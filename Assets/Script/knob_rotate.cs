using UnityEngine;

public class KnobRotate : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 initialMousePosition;
    private float initialAngle;
    private float rotationSpeed = 25.0f;  // You can adjust this value to control rotation sensitivity

    void Update()
    {
        // Get the current mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check for mouse button press
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over the knob
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject && targetObject.gameObject == gameObject)
            {
                isDragging = true;
                initialMousePosition = mousePosition;
                initialAngle = transform.rotation.eulerAngles.z;
                if (initialAngle > 180) initialAngle -= 360;  // Convert to range -180 to 180
            }
        }

        // Check for mouse button release
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // If the knob is being dragged
        if (isDragging)
        {
            // Calculate the vertical movement
            float deltaY = mousePosition.y - initialMousePosition.y;

            // Adjust the angle based on the vertical movement
            float angle = initialAngle + deltaY * rotationSpeed;

            // Clamp the angle to be within -90 and 90 degrees
            angle = Mathf.Clamp(angle, -90.0f, 90.0f);

            // Apply the rotation
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
