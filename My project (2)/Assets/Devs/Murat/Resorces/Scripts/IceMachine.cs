using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class IceMachine : MonoBehaviour
{
    [SerializeField] private List<Mech> MechList;
    [SerializeField] private List<Material> ColorDrink;
    [SerializeField] private Material sprinkels;
    [SerializeField] private List<GameObject> Buttons;
    private float Size;
    private float hight;
    [SerializeField] private float timer = 2;

    public Glass m_Glass;

    private void Start()
    {
        m_Glass = FindAnyObjectByType<Glass>();
    }
    void Update()
    {
        PushButton();
        FillDrink();
    }
    private void drinkColor(int color)
    {
        if (MechList[color].drink != null && !MechList[color].filling)
        {
            if (MechList[color].drink.place != null)
            {
                MechList[color].filling = true;
                MechList[color].chosenColor = color;
            }
        }
    }
    private void KindSprinkle(int kind)
    {
        if (MechList[kind].drink != null && !MechList[kind].sprinkeling)
        {
            if (MechList[kind].drink.place != null)
            {
                MechList[kind].sprinkeling = true;
                MechList[kind].chosenSprinkel = kind;
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
                print("huh");

                MacheneButton button = hit.transform.GetComponent<MacheneButton>();
                switch (button.funcion)
                {
                    case 0: drinkColor(button.flaverNumber);
                            ;break;
                    case 1: KindSprinkle(button.flaverNumber); break;
                    case 2: Done(button.flaverNumber); break;
                    default: break;
                }
            }
        }
    }
    private void AddIceCream(int i)
    {
        float sizeX = 0;
        float sizeZ = 0;
        switch (MechList[i].drink.contaner)
        {
            case Glass.container.IceCreamCone:
                MechList[i].poorDrink.SetActive(true);
                Size = 700f;
                sizeX = 700f;
                sizeZ = 700f;
                hight = 0.07f;
                break;
            case Glass.container.IceCreamCup:
                MechList[i].poorDrink.SetActive(true);
                Size = 0.06f;
                hight = 0.05f;
                sizeX = 0;
                sizeZ = 0;
                break;
            default: break;
        }

        MechList[i].poorDrink.GetComponent<MeshRenderer>().material = ColorDrink[MechList[i].chosenColor];
        MechList[i].drink.Drink.GetComponent<MeshRenderer>().material = ColorDrink[MechList[i].chosenColor];
        timer = 20;
        if (MechList[i].drink.Drink.transform.localScale.y <= Size && MechList[i].drink.place != null)
        {
            MechList[i].drink.Drink.transform.localScale = Vector3.MoveTowards(MechList[i].drink.Drink.transform.localScale, MechList[i].drink.Drink.transform.localScale + new Vector3(sizeX * Time.deltaTime, Size * Time.deltaTime, sizeZ * Time.deltaTime), timer);
            MechList[i].drink.Drink.transform.position = Vector3.MoveTowards(MechList[i].drink.Drink.transform.position, MechList[i].drink.Drink.transform.position + new Vector3(0, hight * Time.deltaTime, 0), timer);
        }
        else if (MechList[i].drink.Drink.transform.localScale.y >= Size)
        {
            MechList[i].drink.Full = true;
            MechList[i].poorDrink.SetActive(false);
        }

    }
    private void AddSprinkels(int i)
    {
        List<Material> addSprinkel = new List<Material>();
        addSprinkel.Add(MechList[i].drink.Drink.GetComponent<MeshRenderer>().material);
        addSprinkel.Add(sprinkels);
        MechList[i].drink.Drink.GetComponent<MeshRenderer>().SetMaterials(addSprinkel);
        MechList[i].drink.Sprinkeled = true;
    }
    private void Done(int i)
    {
        if (MechList[i].drink.Full)
        {
            MechList[i].drink.Done = true;
            MechList[i].filling = false;
            MechList[i].sprinkeling = false;
            MechList[i].drink = null;
        }
    }
    void FillDrink()
    {
        for (int i = 0;i < MechList.Count; i++)
        {
            RaycastHit hit;
            Ray ray = new Ray(MechList[i].Dispence.transform.position, MechList[i].Dispence.transform.forward);
            Debug.DrawRay(MechList[i].Dispence.transform.position, MechList[i].Dispence.transform.forward * 10, UnityEngine.Color.blue);
            if (Physics.Raycast(ray, out hit, 2))
            {
                if (hit.transform.gameObject.GetComponent<Glass>())
                {
                    MechList[i].drink = hit.transform.gameObject.GetComponent<Glass>();
                    if (!MechList[i].drink.Full && MechList[i].filling)
                    {
                        AddIceCream(i);
                    }
                    else if (MechList[i].drink.Full && MechList[i].sprinkeling && !MechList[i].drink.Drink.GetComponent<MeshRenderer>().materials.Contains(sprinkels))
                    {
                        AddSprinkels(i);
                    }
                }
            }
        }
    }
}

