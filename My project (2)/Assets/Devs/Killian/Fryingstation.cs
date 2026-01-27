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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
                if(frituur.ingredientSpawned2)
                {
                    frituur.ingredientSpawned2 = false;
                }
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
