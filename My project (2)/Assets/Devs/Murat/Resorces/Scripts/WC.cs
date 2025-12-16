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
    //spray game
    //flush game
    flushgame fleshgame;
    bool sprayG = false;
    bool flushG = false;
    bool flushing = false;
    public GameObject flushbutton;

    [SerializeField] private GameObject Twater;
    
    public List<Material> durtyness;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FlushGame();
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
                Twater.GetComponent<MeshRenderer>().material = durtyness[0];

                break;
            case flushgame.doused:
                if (flushing)
                {
                    fleshgame = flushgame.clean;
                }
                Twater.GetComponent<MeshRenderer>().material = durtyness[1];
                break;
            case flushgame.clean: flushG = true;
                Twater.GetComponent<MeshRenderer>().material = durtyness[2];
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
