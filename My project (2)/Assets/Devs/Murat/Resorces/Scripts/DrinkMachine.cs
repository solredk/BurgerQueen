using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrinkMachine : MonoBehaviour
{
    [SerializeField] private GameObject Dispence;
    [SerializeField] private List <Material> ColorDrink;
    private int chosenColor;
    private bool on = false;
    private Glass drink;
    void Update()
    {
        FillDrink();
    }
    public void drinkColor(int color)
    {
        if(drink!= null)
        {
            if(drink.place!= null)
            {
                on = true;
                chosenColor = color;
            }
        }
    }
    void FillDrink()
    {
        RaycastHit hit;
        Ray ray = new Ray(Dispence.transform.position, Dispence.transform.forward);
        Debug.DrawRay(Dispence.transform.position, Dispence.transform.forward, Color.blue);
        if (Physics.Raycast(ray, out hit, 1))
        {

            if (hit.transform.gameObject.GetComponent<Glass>())
            {
                drink = hit.transform.gameObject.GetComponent<Glass>();
                drink.Drink.GetComponent<MeshRenderer>().material = ColorDrink[chosenColor];

                if (drink.Drink.transform.localScale.y <= 0.35f && !drink.Full && drink.place != null && on)
                {
                    drink.Drink.transform.localScale = drink.Drink.transform.localScale + new Vector3(0, 0.00175f, 0);
                    drink.Drink.transform.position = drink.Drink.transform.position + new Vector3(0, 0.001f, 0);
                    Debug.Log("filling");
                }
                else if(drink.Drink.transform.localScale.y >= 0.35f)
                {
                    drink.Full = true;
                    on = false;
                    Debug.Log("done");
                }
            }
        }
    }
}
