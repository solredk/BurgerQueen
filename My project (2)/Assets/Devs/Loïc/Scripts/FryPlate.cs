using UnityEngine;

public class FryPlate : MonoBehaviour
{
    public OrderManager m_OrderManager;
    public GameObject currentFry;

    private void Start()
    {
        m_OrderManager = FindAnyObjectByType<OrderManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DoneFries")
        {
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            currentFry = other.gameObject;
        }
        else if (other.gameObject.tag == "DoneNugget")
        {
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            currentFry = other.gameObject;
        }
        else if (other.gameObject.tag == "DoneOnions")
        {
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            currentFry = other.gameObject;
        }
    }

    public void GetFry()
    {
        if (currentFry.gameObject.tag == "DoneFries")
        {
            m_OrderManager.thisOrder[1] = 0;
            m_OrderManager.currentFritT.text = "Fry: Fries";
            Destroy(currentFry.gameObject);
        }
        if (currentFry.gameObject.tag == "DoneNugget")
        {
            m_OrderManager.thisOrder[1] = 1;
            m_OrderManager.currentFritT.text = "Fry: Nuggets";
            Destroy(currentFry.gameObject);
        }
        if (currentFry.gameObject.tag == "DoneOnions")
        {
            m_OrderManager.thisOrder[1] = 2;
            m_OrderManager.currentFritT.text = "Fry: Onions";
            Destroy(currentFry.gameObject);
        }
    }
}
