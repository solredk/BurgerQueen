using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Rejester : MonoBehaviour
{
    public List<Image> Burger;
    public List<Image> CompBurger;
    public GameObject pre_order;
    public List<GameObject> Orders; 
    public GameObject canvas;
    public void PutOnComp()
    {
        for (int i = 0; i < CompBurger.Count; i++)
        {
            CompBurger[i].color = new Color(0, 0, 0, 0);
        }
        for (int i = 0; i < Burger.Count; i++)
        {
            CompBurger[i].color = Burger[i].color;
        }

        GameObject Order = Instantiate(pre_order,canvas.transform.position+ new Vector3(880 + -220* Orders.Count, 350, 0), transform.rotation, canvas.transform);
        
        Order compOrder = Order.GetComponent<Order>();
        for (int i = 0; i < compOrder.Burger.Count; i++)
        {
            compOrder.Burger[i].color = new Color(0, 0, 0, 0);
        }
        for (int i = 0; Burger.Count > i; i++)
        {
            compOrder.Burger[i].color = Burger[i].color;
        }
        
        Orders.Add(Order);
    }

    public void ReShuffel()
    {
        for (int i = 0;i < Orders.Count; i++)
        {
            Orders[i].transform.position = canvas.transform.position + new Vector3(880 + -220 * i, 350, 0);
        }
    }
}
