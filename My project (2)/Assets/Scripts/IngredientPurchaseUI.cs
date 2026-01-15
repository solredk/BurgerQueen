using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IngredientPurchaseUI : MonoBehaviour
{
    [Header("UI")]
    public List<Text> quantityTexts;
    public List<string> ingredientNames;

    [Header("Money")]
    public static int playerMoney = 1000;
    public Text moneyText;
    public int costPerItem = 10;

    private void Start()
    {
        UpdateAllUI();
    }

    public void PurchaseByIndex(int index)
    {
        if (index < 0 || index >= ingredientNames.Count)
            return;

        string ingredientName = ingredientNames[index];
        int currentQuantity = IngredientManager.instance.GetAmount(ingredientName);
        int maxQuantity = GetMaxQuantity(ingredientName);

        if (currentQuantity >= maxQuantity)
            return;

        if (playerMoney >= costPerItem)
        {
            playerMoney -= costPerItem;
            IngredientManager.instance.GiveAmount(ingredientName, 1);
            UpdateAllUI();
        }
    }

    public int GetMaxQuantity(string ingredientName)
    {
        foreach (Ingredient ingredient in IngredientManager.instance.ingredients)
        {
            if (ingredient.Name == ingredientName)
            {
                return ingredient.MaxQuantity;
            }
        }
        return 45;
    }

    public void UpdateAllUI()
    {
        for (int i = 0; i < ingredientNames.Count; i++)
        {
            if (i >= quantityTexts.Count)
                continue;

            string ingredientName = ingredientNames[i];
            int quantity = IngredientManager.instance.GetAmount(ingredientName);
            int maxQuantity = GetMaxQuantity(ingredientName);
            Text text = quantityTexts[i];

            if (quantity >= maxQuantity)
            {
                text.text = quantity + " [MAX]";
                text.color = Color.red;
            }
            else
            {
                text.text = quantity + "/" + maxQuantity;
                text.color = Color.white;
            }
        }

        if (moneyText != null)
        {
            moneyText.text = "Money: $" + playerMoney;
        }
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        UpdateAllUI();
    }
}