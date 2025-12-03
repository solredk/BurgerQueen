using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDragSystem : MonoBehaviour
{
    private bool m_Dragging = false;
    private Camera cam;
    private GameObject m_DraggedObject;
    private Rigidbody m_DraggedRigidbody;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void ClickPerformed(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            Ray ray = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("Grabable"))
                {
                    m_Dragging = true;
                    m_DraggedObject = hit.collider.gameObject;
                    m_DraggedRigidbody = m_DraggedObject.GetComponent<Rigidbody>();
                }
            }
        }
        else if (context.canceled)
        {
            if (m_Dragging)
            {
                m_Dragging = false;

                if (m_DraggedObject != null)
                {
                    m_DraggedObject = null;
                    m_DraggedRigidbody = null;
                }
            }
        }
    }

    public void ClickReleased(InputAction.CallbackContext context)
    {
    }

    private void Update()
    {
        if (m_Dragging && m_DraggedObject != null)
        {
            Vector3 screen = Mouse.current.position.ReadValue();
            Vector3 targetWorld = cam.ScreenToWorldPoint(new Vector3(screen.x, screen.y, cam.WorldToScreenPoint(m_DraggedObject.transform.position).z));

            if (m_DraggedRigidbody != null)
            {
                m_DraggedRigidbody.MovePosition(targetWorld);
            }
            else
            {
                m_DraggedObject.transform.position = targetWorld;
            }
        }
    }
}