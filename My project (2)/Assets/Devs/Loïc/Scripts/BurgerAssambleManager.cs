using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class BurgerAssambleManager : MonoBehaviour
{
    private bool handEmpty = true;
    [SerializeField] private GameObject breadObj;
    private GameObject currentIngredient;
    [SerializeField] private GameObject prepPlaceObj;
    private int ingedientanmount = 0;
    [SerializeField] private List<GameObject> burger;
    [SerializeField] private List<int> voorraadI;
    [SerializeField] private List<TextMeshProUGUI> voorraadTexts;
    private int ingredientID;

    void Start()
    {
        // hoeveelheidBrood = Hoeveel brood de speler heeft toegevoegd van supply
        for (int i = 0; i < voorraadI.Count; i++)
        {
            voorraadTexts[i].text = voorraadI[i].ToString();
        }
        
        handEmpty = true;
    }

    //public void JustGiveMeTheDamnMousePosition(InputAction.CallbackContext context)
    //{
    //    mousePos = context.ReadValue<Vector2>();
    //}

    //public void LeftClick(InputAction.CallbackContext context)
    //{
    //    print("left clickyyyy");
    //}

    void Update()
    {
    
    }

    public void OnPreCull()
    {
        if (!handEmpty)
        {
            if (ingedientanmount == 0)
            {
                GameObject ingredient = Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y - 200), Quaternion.identity);
                burger.Add(ingredient);
            }
            else if (ingedientanmount == 1)
            {
                GameObject ingredient = Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y - 100), Quaternion.identity);
                burger.Add(ingredient);
            }
            else if (ingedientanmount == 2)
            {
               GameObject ingredient = Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y), Quaternion.identity);
                burger.Add(ingredient);
            }
            else if (ingedientanmount == 3)
            {
                GameObject ingredient = Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y + 100), Quaternion.identity);
                burger.Add(ingredient);
            }
            Destroy(currentIngredient);
            handEmpty = true;
            ingedientanmount++;
        }

    }

    public void TempButton2()
    {
        if (ingredientID == 0)
        {
            ingredientID = 1;
        }
        else
        {
            ingredientID = 0;
        }
        print(ingredientID);
    }

    private void VoorraadCheck()
    {
        int ingredientI = ingredientID;
        // ingredient I word bepaald wanneer er op de bak met ingredienten word gedrukt
        // int ingredientI = ingredientDieGepaktWilWordenID
        if (voorraadI[ingredientI] > 0)
        {
            voorraadI[ingredientI]--;
            voorraadTexts[ingredientI].text = voorraadI[ingredientI].ToString();
        }
        else
        {
            print("geen brood????");
        }
    }

    public void TempButton()
    {
        
    }

    public void SlaButton()
    {
        if (handEmpty)
        {
          //  currentIngredient = Instantiate(slaObj, mousePos, Quaternion.identity, gameObject.transform);
            handEmpty = false;
        }
    }

    public void TrashButton()
    {
        if (!handEmpty)
        {
            Destroy(currentIngredient);
            handEmpty = true;
        }
    }

    public void ClearButton()
    {
        for (int i = 0; i < burger.Count; i++)
        {
            Destroy(burger[i]);
        }
        burger.Clear();
        ingedientanmount = 0;
    }
}
