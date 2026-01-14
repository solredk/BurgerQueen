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
    [SerializeField] private List<GameObject> Buttons;
    private int chosenColor;
    private bool on = false;
    private Glass drink;
    private float Size;
    private float hight;
    [SerializeField] private float timer = 2;
    void Update()
    {
        PushButton();
        FillDrink();
    }
    public void drinkColor(int color)
    {
        if(drink!= null&&!on)
        {
            if(drink.place!= null)
            {
                if (color==0&& !drink.Ice.active)
                {
                    drink.Ice.active = true;
                }
                else if(color != 0)
                {
                    on = true;
                    chosenColor = color;
                }

            }
        }
    }
    void PushButton()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray,out hit)&& Mouse.current.leftButton.IsPressed())
        {
            if (Buttons.Contains(hit.transform.gameObject))
            {
                MacheneButton button = hit.transform.GetComponent<MacheneButton>();
                drinkColor(button.flaverNumber);
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
                    float sizeX = 0;
                    float sizeZ = 0;
                    if (chosenColor != 0)
                    {
                        poorDrink.SetActive(true);
                        Size = 0.25f;
                        hight = 0.15f;
                        sizeX = 0;
                        sizeZ = 0;
                    }
                    else
                    {
                        on = false;
                    }
                    poorDrink.GetComponent<MeshRenderer>().material = ColorDrink[chosenColor];
                    drink.Drink.GetComponent<MeshRenderer>().material = ColorDrink[chosenColor];
                    timer = 20;
                    if (drink.Drink.transform.localScale.y <= Size && !drink.Full && drink.place != null && on)
                    {
                        drink.Drink.transform.localScale = Vector3.MoveTowards(drink.Drink.transform.localScale, drink.Drink.transform.localScale + new Vector3(sizeX * Time.deltaTime, Size * Time.deltaTime, sizeZ * Time.deltaTime), timer);
                        drink.Drink.transform.position = Vector3.MoveTowards(drink.Drink.transform.position, drink.Drink.transform.position + new Vector3(0, hight * Time.deltaTime, 0), timer);

                        Debug.Log("filling");
                    }
                    else if (drink.Drink.transform.localScale.y >= Size && on)
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
}
