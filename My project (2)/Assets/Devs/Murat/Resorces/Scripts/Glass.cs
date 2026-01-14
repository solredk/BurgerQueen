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
    public GameObject Drink;
    public GameObject place;
    public GameObject Ice;
    [SerializeField] private LayerMask filling;
    [SerializeField] private LayerMask filled;
    private void Update()
    {
        if (place != null)
        {
            if (!Full)
            {
                transform.position = place.transform.position + new Vector3(0, 0, 0);
                gameObject.layer = filling.value;
                GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log("not full");
            }
            else
            {
                Debug.Log("filled");
                gameObject.layer = filled.value;
                place = null;
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Snap")
        {
            place = other.gameObject;
        }
    }
}
