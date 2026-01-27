using UnityEngine;

public class Fryingstation : MonoBehaviour
{
    public Frituur frituur;
    [SerializeField] private float cookingTime = 10.0f;
    private bool isCooking = false;
    [SerializeField] private GameObject friedFriesSpawn;

    private void Awake()
    {
        frituur = FindFirstObjectByType<Frituur>();
    }
    void Update()
    {
        if(isCooking)
        {
            cookingTime -= Time.deltaTime;
            if(cookingTime <= 0)
            {
                isCooking = false;
                cookingTime = 10.0f;
                frituur.ingredientSpawned = false;
                frituur.currentIngredient = Instantiate(frituur.CookingIngredient, friedFriesSpawn.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FryingIngredient")&& !isCooking)
        {
            Destroy(collision.gameObject);
            frituur.ingredientSpawned = false;
            isCooking = true;
        }
    }
}
