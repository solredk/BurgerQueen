using UnityEngine;

public class NewOil : MonoBehaviour
{
    [SerializeField] private Material newOilMaterial;
    [SerializeField] private bool makeGrabable = true;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        if (makeGrabable)
        {
            gameObject.tag = "Grabable";
        }
        
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && newOilMaterial != null)
        {
            renderer.material = newOilMaterial;
        }
    }
}