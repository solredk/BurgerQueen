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

    public int ingedientAmount = 0;
    
    [SerializeField] private List<int> storage;
    [SerializeField] private List<TextMeshProUGUI> storageText;
    [SerializeField] private List <string> orderIngredients;
    public List<GameObject> burger;

    [SerializeField] private IngredientManager ingredientManager;

    [SerializeField] private GameObject workStation;
    [SerializeField] private GameObject breadObject;
    [SerializeField] private GameObject prepPlaceObj;
    private GameObject currentIngredient;

    [SerializeField] private List<Collider> breadBucket;
    [SerializeField] private List<Collider> lettuceBucket;
    [SerializeField] private List<Collider> cheeseBucket;
    [SerializeField] private List<Collider> tomatoBucket;
    [SerializeField] private List<Collider> meatBucket;




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

        ingredientManager.GiveAmount("Burger Bun", 20);
        storageText[0].text = ingredientManager.GetAmount("Burger Bun").ToString();

        ingredientManager.GiveAmount("Lettuce", 20);
        storageText[1].text = ingredientManager.GetAmount("Lettuce").ToString();

        ingredientManager.GiveAmount("Cheese", 20);
        storageText[2].text = ingredientManager.GetAmount("Cheese").ToString();

        ingredientManager.GiveAmount("Tomatoes", 20);
        storageText[3].text = ingredientManager.GetAmount("Tomatoes").ToString();
        
        ingredientManager.GiveAmount("Meat", 20);
        storageText[4].text = ingredientManager.GetAmount("Meat").ToString();

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


    private void AddIngredient(string ingredientName, int ingredientNumber, List<Collider> buckets)
    {
        for (int i = 0; i < buckets.Count; i++)
        {
            bool bucketFilled = buckets[i].gameObject.GetComponent<CollisionDetector>().occupied;
            if (bucketFilled == false)
            {
                if (ingredientManager.GetAmount(ingredientName) > 0)
                {
                    Instantiate(ingredients[ingredientNumber], buckets[i].gameObject.transform.position, Quaternion.identity);
                    ingredientManager.GiveAmount(ingredientName, -1);
                }
            }
            storageText[ingredientNumber].text = ingredientManager.GetAmount(ingredientName).ToString();
        }
    }
    public void AddBun()
    {
        AddIngredient("Burger Bun", 0, breadBucket);
    }
    public void AddLettuce()
    {
        AddIngredient("Lettuce", 1, lettuceBucket);
    }
    public void AddCheese()
    {
        AddIngredient("Cheese", 2, cheeseBucket);
    }
    public void AddTomatoes()
    {
        AddIngredient("Tomatoes", 3, tomatoBucket);
    }
    public void AddMeat()
    {
        AddIngredient("Meat", 4, meatBucket);
    }

    
}