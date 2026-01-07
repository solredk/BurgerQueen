using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class BurgerAssambleManager : MonoBehaviour
{
    private bool handEmpty = true;

    private int ingedientAmount = 0;
    private int ingredientID;  
    
    [SerializeField] private List<int> storage;
    [SerializeField] private List<TextMeshProUGUI> storageText;
    [SerializeField] private List <string> orderIngredients;
    public List<GameObject> burger;

    [SerializeField] private IngredientManager ingredientManager;

    [SerializeField] private GameObject workStation;
    [SerializeField] private GameObject breadObject;
    [SerializeField] private GameObject prepPlaceObj;
    private GameObject currentIngredient;

    [SerializeField] private List<Collider> ingredientBucket;

    [SerializeField] private List<GameObject> ingredients;
    
    [SerializeField] private GameObject pauseScreen;
    private bool paused;

    void Start()
    {
        if (StorageIngridientsCheck())
        {
            // hoeveelheidBrood = Hoeveel brood de speler heeft toegevoegd van supply
            for (int i = 0; i < ingredientManager.ingredients.Count; i++)
            {

                storageText[i].text = storage[i].ToString();
            }

            handEmpty = true;
        }

        ingredientManager.GiveAmount("Lettuce", 5);
        storageText[0].text = ingredientManager.GetAmount("Lettuce").ToString();
        
    }

    public void Pause()
    {
        if (paused == false)
        {
            pauseScreen.SetActive(true);
            paused = true;
            Time.timeScale = 0f;
           
        }
        else
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            paused = false;
        }   
    }

    public bool StorageIngridientsCheck()
    {
        if (ingredientManager.GetAmount("Burger Bun") > 1) 
        { 

            return true;
        }
        return false;
    }

    public void AddToBurger()
    {
        if (!handEmpty)
        {
            if (ingedientAmount == 0)
            {
                GameObject ingredient = Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y - 200), Quaternion.identity);
                burger.Add(ingredient);
            }
            Destroy(currentIngredient);
            handEmpty = true;
            ingedientAmount++;
        }
    }

    public void TempButton()
    {
        //check oger
        //bool goodOrder = true;
        //for (int i = 0; i < burger.Count; i++)
        //{
        //    if (burger[i].gameObject.name != orderIngredients[i])
        //    {
        //        print("roblox oof");
        //        goodOrder = false;
        //    }
        //    else if (goodOrder)
        //    {
        //        print("that burger is a burger");
        //    }
        //}

        ingredientManager.GiveAmount("Lettuce", 5);

        int lettuceAmount = ingredientManager.GetAmount("Lettuce");
        print(lettuceAmount);
    }

    public void AddLettuce()
    {
        int lettuceAmount = ingredientManager.GetAmount("Lettuce");
        for (int i = 0; i < ingredientBucket.Count; i++)
        {
            bool canEnter = ingredientBucket[i].gameObject.GetComponent<CollisionDetector>().occupied;
            if (canEnter == false)
            {
                if (lettuceAmount > 0)
                {
                    Instantiate(ingredients[0], ingredientBucket[i].gameObject.transform.position, Quaternion.identity);
                    lettuceAmount--;
                    storageText[0].text = lettuceAmount.ToString();
                }
            }

        }
    }

    public void ClearButton()
    {
        for (int i = 0; i < burger.Count; i++)
        {
            Destroy(burger[i]);
        }
        burger.Clear();
        ingedientAmount = 0;
    }
}