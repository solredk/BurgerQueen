using UnityEngine;

public class Fryingstation : MonoBehaviour
{
    public Frituur frituur;
    [SerializeField] private float cookingTime = 10.0f;
    private bool isCooking = false;

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
                frituur.handEmpty = false;
                frituur.currentIngredient = Instantiate(frituur.friedFriesobj, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity, frituur.transform);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fries") && !isCooking)
        {
            Destroy(collision.gameObject);
            frituur.handEmpty = true;
            isCooking = true;
        }
    }
}
