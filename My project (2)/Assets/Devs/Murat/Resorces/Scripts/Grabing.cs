using UnityEngine;
using UnityEngine.InputSystem;

public class Grabing : MonoBehaviour
{
    public GameObject Helditem;
    Vector3 MousePos;
    public LayerMask mask;
    [SerializeField] private Rigidbody rb;
    public float Zpos;
    RaycastHit hit;
    //public Plate BurgerPlate;
    private void Start()
    {

    }
    private void Update()
    {
        Vector2 mouse = Mouse.current.position.ReadValue();
        MousePos = new Vector3(mouse.x, mouse.y, Zpos);
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        Debug.DrawRay(Camera.main.transform.position, MousePos - Camera.main.transform.position, Color.red);
        if (Mouse.current.leftButton.IsPressed())
        {
            if (Helditem == null)
            {
                PickUp();
                Debug.Log("Pickup");
            }
        }else if (Helditem != null)
        {
            Drop();
            Debug.Log("Drop");
        }

        if (Helditem != null)
        {
            Move();
            Debug.Log("Move");
        }
    }
    void PickUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit, 100, mask) && Helditem == null)
        {
            Helditem = hit.transform.gameObject;
            rb = Helditem.GetComponent<Rigidbody>();
            
            if (rb)
            {
                rb.useGravity=false;
                rb.linearDamping=10;
                rb.angularDamping=10;
            }
        }
    }
    void Drop()
    {
        rb.useGravity = true;
        rb.linearDamping = 0;
        rb.angularDamping = 0;
        rb = null;
        Helditem = null;
    }
    void Move()
    {
        Vector3 direction = new Vector3(MousePos.x, MousePos.y, Helditem.transform.position.z); 
        rb.MovePosition(direction);
    }
    /*

*/
}
