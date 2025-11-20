using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public List<Image> Burger;
    Rejester Rejester;
    private void Start()
    {
        Rejester = FindObjectOfType<Rejester>();
    }
    public void Complete()
    {
        Rejester.Orders.Remove(gameObject);
        Rejester.ReShuffel();
        gameObject.SetActive(false);
        Destroy(gameObject,0.1f);
    }
}
