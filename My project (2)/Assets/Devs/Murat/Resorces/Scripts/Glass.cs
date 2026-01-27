using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Glass : MonoBehaviour
{
    public enum container
    {
        Drink,
        IceCreamCup,
        IceCreamCone
    }
    public container contaner;
    public bool Full = false;
    public bool Done = false;
    public bool Sprinkeled = false;
    public GameObject Drink;
    public GameObject place;
    public GameObject Ice;

    public int drinkNumber;

    private void Update()
    {
        if (place != null)
        {
            if (!Done)
            {
                transform.position = place.transform.position + new Vector3(0, 0, 0);
                gameObject.layer = 0;
                GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                gameObject.layer = 6;
                place.GetComponent<Snap>().Glass = null;
                place = null;
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Snap>())
        {
            if(!other.gameObject.GetComponent<Snap>().Glass|| other.gameObject.GetComponent<Snap>().Glass==gameObject)
            {
                place = other.gameObject;
                other.gameObject.GetComponent<Snap>().Glass = gameObject;
            }
            
        }
    }
}
