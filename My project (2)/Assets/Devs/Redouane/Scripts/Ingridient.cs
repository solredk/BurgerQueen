using UnityEngine;
using TMPro;

public class Ingridient : MonoBehaviour
{
    [SerializeField] private string ingredientName;

    [SerializeField] private int addedQuantity;
    [SerializeField] private int quantity;
    [SerializeField] private int price;

    [SerializeField] private bool hasPrice;

    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI priceText;

    private void Start()
    {
        StartSetting();
    }

    private void StartSetting()
    {
        price = IngredientManager.instance.GetPrice(ingredientName);

        if (!hasPrice)
        {
            priceText.gameObject.SetActive(false);
            quantity = IngredientManager.instance.GetAmount(ingredientName);
        }
        else         
        {
            priceText.text = "€" + price.ToString();
        }
        quantityText.text = quantity.ToString();
    }

    public void GiveAmount() 
    {
        IngredientManager.instance.GiveAmount(ingredientName, addedQuantity);
        quantity = IngredientManager.instance.GetAmount(ingredientName);
        quantityText.text = quantity.ToString();
    }

}
