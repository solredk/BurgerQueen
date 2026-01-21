using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public static IngredientManager instance;

    [SerializeField] public List<Ingredient> ingredients;

    private void Awake()
    {
        instance = this;        
    }

    public void ResetIngridients(int defeaultValue)
    {
        foreach (Ingredient ingredients in ingredients)
        {
            ingredients.Quantity = defeaultValue;
        }
    }

    public int GetAmount(string ingredientName)
    {
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredient.Name == ingredientName)
                return ingredient.Quantity;
        }

        Debug.Log("ingredient not found");

        return 0;
    }

    public void GiveAmount(string ingredientName, int amount)
    {
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredient.Name == ingredientName)
            {
                ingredient.Quantity += amount;
                if (ingredient.Quantity > 50)
                    ingredient.Quantity = 50;
                break;
            }
        }
        Debug.Log("ingredient not found");
    }
    public void SetAmount()
    {
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredient.Quantity < 20)
            {
                ingredient.Quantity += 10;
                break;
            }
        }
        Debug.Log("ingredient not found");
    }
}
