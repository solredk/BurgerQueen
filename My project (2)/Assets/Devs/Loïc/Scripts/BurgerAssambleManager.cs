using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class BurgerAssambleManager : MonoBehaviour
{
    [SerializeField] private GameObject maakPlaat;
    private bool handEmpty = true;
    [SerializeField] private GameObject breadObj;
    private GameObject currentIngredient;
    [SerializeField] private GameObject prepPlaceObj;
    private int ingedientanmount = 0;
    [SerializeField] private List<GameObject> burger;
    [SerializeField] private List<int> voorraadI;
    [SerializeField] private List<TextMeshProUGUI> voorraadTexts;
    private int ingredientID;
    private Collider plaatCollider;
    private Grabing grabingS;
  


    void Start()
    {
        grabingS = FindAnyObjectByType<Grabing>();
        plaatCollider = maakPlaat.GetComponent<Collider>();
        
        // hoeveelheidBrood = Hoeveel brood de speler heeft toegevoegd van supply
        for (int i = 0; i < voorraadI.Count; i++)
        {
            voorraadTexts[i].text = voorraadI[i].ToString();
        }
        
        handEmpty = true;
    }

    void Update()
    {
    
    }


    public void AddToBurger()
    {
        if (!handEmpty)
        {
            if (ingedientanmount == 0)
            {
                GameObject ingredient = Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y - 200), Quaternion.identity);
                burger.Add(ingredient);
            }
            Destroy(currentIngredient);
            handEmpty = true;
            ingedientanmount++;
        }
    }

    public void IsTriggered()
    {
       // GameObject burgerIng = Instantiate(grabingS.Helditem, new Vector2(maakPlaat.transform.position.x, maakPlaat.transform.position.y), Quaternion.identity);
       // burgerIng.layer = default;
        Destroy(breadObj);
    }

    public void TempButton()
    {

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
