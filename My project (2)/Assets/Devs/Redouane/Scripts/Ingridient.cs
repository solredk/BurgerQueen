using UnityEngine;
using TMPro;

public class Ingridient : MonoBehaviour
{
    [SerializeField] private string ingredientName;
    [SerializeField] private int addedQuantity;
    [SerializeField] private int quantity;
    [SerializeField] private TextMeshProUGUI quantityText;
    private void Awake()
    {
        quantity = IngredientManager.instance.GetAmount(ingredientName);
        quantityText.text = quantity.ToString();
    }


    public void GiveAmount() 
    {
        IngredientManager.instance.GiveAmount(ingredientName, addedQuantity);
        quantity = IngredientManager.instance.GetAmount(ingredientName);
        quantityText.text = quantity.ToString();
    }

}
