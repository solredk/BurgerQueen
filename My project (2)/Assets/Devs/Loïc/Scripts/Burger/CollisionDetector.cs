using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public bool occupied;
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Meat")
        {
            occupied = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        occupied = false;
    }
}
