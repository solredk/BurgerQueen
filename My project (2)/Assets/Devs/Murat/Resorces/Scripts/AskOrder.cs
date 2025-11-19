using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AskOrder : MonoBehaviour
{
    Rejester Rejester;
    int Toppings = 0;
    public Image upperBun;
    public List<Image> toppings;
    public Image lowerBun;
    void Start()
    {
        Rejester = FindObjectOfType<Rejester>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChoseOrder()
    {
        Rejester.Burger.Clear();
        Toppings = Random.Range(1, 4);
        Rejester.Burger.Add(lowerBun);
        for (int i = 0; i < Toppings; i++)
        {
            int chose = Random.Range(0, 3);
            Rejester.Burger.Add(toppings[chose]);
        }
        Rejester.Burger.Add(upperBun);
        Rejester.PutOnComp();
    }
}
