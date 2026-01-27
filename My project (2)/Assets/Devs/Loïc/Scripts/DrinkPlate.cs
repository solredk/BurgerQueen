using UnityEngine;

public class DrinkPlate : MonoBehaviour
{
    public OrderManager m_OrderManager;
    public GameObject currentDrink;

    private void Start()
    {
        m_OrderManager = FindAnyObjectByType<OrderManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ColaDietDr")
        {
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            currentDrink = other.gameObject;
        }
        else if (other.gameObject.tag == "OrangeDr")
        {
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            currentDrink = other.gameObject;
        }
        else if (other.gameObject.tag == "JoltDr")
        {
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            currentDrink = other.gameObject;
        }
        else if (other.gameObject.tag == "LemonDr")
        {
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            currentDrink = other.gameObject;
        }
        if (other.gameObject.tag == "ColaDr")
        {
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            currentDrink = other.gameObject;
        }
        
        else if (other.gameObject.tag == "BananaIce")
        {
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            currentDrink = other.gameObject;
        }
        else if (other.gameObject.tag == "ChocoIce")
        {
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            currentDrink = other.gameObject;
        }
        else if (other.gameObject.tag == "StrawIce")
        {
            other.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.2f, gameObject.transform.position.z);
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            currentDrink = other.gameObject;
        }
    }

    public void GetFry()
    {
        if (currentDrink.gameObject.tag == "ColaDr")
        {
            m_OrderManager.thisOrder[1] = 0;
            m_OrderManager.currentFritT.text = "Fry: Fries";
            Destroy(currentDrink.gameObject);
        }
        if (currentDrink.gameObject.tag == "DoneNugget")
        {
            m_OrderManager.thisOrder[1] = 1;
            m_OrderManager.currentFritT.text = "Fry: Nuggets";
            Destroy(currentDrink.gameObject);
        }
        if (currentDrink.gameObject.tag == "DoneOnions")
        {
            m_OrderManager.thisOrder[1] = 2;
            m_OrderManager.currentFritT.text = "Fry: Onions";
            Destroy(currentDrink.gameObject);
        }
    }
}
