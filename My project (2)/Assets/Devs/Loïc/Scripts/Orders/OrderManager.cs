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

    public List<int> currentOrder;
    [SerializeField] private List<Customer> allOrders;
    public List<int> thisOrder;
    [SerializeField] private List<GameObject> orderCards;
    [SerializeField] private List<Transform> cardPositions;

    [SerializeField] private List<GameObject> burgUi;
    [SerializeField] private List<GameObject> fryUi;
    [SerializeField] private List<GameObject> drinkUi;

    [SerializeField] private List<int> burger;
    [SerializeField] private List<int> frituur;
    [SerializeField] private List<int> drinks;

    public bool orderGivePoints = false;
    public bool orderGiveReverse = false;
    private bool closed = true;
    private bool spotfilled;

    public int currentBurg;
    public int currentFrit;
    public int currentDrink;

    public TextMeshProUGUI currentBurgT;
    public TextMeshProUGUI currentFritT;
    public TextMeshProUGUI currentDrinkT;

    public TextMeshProUGUI wrongText;

    public int served;

    public List<GameObject> costumers;
    public GameObject costumerSpawn;
    private GameObject currentCost;


    private void Start()
    {
        ordersTab.transform.position = new Vector3(Screen.width / 50, Screen.height / 1.5f, 0);
        // currentCost = Instantiate(costumers[0], costumerSpawn.transform);
        costumers[0].SetActive(true);
        costumers[1].SetActive(false);
        costumers[2].SetActive(false);

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
        takeOrderButton.SetActive(false);
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
                Instantiate(burgUi[orderBurger], currentCard.transform.GetChild(1).gameObject.transform);
                Instantiate(fryUi[orderFrituur], currentCard.transform.GetChild(2).gameObject.transform);
                Instantiate(drinkUi[orderDrink], currentCard.transform.GetChild(3).gameObject.transform);

                spotfilled = true;
            }
        }

        AddCustomerOrder(new List<int>() { currentOrder[0], currentOrder[1], currentOrder[2] });
    }

    public void CompareOrder()
    {
        //string orderNumberS = orderNumberGO.text;
        //int orderNumberI = 0;
        //int.TryParse(orderNumberS, out orderNumberI);

        for (int i = 0; i < 3; i++)
        {

            if (allOrders[0].Order[i] == thisOrder[i])
            {
                orderGiveReverse = true;
                currentBurgT.text = "Burger: ";
                currentDrinkT.text = "Drink: ";
                currentFritT.text = "Fry: ";
                thisOrder[i] = 0;
                served++;
                print("YAAAHSSS");
                print(served);
            }
            else
            {
                WrongOrder();

            }
        }
        takeOrderButton.SetActive(true);
       // currentCost.SetActive(false);
        //currentCost = Instantiate(costumers[UnityEngine.Random.Range(0, costumers.Count)], costumerSpawn.transform);

        currentOrder.Clear();
        allOrders.Clear();

        costumers[0].SetActive(false);
        costumers[1].SetActive(false);
        costumers[2].SetActive(false);
        int cosNum = UnityEngine.Random.Range(0, 3);
        costumers[cosNum].SetActive(true);

        Destroy(orderCards[0]);

        thisOrder[0] = 0;
        thisOrder[1] = 0;
        thisOrder[2] = 0;


        if (served >= 9) 
        {
            //HIER LOAD EINDE SHIFT SCREEN / SCORE SCENE
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
