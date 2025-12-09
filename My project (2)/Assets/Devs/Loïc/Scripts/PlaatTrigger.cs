using System;
using UnityEngine;

public class PlaatTrigger : MonoBehaviour
{
    private BurgerAssambleManager m_AssambleManager;
    public GameObject addedIngredient;

    private void Start()
    {
        m_AssambleManager = FindAnyObjectByType<BurgerAssambleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        addedIngredient = other.gameObject;
        GameObject burgerIng = Instantiate(addedIngredient, new Vector3(-3, 1.55f + m_AssambleManager.burger.Count, -10), Quaternion.identity);
        burgerIng.transform.localScale = new Vector3(0.3f, 0.06f, 0.3f);
        burgerIng.layer = default;
        burgerIng.GetComponent<Collider>().enabled = false;
        burgerIng.GetComponent<Rigidbody>().useGravity = false;
        m_AssambleManager.burger.Add(burgerIng);
        Destroy(addedIngredient);
    }
}