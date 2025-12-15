using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private GameObject ordersTab;
    [SerializeField] private GameObject takeOrderButton;
    [SerializeField] private TMP_InputField orderNumberGO;

    [SerializeField] private List<int> currentOrder;
    [SerializeField] private List<Customer> allOrders;
    [SerializeField] private List<int> thisOrder;

    [SerializeField] private List<int> burger;
    [SerializeField] private List<int> frituur;
    [SerializeField] private List<int> drinks;

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
        string orderNumberS = orderNumberGO.text;
        int orderNumberI = 0;
        int.TryParse(orderNumberS, out orderNumberI);

        for (int i = 0; i < 3; i++)
        {
            //thisOrder.Add(allOrders[orderNumberI].Order[i]);
          //  thisOrder[0] = 4;

            if (allOrders[orderNumberI].Order[i] == thisOrder[i])
            {
                print("nice soup");
            }
            else
            {
                print("I ASKED FOR NO PICKLES");
            }
        }
    
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
