using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rejester : MonoBehaviour
{
    public List<Image> Burger;
    public List<Image> CompBurger;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
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
    }
}
