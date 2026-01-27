using UnityEngine;

public class Add : MonoBehaviour
{
        public IngredientPurchaseUI IPI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        IPI.AddMoney(100);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
