using Unity.VisualScripting;
using UnityEngine;

public class WCWater : MonoBehaviour
{
    [SerializeField] private int dif;
    public bool progres = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (dif)
        {
            case 0:
                if (other.tag=="CleanWater")
                {
                    progres = true;
                }
                break;
            case 1:
                if (other.tag == "CleanWC")
                {
                    progres = true;
                }
                break;
            default: break;
        }
    }
}
