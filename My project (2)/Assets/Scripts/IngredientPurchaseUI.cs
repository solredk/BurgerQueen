using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class IngredientPurchaseUI : MonoBehaviour
{
    [Header("UI - Match order with IngredientManager ingredients list")]
    public List<TextMeshProUGUI> quantityTexts;
    public List<TextMeshProUGUI> priceTexts;

    [Header("Money")]
    public int startingMoney = 1000;
    public TextMeshProUGUI moneyText;

    private static int playerMoney;
    private static bool moneyLoaded = false;

    private void Start()
    {
        
        if (IngredientManager.instance == null)
        {
            Debug.LogError("IngredientManager.instance is null! Make sure IngredientManager exists in scene.");
            return;
        }

        LoadAll();
        UpdateAllUI();
    }

    private void OnApplicationQuit()
    {
        SaveAll();
    }

    private void OnDisable()
    {
        SaveAll();
    }

    public void PurchaseByIndex(int index)
    {
        if (IngredientManager.instance == null)
            return;

        if (index < 0 || index >= IngredientManager.instance.ingredients.Count)
            return;

        Ingredient ingredient = IngredientManager.instance.ingredients[index];

        if (ingredient == null)
            return;

        if (ingredient.Quantity >= ingredient.MaxQuantity)
            return;

        if (playerMoney >= ingredient.PurchaseCost)
        {
            playerMoney -= ingredient.PurchaseCost;
            IngredientManager.instance.GiveAmount(ingredient.Name, 1);
            SaveAll();
            UpdateAllUI();
        }
    }

    public void UpdateAllUI()
    {
        if (IngredientManager.instance == null)
            return;

        for (int i = 0; i < IngredientManager.instance.ingredients.Count; i++)
        {
            Ingredient ingredient = IngredientManager.instance.ingredients[i];

            if (ingredient == null)
                continue;

            // Update quantity text
            if (i < quantityTexts.Count && quantityTexts[i] != null)
            {
                if (ingredient.Quantity >= ingredient.MaxQuantity)
                {
                    quantityTexts[i].text = ingredient.Quantity + " [MAX]";
                    quantityTexts[i].color = Color.red;
                }
                else
                {
                    quantityTexts[i].text = ingredient.Quantity + "/" + ingredient.MaxQuantity;
                    quantityTexts[i].color = Color.white;
                }
            }

            // Update price text
            if (i < priceTexts.Count && priceTexts[i] != null)
            {
                priceTexts[i].text = "$" + ingredient.PurchaseCost;
            }
        }

        // Update money text
        if (moneyText != null)
        {
            moneyText.text = "Money: $" + playerMoney;
        }
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        SaveAll();
        UpdateAllUI();
    }

    public void SaveAll()
    {
        if (IngredientManager.instance == null)
            return;

        PlayerPrefs.SetInt("PlayerMoney", playerMoney);

        foreach (Ingredient ingredient in IngredientManager.instance.ingredients)
        {
            if (ingredient != null)
            {
                PlayerPrefs.SetInt("Ingredient_" + ingredient.Name, ingredient.Quantity);
            }
        }

        PlayerPrefs.Save();
    }

    public void LoadAll()
    {
        if (IngredientManager.instance == null)
            return;

        // Load money only once
        if (!moneyLoaded)
        {
            playerMoney = PlayerPrefs.GetInt("PlayerMoney", startingMoney);
            moneyLoaded = true;
        }

        // Load ingredient quantities
        foreach (Ingredient ingredient in IngredientManager.instance.ingredients)
        {
            if (ingredient != null && PlayerPrefs.HasKey("Ingredient_" + ingredient.Name))
            {
                int savedQuantity = PlayerPrefs.GetInt("Ingredient_" + ingredient.Name);
                ingredient.Quantity = savedQuantity;
            }
        }
    }
    
    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        playerMoney = startingMoney;
        moneyLoaded = true;

        if (IngredientManager.instance != null)
        {
            IngredientManager.instance.ResetIngridients(0);
        }

        UpdateAllUI();
    }
}