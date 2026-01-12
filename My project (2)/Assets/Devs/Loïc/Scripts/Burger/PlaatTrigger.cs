using System;
using UnityEngine;

public class PlaatTrigger : MonoBehaviour
{
    private BurgerAssambleManager m_AssambleManager;
    public GameObject addedIngredient;
    [SerializeField] float ingredientDistance;
    private bool ingredientPlaced;

    private void Start()
    {
        m_AssambleManager = FindAnyObjectByType<BurgerAssambleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        addedIngredient = other.gameObject;
        GameObject burgerIng = Instantiate(addedIngredient, new Vector3(gameObject.transform.position.x, m_AssambleManager.burger.Count / ingredientDistance + 1.55f, gameObject.transform.position.z), Quaternion.identity);
        burgerIng.transform.localScale = burgerIng.transform.localScale / 10;
        burgerIng.layer = default;
        burgerIng.GetComponent<Collider>().enabled = false;
        burgerIng.GetComponent<Rigidbody>().useGravity = false;
        m_AssambleManager.burger.Add(burgerIng);
        Destroy(addedIngredient);
    }
}