using UnityEngine;
using TMPro;

public class Ingridient : MonoBehaviour
{
    [SerializeField] private string ingredientName;

    [SerializeField] private int addedQuantity;
    [SerializeField] private int quantity;
    [SerializeField] private int price;

    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI priceText;

    private void Start()
    {
        StartSetting();
    }

    private void StartSetting()
    {
        price = IngredientManager.instance.GetPrice(ingredientName);
        quantity = IngredientManager.instance.GetAmount(ingredientName);
        quantityText.text = quantity.ToString();
        priceText.text = "€" + price.ToString();
    }

    public void GiveAmount() 
    {
        IngredientManager.instance.GiveAmount(ingredientName, addedQuantity);
        quantity = IngredientManager.instance.GetAmount(ingredientName);
        quantityText.text = quantity.ToString();
    }

}
