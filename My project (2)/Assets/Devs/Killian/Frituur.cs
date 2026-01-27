using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


public class Frituur : MonoBehaviour
{
    public bool ingredientSpawned = false;
    public GameObject currentIngredient;
    [SerializeField] private GameObject friesObj;
    [SerializeField] public GameObject friedFriesobj;
    [SerializeField] private GameObject chickenNuggetObj;
    [SerializeField] private GameObject friedChickenNuggetObj;
    [SerializeField] private GameObject onionRingObj;
    [SerializeField] private GameObject friedOnionRingObj;
    [SerializeField] private GameObject friesSpawnPos;
    private Grabing grabingS;

    public GameObject CookingIngredient;

    public FryPlate m_FryPlate;

    void Start()
    {
        grabingS = FindAnyObjectByType<Grabing>();
        m_FryPlate = FindAnyObjectByType<FryPlate>();
    }
    public void FriesButton()
    {
        if (ingredientSpawned == false)
        {
            ingredientSpawned = true;
            currentIngredient = Instantiate(friesObj, friesSpawnPos.transform.position, Quaternion.identity);
            CookingIngredient = friedFriesobj;
            IngredientManager.instance.GiveAmount("Fries", -1);
        }
    }
    public void ChickenNuggetButton()
    {
        if (ingredientSpawned == false)
        {
            currentIngredient = Instantiate(chickenNuggetObj, friesSpawnPos.transform.position, Quaternion.identity);
            ingredientSpawned = true;
            CookingIngredient = friedChickenNuggetObj;
            IngredientManager.instance.GiveAmount("Nuggets", -1);

        }
    }

    public void OnionRingButton()
    {
        if (ingredientSpawned == false)
        {
            currentIngredient = Instantiate(onionRingObj, friesSpawnPos.transform.position, Quaternion.identity);
            ingredientSpawned = true;
            CookingIngredient = friedOnionRingObj;
            IngredientManager.instance.GiveAmount("OnionRings", -1);


        }
    }

    public void TrashButton()
    {
        if (ingredientSpawned)
        {
            Destroy(currentIngredient);
            ingredientSpawned = false;
        }
    }
}
