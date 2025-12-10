using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Glass : MonoBehaviour
{
    public bool Full = false;
    public GameObject Drink;
    public GameObject place;
    [SerializeField] private LayerMask filling;
    [SerializeField] private LayerMask filled;
    private void Update()
    {
        if (place != null)
        {
            if (!Full)
            {
                transform.position = place.transform.position + new Vector3(0, 0.25f, 0);
                gameObject.layer = filling.value-1;
            }
            else
            {
                gameObject.layer = filled.value -1;
                place = null;
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
