using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


public class Frituur : MonoBehaviour
{
    public bool noFriesSpawned = true;
    public GameObject currentIngredient;
    [SerializeField] private GameObject friesObj;
    [SerializeField] public GameObject friedFriesobj;
    [SerializeField] private GameObject friesSpawnPos;
    private Grabing grabingS;


    void Start()
    {
        grabingS = FindAnyObjectByType<Grabing>();
        noFriesSpawned = true;
    }

    void Update()
    {

    }

    public void FriesButton()
    {
        if (noFriesSpawned)
        {
            currentIngredient = Instantiate(friesObj, friesSpawnPos.transform.position, Quaternion.identity);
            noFriesSpawned = false;
        }
    }

    public void TrashButton()
    {
        if (!noFriesSpawned)
        {
            Destroy(currentIngredient);
            noFriesSpawned = true;
        }
    }
}
