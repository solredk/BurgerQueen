using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDragSystem : MonoBehaviour
{
    private bool m_Dragging = false;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void ClickPerformed(InputAction.CallbackContext context)
    {
        // Check if hit THIS object
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                m_Dragging = true;
                Debug.Log("Context True");
            }
        }
    }

    public void ClickReleased(InputAction.CallbackContext context)
    {
        m_Dragging = false;
    }

    private void Update()
    {
        if (m_Dragging)
        {
            // Drag to mouse position
            Vector3 screen = Mouse.current.position.ReadValue();
            Vector3 world = cam.ScreenToWorldPoint(new Vector3(screen.x, screen.y, cam.WorldToScreenPoint(transform.position).z));
            transform.position = world;
        }
    }
}
