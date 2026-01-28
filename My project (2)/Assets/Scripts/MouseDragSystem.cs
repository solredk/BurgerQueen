using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MouseDragSystem : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent OnObjectGrabbed;
    public UnityEvent OnObjectReleased;

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
                    GameObject targetObject = hit.collider.gameObject;

                    // Check if object can be grabbed
                    OldOilCan oldCan = targetObject.GetComponent<OldOilCan>();
                    if (oldCan != null && !oldCan.CanBeGrabbed())
                    {
                        return;
                    }

                    NewOilCan newCan = targetObject.GetComponent<NewOilCan>();
                    if (newCan != null && !newCan.CanBeGrabbed())
                    {
                        return;
                    }

                    // Start dragging
                    m_Dragging = true;
                    m_DraggedObject = targetObject;
                    m_DraggedRigidbody = m_DraggedObject.GetComponent<Rigidbody>();

                    // Notify object it's being held
                    if (oldCan != null) oldCan.SetHeld(true);
                    if (newCan != null) newCan.SetHeld(true);

                    // Fire grab event
                    OnObjectGrabbed.Invoke();
                }
            }
        }
        else if (context.canceled)
        {
            if (m_Dragging)
            {
                // Notify object it's no longer held
                if (m_DraggedObject != null)
                {
                    OldOilCan oldCan = m_DraggedObject.GetComponent<OldOilCan>();
                    if (oldCan != null) oldCan.SetHeld(false);

                    NewOilCan newCan = m_DraggedObject.GetComponent<NewOilCan>();
                    if (newCan != null) newCan.SetHeld(false);
                }

                // Fire release event
                OnObjectReleased.Invoke();

                m_Dragging = false;
                m_DraggedObject = null;
                m_DraggedRigidbody = null;
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