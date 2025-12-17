using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoints : MonoBehaviour
{
    public GameObject endPoint;
    private GameObject player;
    [SerializeField] private List<GameObject> maps = new List<GameObject>();
    [SerializeField] private int m_Map;

    private void Start()
    {
        player = FindFirstObjectByType<StationSwitchPlayer>().gameObject;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject ==  player)
        {
            int i = 0;
            foreach(GameObject map in maps)
            {
                if( i != m_Map)
                {
                    maps[i].SetActive(false);
                }
                else
                {
                    maps[i].SetActive(true);
                }
                i++;
            }
        }
    }

}
