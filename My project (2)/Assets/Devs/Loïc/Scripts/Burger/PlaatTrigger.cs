using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlaatTrigger : MonoBehaviour
{
    private BurgerAssambleManager m_AssambleManager;
    public GameObject addedIngredient;
    [SerializeField] float ingredientDistance;
    public List<GameObject> burger;

    private void Start()
    {
        m_AssambleManager = FindAnyObjectByType<BurgerAssambleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        addedIngredient = other.gameObject;
        GameObject burgerIng = Instantiate(addedIngredient, new Vector3(gameObject.transform.position.x, m_AssambleManager.burger.Count / ingredientDistance + 1.55f, gameObject.transform.position.z), Quaternion.identity);
       // burgerIng.transform.localScale = burgerIng.transform.localScale / 5;
        burgerIng.layer = default;
        burgerIng.GetComponent<Collider>().enabled = false;
        burgerIng.GetComponent<Rigidbody>().useGravity = false;
        m_AssambleManager.burger.Add(burgerIng);
        Destroy(addedIngredient);
        burger.Add(burgerIng);
    }

    public void doesBrunoMarsIsGay()
    {
        if (burger[0].tag == "bread" && burger[0].tag == "lettuce" && burger[2].tag == "bread")
        {
            print("Megan");
        }
        else
        {
            print("You flippin twit");
        }

    }

    public void ClearButton()
    {
        for (int i = 0; i < burger.Count; i++)
        {
            Destroy(burger[i]);
        }
        burger.Clear();
        m_AssambleManager.ingedientAmount = 0;
    }
}