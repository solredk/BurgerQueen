using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    public List<Material> Heated;
    [SerializeField] private float timer;
    private bool toutingGrill =false;
    private void Update()
    {
        if (toutingGrill)
        {
            if (timer < 5)
            {
                gameObject.GetComponent<MeshRenderer>().material = Heated[0];
            }
            else if (5 <= timer && timer <= 15)
            {
                gameObject.GetComponent<MeshRenderer>().material = Heated[1];
            }
            else if (timer > 15)
            {
                gameObject.GetComponent<MeshRenderer>().material = Heated[2];
            }
            timer += Time.deltaTime;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Grill")
        {
            toutingGrill =true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Grill")
        {
            toutingGrill = false;
        }
    }
}
