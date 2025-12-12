using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrinkMachine : MonoBehaviour
{
    [SerializeField] private GameObject Dispence;
    [SerializeField] private GameObject poorDrink;
    [SerializeField] private List <Material> ColorDrink;
    private int chosenColor;
    private bool on = false;
    private Glass drink;
    private float Size;
    private float hight;
    [SerializeField] private float timer = 2;
    void Update()
    {
        FillDrink();
    }
    public void drinkColor(int color)
    {
        if(drink!= null&&!on)
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
        Debug.DrawRay(Dispence.transform.position, Dispence.transform.forward*10, Color.blue);
        if (Physics.Raycast(ray, out hit, 2))
        {
            if (hit.transform.gameObject.GetComponent<Glass>())
            {
                drink = hit.transform.gameObject.GetComponent<Glass>();
                if (on)
                {
                    Math();
                    drink.Drink.GetComponent<MeshRenderer>().material = ColorDrink[chosenColor];
                    switch (drink.contaner)
                    {
                        case Glass.container.Drink:
                            poorDrink.SetActive(true);
                            poorDrink.GetComponent<MeshRenderer>().material = ColorDrink[chosenColor];
                            break;
                        case Glass.container.IceCreamCup:
                            break;
                        case Glass.container.IceCreamCone:
                            break;
                    }
                    if (!drink.Full && drink.place != null)
                    {
                        drink.Drink.transform.localScale = Vector3.MoveTowards(drink.Drink.transform.localScale, drink.Drink.transform.localScale + new Vector3(0, hight, 0), timer);
                        drink.Drink.transform.position = Vector3.MoveTowards(drink.Drink.transform.position, drink.Drink.transform.position + new Vector3(0, hight, 0), timer);

                        Debug.Log("filling");
                    }
                    else if (drink.Drink.transform.localScale.y >= 0.25f)
                    {
                        drink.Full = true;
                        on = false;
                        poorDrink.SetActive(false);
                        Debug.Log("done");
                    }
                }
            }
        }
    }
    void Math()
    {
        switch (drink.contaner)
        {
            case Glass.container.Drink: 
                Size = 0.25f;
                hight = 0.15f;
                timer = 2;
                break;
            case Glass.container.IceCreamCup:
                Size = 0.06f;
                hight = 0.1f;
                break;
            case Glass.container.IceCreamCone: 
                break;
        }
    }

}
