using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IngredientPurchaseUI : MonoBehaviour
{
    [Header("UI")]
    public List<Text> quantityTexts;
    public List<string> ingredientNames;
    public List<int> quantities;
    public List<int> maxQuantities;

    [Header("Money")]
    public int playerMoney = 1000;
    public Text moneyText;
    public int costPerItem = 10;

    private void Start()
    {
        if (quantities == null || quantities.Count == 0)
        {
            quantities = new List<int>();
            maxQuantities = new List<int>();
            for (int i = 0; i < ingredientNames.Count; i++)
            {
                quantities.Add(0);
                maxQuantities.Add(100);
            }
        }
        UpdateAllUI();
    }

    public void PurchaseByIndex(int index)
    {
        if (index < 0 || index >= quantities.Count)
            return;

        if (quantities[index] >= maxQuantities[index])
            return;

        if (playerMoney >= costPerItem)
        {
            playerMoney -= costPerItem;
            quantities[index]++;
            UpdateAllUI();
        }
    }

    public void UpdateAllUI()
    {
        for (int i = 0; i < quantities.Count; i++)
        {
            if (i >= quantityTexts.Count)
                continue;

            Text text = quantityTexts[i];

            if (quantities[i] >= maxQuantities[i])
            {
                text.text = quantities[i] + " [MAX]";
                text.color = Color.red;
            }
            else
            {
                text.text = quantities[i] + "/" + maxQuantities[i];
                text.color = Color.white;
            }
        }

        if (moneyText != null)
        {
            moneyText.text = "Money: $" + playerMoney;
        }
    }

    public int GetQuantity(int index)
    {
        if (index >= 0 && index < quantities.Count)
            return quantities[index];
        return 0;
    }
}