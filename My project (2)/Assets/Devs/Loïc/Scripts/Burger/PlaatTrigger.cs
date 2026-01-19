using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlaatTrigger : MonoBehaviour
{
    private BurgerAssambleManager m_AssambleManager;
    public GameObject addedIngredient;
    [SerializeField] float ingredientDistance;
    [SerializeField] float sauseDistance;

    [SerializeField] private GameObject ketchupSplat;
    [SerializeField] private GameObject mayoSplat;
    [SerializeField] private GameObject mustSplat;


    private void Start()
    {
        m_AssambleManager = FindAnyObjectByType<BurgerAssambleManager>();
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
        if (m_AssambleManager.burger[0].gameObject.tag == ("bread"))
        {
            if (m_AssambleManager.burger[1].gameObject.tag == ("Meat") && m_AssambleManager.burger[2].gameObject.tag == ("ketchup") && m_AssambleManager.burger[3].gameObject.tag == ("mustord") && m_AssambleManager.burger[4].gameObject.tag == ("bread"))
            {
                print("you basic ass bitch");
            }
            else
            {
                print("fake ass burger");
            }
        }
        else 
        {
            print("neit eens een burger wtihowea");
        }


        
    }

    public void ClearButton()
    {
        for (int i = 0; i < m_AssambleManager.burger.Count; i++)
        {
            Destroy(m_AssambleManager.burger[i]);
        }
        m_AssambleManager.burger.Clear();
        m_AssambleManager.ingedientAmount = 0;
    }
}