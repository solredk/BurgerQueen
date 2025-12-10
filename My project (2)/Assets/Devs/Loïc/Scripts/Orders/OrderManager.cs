using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private GameObject ordersTab;
    [SerializeField] private GameObject takeOrderButton;
    [SerializeField] private List<int> currentOrder;
    // [SerializeField] private List<string> allOrder;
    [SerializeField] private List<int> burger;
    [SerializeField] private List<int> frituur;
    [SerializeField] private List<int> drinks;
    [SerializeField] private List<Customer> allOrders;

    private bool closed = true;


    private void Start()
    {
        ordersTab.transform.position = new Vector3(Screen.width / 50, Screen.height / 1.5f, 0);
    }

    private void AddCustomerOrder(List<int> order)
    {
        allOrders.Add(
            new Customer()
            {
                Order = order
            }
        );

    }

    public void TakeOrder()
    {
        //takeOrderButton.SetActive(false);
        currentOrder.Clear();

        int orderBurger = UnityEngine.Random.Range(0, burger.Count);
        currentOrder.Add(orderBurger);
        int orderFrituur = UnityEngine.Random.Range(0, frituur.Count);
        currentOrder.Add(orderFrituur);
        int orderDrink = UnityEngine.Random.Range(0, drinks.Count);
        currentOrder.Add(orderDrink);

        AddCustomerOrder(new List<int>() { currentOrder[0], currentOrder[1], currentOrder[2] });

    }

    public void CompareOrder()
    {
        print(allOrders[0].Order[0]);
        print(allOrders[0].Order[1]);
        print(allOrders[0].Order[2]);

    }

    public void OrdersTab()
    {
        if (closed)
        {
            ordersTab.transform.position = new Vector3(Screen.width / 2, Screen.height / 1.5f, 0);
            closed = false;
        }
        else
        {
            ordersTab.transform.position = new Vector3(Screen.width / 50, Screen.height / 1.5f, 0);
            closed = true;
        }
    }

    [Serializable]
    public struct Customer
    {
        public List<int> Order;
    }
}
