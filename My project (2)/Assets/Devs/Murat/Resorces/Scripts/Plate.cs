using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField]private
    List<GameObject> pos;
    [SerializeField]
    private
    List<GameObject> meat;
    public float up;
    private void Update()
    {
        int counting = 0;
        int tell = 0;
        if (meat[0]!=null)
        {
            for (int i = 0; i < meat.Count; i++)
            {
                meat[i].transform.position = new Vector3(pos[counting].transform.position.x, pos[counting].transform.position.y + (up * tell), pos[counting].transform.position.z);
                counting++;
                if (counting == 4)
                {
                    counting = 0;
                    tell++;
                }
            }
        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Meat")
        {
            Debug.Log(collision.gameObject);
            if (meat.Contains(collision.gameObject))
            {

            }
            else
            {
                meat.Add(collision.gameObject);
            }
            
        }
    }
}
