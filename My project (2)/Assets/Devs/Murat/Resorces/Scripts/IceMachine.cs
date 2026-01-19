using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceMachine : MonoBehaviour
{
    [SerializeField] private GameObject Dispence;
    [SerializeField] private GameObject poorDrink;
    [SerializeField] private List<Material> ColorDrink;
    [SerializeField] private List<GameObject> Buttons;
    private int chosenColor;
    private int chosenSprinkel;
    [SerializeField] private Glass drink;
    private bool filling = false;
    private bool sprinkeling = false;
    private float Size;
    private float hight;
    [SerializeField] private float timer = 2;
    void Update()
    {
        PushButton();
        FillDrink();
    }
    private void drinkColor(int color)
    {
        if (drink != null&&!filling)
        {
            if (drink.place != null)
            {
                filling = true;
                chosenColor = color;
            }
        }
    }
    private void KindSprinkle(int kind)
    {
        if (drink != null && !sprinkeling)
        {
            if (drink.place != null)
            {
                sprinkeling = true;
                chosenSprinkel = kind;
            }
        }
    }
    private void PushButton()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit) && Mouse.current.leftButton.IsPressed())
        {
            if (Buttons.Contains(hit.transform.gameObject))
            {
                MacheneButton button = hit.transform.GetComponent<MacheneButton>();
                switch (button.funcion)
                {
                    case 0: drinkColor(button.flaverNumber);break;
                    case 1: KindSprinkle(button.flaverNumber); break;
                    default: break;
                }
            }
        }
    }
    private void AddIceCream()
    {
        float sizeX = 0;
        float sizeZ = 0;
        switch (drink.contaner)
        {
            case Glass.container.IceCreamCone:
                poorDrink.SetActive(true);
                Size = 1.5f;
                sizeX = 1.8f;
                sizeZ = 1.8f;
                hight = 0.07f;
                break;
            case Glass.container.IceCreamCup:
                poorDrink.SetActive(true);
                Size = 0.06f;
                hight = 0.05f;
                sizeX = 0;
                sizeZ = 0;
                break;
            default: break;
        }

        poorDrink.GetComponent<MeshRenderer>().material = ColorDrink[chosenColor];
        drink.Drink.GetComponent<MeshRenderer>().material = ColorDrink[chosenColor];
        timer = 20;
        if (drink.Drink.transform.localScale.y <= Size && drink.place != null)
        {
            drink.Drink.transform.localScale = Vector3.MoveTowards(drink.Drink.transform.localScale, drink.Drink.transform.localScale + new Vector3(sizeX * Time.deltaTime, Size * Time.deltaTime, sizeZ * Time.deltaTime), timer);
            drink.Drink.transform.position = Vector3.MoveTowards(drink.Drink.transform.position, drink.Drink.transform.position + new Vector3(0, hight * Time.deltaTime, 0), timer);
        }
        else if (drink.Drink.transform.localScale.y >= Size)
        {
            drink.Full = true;
            poorDrink.SetActive(false);
        }
    }
    private void AddSprinkels()
    {
        if (true)
        {
            drink.Sprinkeled = true;
        }
    }
    public void Done()
    {
        drink.Done = true;
        filling = false;
        sprinkeling = false;
        drink = null;
    }
    void FillDrink()
    {
        RaycastHit hit;
        Ray ray = new Ray(Dispence.transform.position, Dispence.transform.forward);
        Debug.DrawRay(Dispence.transform.position, Dispence.transform.forward * 10, UnityEngine.Color.blue);
        if (Physics.Raycast(ray, out hit, 2))
        {
            if (hit.transform.gameObject.GetComponent<Glass>())
            {
                drink = hit.transform.gameObject.GetComponent<Glass>();
                if (!drink.Full&& filling)
                {
                    AddIceCream();
                }
                else if (drink.Full&& sprinkeling)
                {
                    AddSprinkels();
                }
            }
        }
    }
}

