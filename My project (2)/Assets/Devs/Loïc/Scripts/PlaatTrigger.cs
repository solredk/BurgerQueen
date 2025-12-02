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
        GameObject burgerIng = Instantiate(addedIngredient, new Vector3(gameObject.transform.position.x, 1 + m_AssambleManager.burger.Count, -2), Quaternion.identity);
        burgerIng.layer = default;
        burgerIng.GetComponent<Collider>().enabled = false;
        m_AssambleManager.burger.Add(burgerIng);
        Destroy(addedIngredient);

      //  m_AssambleManager.IsTriggered();
    }
}
