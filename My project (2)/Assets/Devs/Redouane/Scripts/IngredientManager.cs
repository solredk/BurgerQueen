using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public static IngredientManager instance;

    [SerializeField] private Ingredient[] ingredients;

    public int GetAmount(string ingredientName)
    {
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredient.ingredientName == ingredientName)
            {
                return ingredient.quantity;
            }
        }
        Debug.Log("ingredient not found");
        return 0;
    }

    public void GiveAmount(string ingredientName, int amount)
    {
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredient.ingredientName == ingredientName)
            {
                ingredient.quantity += amount;
                break;
            }
        }
        Debug.Log("ingredient not found");
    }
}
