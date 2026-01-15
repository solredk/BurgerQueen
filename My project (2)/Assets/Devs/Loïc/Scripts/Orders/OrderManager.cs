using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private GameObject ordersTab;
    [SerializeField] private GameObject takeOrderButton;
    [SerializeField] private TMP_InputField orderNumberGO;
    [SerializeField] private GameObject orderCard;

    [SerializeField] private List<int> currentOrder;
    [SerializeField] private List<Customer> allOrders;
    [SerializeField] private List<int> thisOrder;
    [SerializeField] private List<GameObject> orderCards;
    [SerializeField] private List<Transform> cardPositions;

    [SerializeField] private List<int> burger;
    [SerializeField] private List<int> frituur;
    [SerializeField] private List<int> drinks;

    public bool orderGivePoints = false;
    public bool orderGiveReverse = false;
    private bool closed = true;
    private bool spotfilled;


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

        //currentCard.transform.SetParent(ordersTab.transform);
        spotfilled = false;
        for (int i = 0; i < 4; i++)
        {
            if (orderCards[i] == null && spotfilled == false)
            {
                GameObject currentCard = Instantiate(orderCard, cardPositions[i]);
                orderCards.Insert(i, currentCard);
                currentCard.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = orderBurger.ToString();
                currentCard.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = orderFrituur.ToString();
                currentCard.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = orderDrink.ToString();

                spotfilled = true;
            }

            //  Vector3 cardPosition = currentCard.transform.position += ordersTab.transform.position;
            // cardPosition.x = currentCard.transform.position.x - ordersTab.transform.position.x * 5 * orderCards.Count + 1;
            // currentCard.transform.position = cardPosition;
        }

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
                orderGiveReverse = true;
            }
            else
            {
                print("I ASKED FOR NO PICKLES");
                WrongOrder();

            }
        }

    }

    private void WrongOrder() // wanneer een foute order ingeleverd word
    {
        orderGivePoints = true; //public bool stuurt door naar een ander script
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
