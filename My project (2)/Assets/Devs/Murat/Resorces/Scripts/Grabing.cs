using UnityEngine;
using UnityEngine.InputSystem;

public class Grabing : MonoBehaviour
{
    [SerializeField] private GameObject Helditem;
    Vector3 MousePos;
    public LayerMask mask;
    Rigidbody rb;
    public float Zpos;
    private void Start()
    {

    }
    private void Update()
    {
        Vector2 mouse = Mouse.current.position.ReadValue();
        MousePos = new Vector3(mouse.x, mouse.y, Z) ;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        /*MousePos.z = 1f;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);*/
        Debug.DrawRay(MousePos, MousePos - Camera.main.transform.position, Color.red);
        if (Mouse.current.leftButton.IsPressed())
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, mask)) 
            {
                Helditem = hit.transform.gameObject;
            }
        }
        else if (Helditem != null)
        {
            rb.useGravity = true;
            Helditem = null;
        }
        if (Helditem != null)
        {
            rb = Helditem.GetComponent<Rigidbody>();
            rb.MovePosition(new Vector3(MousePos.x, MousePos.y, Helditem.transform.position.z));
            rb.useGravity = false;
        }

    }
    private void FixedUpdate()
    {

    }
}
