using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrinkMachine : MonoBehaviour
{
    [SerializeField] private List <GameObject> Dispence;
    void Update()
    {
        FillDrink();
    }
    void drink1()
    {

    }
    void FillDrink()
    {
        for (int i = 0; i < Dispence.Count; i++)
        {
            RaycastHit hit;
            Ray ray = new Ray(Dispence[i].transform.position, Dispence[i].transform.forward);
            Debug.DrawRay(Dispence[i].transform.position, Dispence[i].transform.forward, Color.blue);
            if (Physics.Raycast(ray, out hit, 1))
            {

                if (hit.transform.gameObject.GetComponent<Glass>())
                {
                    Debug.Log("see glass");
                    Glass drink = hit.transform.gameObject.GetComponent<Glass>();
                    if (drink.Drink.transform.localScale.y<=0.35f&& !drink.Full && drink.place!=null)
                    {
                        Debug.Log("filling");
                        drink.Drink.transform.localScale = drink.Drink.transform.localScale + new Vector3(0, 0.00175f, 0);
                        drink.Drink.transform.position = drink.Drink.transform.position + new Vector3(0, 0.001f, 0);
                    }else
                    {
                        Debug.Log("done");
                        drink.Full = true;
                    }
                }
            }
        }
    }
}
