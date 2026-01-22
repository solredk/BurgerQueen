using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlaatTrigger : MonoBehaviour
{
    private BurgerAssambleManager m_AssambleManager;
    private OrderManager m_OrderManager;
    private GameObject addedIngredient;
    [SerializeField] private List<GameObject> burger0;
    [SerializeField] private List<GameObject> burger1;
    [SerializeField] private List<GameObject> burger2;
    [SerializeField] private List<GameObject> burger3;
    [SerializeField] private List<GameObject> burger4;

    [SerializeField] float ingredientDistance;
    [SerializeField] float sauseDistance;

    [SerializeField] private GameObject ketchupSplat;
    [SerializeField] private GameObject mayoSplat;
    [SerializeField] private GameObject mustSplat;

    [SerializeField] private TextMeshProUGUI noBurgT;



    private void Start()
    {
        m_AssambleManager = FindAnyObjectByType<BurgerAssambleManager>();
        m_OrderManager = FindAnyObjectByType<OrderManager>();
        noBurgT.text = string.Empty;
        noBurgT.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        addedIngredient = other.gameObject;
        GameObject burgerIng = Instantiate(addedIngredient, new Vector3(gameObject.transform.position.x, m_AssambleManager.burger.Count / ingredientDistance + 1.55f, gameObject.transform.position.z), Quaternion.identity);
        burgerIng.layer = default;
        burgerIng.GetComponent<Collider>().enabled = false;
        burgerIng.GetComponent<Rigidbody>().useGravity = false;
        m_AssambleManager.burger.Add(burgerIng);
        Destroy(addedIngredient);
    }
    public void AddKetchup()
    {
        GameObject burgerIng = Instantiate(ketchupSplat, new Vector3(gameObject.transform.position.x, m_AssambleManager.burger.Count / sauseDistance + 1.55f, gameObject.transform.position.z), Quaternion.identity);
        m_AssambleManager.burger.Add(burgerIng);
    }
    public void AddMayo()
    {
        GameObject burgerIng = Instantiate(mayoSplat, new Vector3(gameObject.transform.position.x, m_AssambleManager.burger.Count / sauseDistance + 1.55f, gameObject.transform.position.z), Quaternion.identity);
        m_AssambleManager.burger.Add(burgerIng);
    }
    public void AddMust()
    {
        GameObject burgerIng = Instantiate(mustSplat, new Vector3(gameObject.transform.position.x, m_AssambleManager.burger.Count / sauseDistance + 1.55f, gameObject.transform.position.z), Quaternion.identity);
        m_AssambleManager.burger.Add(burgerIng);
    }

    public void doesBrunoMarsIsGay()
    {
        if (m_OrderManager.currentOrder[0] == 0)
        {
            for (int i = 0; i < burger0.Count; i++)
            {
                if (m_AssambleManager.burger[i].gameObject.tag == burger0[i].gameObject.tag)
                {
                    m_OrderManager.thisOrder[0] = 0;
                    noBurgT.text = string.Empty;
                    m_OrderManager.currentBurgT.text = "Burger: Basic";
                }
                else
                {
                    noBurgT.text = "Not a valid burger";
                }
            }
        }

        if (m_OrderManager.currentOrder[0] == 1)
        {
            for (int i = 0; i < burger1.Count; i++)
            {
                if (m_AssambleManager.burger[i].gameObject.tag == burger1[i].gameObject.tag)
                {
                    m_OrderManager.thisOrder[0] = 1;
                    noBurgT.text = string.Empty;
                    m_OrderManager.currentBurgT.text = "Burger: Deluxe";
                }
                else
                {
                    noBurgT.text = "Not a valid burger";
                }
            } 
        }

        if (m_OrderManager.currentOrder[0] == 2)
        {
            for (int i = 0; i < burger2.Count; i++)
            {
                if (m_AssambleManager.burger[i].gameObject.tag == burger2[i].gameObject.tag)
                {
                    m_OrderManager.thisOrder[0] = 2;
                    noBurgT.text = string.Empty;
                    m_OrderManager.currentBurgT.text = "Burger: Deluxe Cheese";
                }
                else
                {
                    noBurgT.text = "Not a valid burger";
                }
            }
        }
        if (m_OrderManager.currentOrder[0] == 3)
        {
            for (int i = 0; i < burger3.Count; i++)
            {
                if (m_AssambleManager.burger[i].gameObject.tag == burger3[i].gameObject.tag)
                {
                    m_OrderManager.thisOrder[0] = 3;
                    noBurgT.text = string.Empty;
                    m_OrderManager.currentBurgT.text = "Burger: Cheese burger";
                }
                else
                {
                    noBurgT.text = "Not a valid burger";
                }
            }
        }
        if (m_OrderManager.currentOrder[0] == 4)
        {
            for (int i = 0; i < burger4.Count; i++)
            {
                if (m_AssambleManager.burger[i].gameObject.tag == burger4[i].gameObject.tag)
                {
                    m_OrderManager.thisOrder[0] = 4;
                    noBurgT.text = string.Empty;
                    m_OrderManager.currentBurgT.text = "Burger: Kaasbroodje";
                }
                else
                {
                    noBurgT.text = "Not a valid burger";
                }
            }
        }    
    }

    public void ClearButton()
    {
        noBurgT.text = string.Empty;
        for (int i = 0; i < m_AssambleManager.burger.Count; i++)
        {
            Destroy(m_AssambleManager.burger[i]);
        }
        m_AssambleManager.burger.Clear();
        m_AssambleManager.ingedientAmount = 0;
    }
}