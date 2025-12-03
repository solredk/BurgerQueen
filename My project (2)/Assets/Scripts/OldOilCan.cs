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

        if (newOilCan != null)
        {
            newOilCan.tag = "Untagged";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Old Oil" && !hasBeenUsed)
        {
            hasBeenUsed = true;
            StartCoroutine(DelayedProcessOil(other.gameObject));
        }

        if (hasBeenUsed && !hasReturnedToSpawn && other.gameObject.name == "OldCanSpawnPoint")
        {
            hasReturnedToSpawn = true;
            MakeNewOilCanGrabable();
        }

        if (hasBeenUsed && !hasReturnedToSpawn && oldCanSpawnPoint != null && other.gameObject == oldCanSpawnPoint)
        {
            hasReturnedToSpawn = true;
            MakeNewOilCanGrabable();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Old Oil" && !hasBeenUsed)
        {
            hasBeenUsed = true;
            StartCoroutine(DelayedProcessOil(collision.gameObject));
        }

        if (hasBeenUsed && !hasReturnedToSpawn && collision.gameObject.name == "OldCanSpawnPoint")
        {
            hasReturnedToSpawn = true;
            MakeNewOilCanGrabable();
        }

        if (hasBeenUsed && !hasReturnedToSpawn && oldCanSpawnPoint != null && collision.gameObject == oldCanSpawnPoint)
        {
            hasReturnedToSpawn = true;
            MakeNewOilCanGrabable();
        }
    }

    private void MakeNewOilCanGrabable()
    {
        if (newOilCan != null)
        {
            newOilCan.tag = "Grabable";
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