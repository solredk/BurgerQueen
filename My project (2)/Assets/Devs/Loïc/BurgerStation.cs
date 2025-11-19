using System.Runtime.CompilerServices;
using UnityEngine;

public class BurgerStation : MonoBehaviour
{
    private bool handEmpty = false;
    [SerializeField] private GameObject breadObj;
    private Vector3 mousePos = Input.mousePosition;
    //private float mouseY = Input.mousePosition.y;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void BroodButton()
    {
       print(mousePos);
       // Instantiate(breadObj,new Vector3(Input.mousePosition.x, Input.mousePosition.y), Quaternion.identity);
    }
}
