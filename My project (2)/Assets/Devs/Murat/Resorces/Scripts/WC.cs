using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WC : MonoBehaviour
{
    enum flushgame
    {
        dirty,
        doused,
        clean
    }
    enum spraygame
    {
        dirty,
        Sprayed,
        clean
    }

    flushgame fleshgame;

    spraygame Spraygame;
    bool sprayG = false;
    bool flushG = false;
    bool flushing = false;
    public GameObject flushbutton;

    [SerializeField] private GameObject Twater;
    [SerializeField] private GameObject Lid;
    [SerializeField] private GameObject Rim;

    public List<Material> durtynessWater; 
    public List<Material> durtyness;
    public List<WCWater> water;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FlushGame();
        CleanGame();
        if(flushG&&sprayG)
        {
            //win
        }
    }
    void CleanGame()
    {
        switch (Spraygame)
        {
            case spraygame.dirty:
                Lid.GetComponent<MeshRenderer>().material = durtyness[0];
                Rim.GetComponent<MeshRenderer>().material = durtyness[0];
                int show = 0;
                for (int i = 0; i < water.Count; i++)
                {
                    if (water[i].progres)
                    {
                        show++;
                    }
                }
                if (show >= 3)
                {
                    Spraygame = spraygame.Sprayed;
                }
                break;
            case spraygame.Sprayed:
                Lid.GetComponent<MeshRenderer>().material = durtyness[1];
                Rim.GetComponent<MeshRenderer>().material = durtyness[1];
                break;
            case spraygame.clean:
                Lid.GetComponent<MeshRenderer>().material = durtyness[2];
                Rim.GetComponent<MeshRenderer>().material = durtyness[2];
                sprayG = true;
                break;
            default: break;
        }
    }
    void FlushGame()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject == flushbutton&& !flushing)
                {
                    StartCoroutine(Flushing());
                }
            }
        }
        switch (fleshgame)
        {
            case flushgame.dirty:
                Twater.GetComponent<MeshRenderer>().material = durtynessWater[0];
                if (Twater.GetComponent<WCWater>().progres)
                {
                    fleshgame = flushgame.doused;
                }
                
                break;
            case flushgame.doused:
                if (flushing)
                {
                    fleshgame = flushgame.clean;
                }
                Twater.GetComponent<MeshRenderer>().material = durtynessWater[1];
                break;
            case flushgame.clean: flushG = true;
                Twater.GetComponent<MeshRenderer>().material = durtynessWater[2];
                break;
            default: break;
        }


    }
    private IEnumerator Flushing()
    {
        flushing = true;
        GameObject pivit = flushbutton.transform.parent.gameObject;
        
        while (pivit.transform.rotation.z <= 0.35f)
        {
            pivit.transform.Rotate(0,0, 1);
            yield return new WaitForSeconds(0.001f);
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        while (pivit.transform.rotation.z >= -0.35f)
        {
            pivit.transform.Rotate(0, 0, -1);
            yield return new WaitForSeconds(0.0001f);
            yield return null;
        }
        Debug.Log("done");
        flushing = false;
    }

}
