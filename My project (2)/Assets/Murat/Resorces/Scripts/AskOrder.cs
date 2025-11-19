using UnityEngine;
using UnityEngine.UI;

public class AskOrder : MonoBehaviour
{
    Rejester Rejester;
    int Toppings = 0;
    Image upperBun;
    Image meat;
    Image chees;
    Image lowerBun;
    void Start()
    {
        Rejester = FindObjectOfType<Rejester>();
        ChoseOrder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChoseOrder()
    {
        Toppings = Random.Range(0, 2);

    }
}
