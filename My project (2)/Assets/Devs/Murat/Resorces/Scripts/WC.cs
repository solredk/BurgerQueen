using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WC : MonoBehaviour
{
    enum flushgame
    {
        dirty,
        doused,
        clean
    }
    //spray game
    //flush game
    flushgame fleshgame;
    bool sprayG = false;
    bool flushG = false;
    public GameObject flushbutton;
    public GameObject Twater;
    public List<Material> durtyness;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FlushGame()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject == flushbutton)
                {

                }
            }
        }
        switch (fleshgame)
        {
            case flushgame.dirty:
                Twater.GetComponent<MeshRenderer>().material = durtyness[0];
                break;
            case flushgame.doused:
                Twater.GetComponent<MeshRenderer>().material = durtyness[1];
                break;
            case flushgame.clean: flushG = true;
                Twater.GetComponent<MeshRenderer>().material = durtyness[2];
                break;
            default: break;
        }


    }

}
