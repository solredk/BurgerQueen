using System.Collections.Generic;
using TMPro;
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
    
    
    [SerializeField] private GameObject workStation;
    [SerializeField] private GameObject breadObject;
    [SerializeField] private GameObject prepPlaceObj;
    private GameObject CurrentIngredient;


    void Start()
    {        
        // hoeveelheidBrood = Hoeveel brood de speler heeft toegevoegd van supply
        for (int i = 0; i < storage.Count; i++)
        {
            storageText[i].text = storage[i].ToString();
        }
        
        handEmpty = true;
    }


    public void AddToBurger()
    {
        if (!handEmpty)
        {
            if (ingedientAmount == 0)
            {
                GameObject ingredient = Instantiate(CurrentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y - 200), Quaternion.identity);
                burger.Add(ingredient);
            }
            Destroy(CurrentIngredient);
            handEmpty = true;
            ingedientAmount++;
        }
    }

    public void TempButton()
    {
        //check oger
        bool goodOrder = true;
        for (int i = 0; i < burger.Count; i++)
        {
            if (burger[i].gameObject.name != orderIngredients[i])
            {
                print("roblox oof");
                goodOrder = false;
            }
            else if (goodOrder)
            {
                print("that burger is a burger");
            }
        }
    }

    //if (ingredientID == 0)
    //{
    //    ingredientID = 1;
    //}
    //else
    //{
    //    ingredientID = 0;
    //}
    //print(ingredientID);

    private void VoorraadCheck()
    {
        int ingredientI = ingredientID;
        // ingredient I word bepaald wanneer er op de bak met ingredienten word gedrukt
        // int ingredientI = ingredientDieGepaktWilWordenID
        if (storage[ingredientI] > 0)
        {
            storage[ingredientI]--;
            storageText[ingredientI].text = storage[ingredientI].ToString();
        }
        else
        {
            print("geen brood????");
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