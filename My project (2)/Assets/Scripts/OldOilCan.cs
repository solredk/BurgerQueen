using UnityEngine;
using System.Collections;

public class OldOilCan : MonoBehaviour
{
    [SerializeField] private Material darkBrownMaterial;
    [SerializeField] private GameObject oldOilObject;
    [SerializeField] private float processDelay = 0.5f;
    [SerializeField] private GameObject oldCanSpawnPoint;
    [SerializeField] private GameObject newOilCan;

    private Renderer canRenderer;
    private bool hasBeenUsed = false;
    private bool hasReturnedToSpawn = false;

    private void Awake()
    {
        canRenderer = GetComponent<Renderer>();

        // Ensure NewOilCan starts without Grabable tag
        if (newOilCan != null)
        {
            newOilCan.tag = "Untagged";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OldOilCan triggered by: {other.gameObject.name}");

        if (other.gameObject.name == "Old Oil" && !hasBeenUsed)
        {
            Debug.Log("Hit Old Oil! Processing collision...");
            hasBeenUsed = true;
            StartCoroutine(DelayedProcessOil(other.gameObject));
        }

        // Check spawn point collision using name instead of reference
        if (hasBeenUsed && !hasReturnedToSpawn && other.gameObject.name == "OldCanSpawnPoint")
        {
            Debug.Log("Old Oil Can returned to spawn point!");
            hasReturnedToSpawn = true;
            MakeNewOilCanGrabable();
        }

        // Alternative: Also check by GameObject reference if name doesn't work
        if (hasBeenUsed && !hasReturnedToSpawn && oldCanSpawnPoint != null && other.gameObject == oldCanSpawnPoint)
        {
            Debug.Log("Old Oil Can returned to spawn point (by reference)!");
            hasReturnedToSpawn = true;
            MakeNewOilCanGrabable();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"OldOilCan collision with: {collision.gameObject.name}");

        if (collision.gameObject.name == "Old Oil" && !hasBeenUsed)
        {
            Debug.Log("Collision with Old Oil! Processing...");
            hasBeenUsed = true;
            StartCoroutine(DelayedProcessOil(collision.gameObject));
        }

        // Check spawn point collision using name
        if (hasBeenUsed && !hasReturnedToSpawn && collision.gameObject.name == "OldCanSpawnPoint")
        {
            Debug.Log("Old Oil Can returned to spawn point!");
            hasReturnedToSpawn = true;
            MakeNewOilCanGrabable();
        }

        // Alternative: Also check by GameObject reference
        if (hasBeenUsed && !hasReturnedToSpawn && oldCanSpawnPoint != null && collision.gameObject == oldCanSpawnPoint)
        {
            Debug.Log("Old Oil Can returned to spawn point (by reference)!");
            hasReturnedToSpawn = true;
            MakeNewOilCanGrabable();
        }
    }

    private void MakeNewOilCanGrabable()
    {
        if (newOilCan != null)
        {
            newOilCan.tag = "Grabable";
            Debug.Log($"New Oil Can is now draggable! Tag set to: {newOilCan.tag}");
        }
        else
        {
            Debug.LogWarning("New Oil Can reference is missing!");
        }
    }

    private IEnumerator DelayedProcessOil(GameObject hitOilObject)
    {
        yield return new WaitForSeconds(processDelay);

        if (oldOilObject != null)
        {
            oldOilObject.SetActive(false);
        }
        else
        {
            hitOilObject.SetActive(false);
        }

        if (canRenderer != null && darkBrownMaterial != null)
        {
            canRenderer.material = darkBrownMaterial;
        }

        Debug.Log("Oil processed. Return to spawn point to continue.");
    }

    public void ResetCan(Material originalMaterial)
    {
        hasBeenUsed = false;
        hasReturnedToSpawn = false;

        if (canRenderer != null && originalMaterial != null)
        {
            canRenderer.material = originalMaterial;
        }

        if (oldOilObject != null)
        {
            oldOilObject.SetActive(true);
        }

        if (newOilCan != null)
        {
            newOilCan.tag = "Untagged";
        }
    }
}