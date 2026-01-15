using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BurgerAssambleManager : MonoBehaviour
{
    private bool handEmpty = true;

    public int ingedientAmount = 0;
    
    [SerializeField] private List<int> storage;
    [SerializeField] private List<TextMeshProUGUI> storageText;
    [SerializeField] private List <string> orderIngredients;
    public List<GameObject> burger;

  //  [SerializeField] private IngredientManager ingredientManager;

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
        
        IngredientManager.instance.ResetIngridients(20);

        storageText[0].text = IngredientManager.instance. GetAmount("Burger Bun").ToString();

        storageText[1].text = IngredientManager.instance.GetAmount("Lettuce").ToString();

        storageText[2].text = IngredientManager.instance.GetAmount("Cheese").ToString();

        storageText[3].text = IngredientManager.instance.GetAmount("Tomatoes").ToString();

        storageText[4].text = IngredientManager.instance.GetAmount("Meat").ToString();

        if (StorageIngridientsCheck())
        {
            // hoeveelheidBrood = Hoeveel brood de speler heeft toegevoegd van supply
            for (int i = 0; i < IngredientManager.instance.ingredients.Count; i++)
            {

                storageText[i].text = storage[i].ToString();
            }

            handEmpty = true;
        }

       

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
        if (IngredientManager.instance.GetAmount("Burger Bun") > 1) 
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
                if (IngredientManager.instance.GetAmount(ingredientName) > 0)
                {
                    Instantiate(ingredients[ingredientNumber], buckets[i].gameObject.transform.position, Quaternion.identity);
                    IngredientManager.instance.GiveAmount(ingredientName, -1);
                }
            }
            storageText[ingredientNumber].text = IngredientManager.instance.GetAmount(ingredientName).ToString();
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