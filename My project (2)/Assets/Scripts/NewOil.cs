using UnityEngine;

public class NewOil : MonoBehaviour
{
    [SerializeField] private Material newOilMaterial;
    [SerializeField] private bool makeGrabable = true;
    [SerializeField] private Transform spawnPoint;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake()
    {
        // Store original position if no spawn point is set
        if (spawnPoint == null)
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;
        }

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

    public void ResetPosition()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
        else
        {
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }
}