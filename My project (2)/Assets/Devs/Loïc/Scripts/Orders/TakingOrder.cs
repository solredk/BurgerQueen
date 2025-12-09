using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TakingOrder : MonoBehaviour
{
    [SerializeField] private GameObject ordersTab;
    [SerializeField] private GameObject takeOrderButton;
    [SerializeField] private List<int> currentOrder;
    [SerializeField] private List<int> burger;
    [SerializeField] private List<int> frituur;
    [SerializeField] private List<int> ijs;
    [SerializeField] private List<int> drinks;


    public void TakeOrder()
    {
        takeOrderButton.SetActive(false);
        int orderBurger = Random.Range(0, drinks.Count);
        currentOrder.Add(orderBurger);
        int orderFrituur = Random.Range(0, drinks.Count);
        currentOrder.Add(orderFrituur);
        int orderIjs = Random.Range(0, drinks.Count);
        currentOrder.Add(orderIjs);
        int orderDrink = Random.Range(0, drinks.Count);
        currentOrder.Add(orderDrink);
    }

    public void OrdersTab()
    {
        bool closed = true;
        if (closed)
        {
            ordersTab.transform.position = new Vector3(17, -116, 0);
            closed = false;
        }
        else
        {
            ordersTab.transform.position = new Vector3(420, -116, 0);
            closed = true;
        }
    }

    void Update()
    {
        
    }
}
