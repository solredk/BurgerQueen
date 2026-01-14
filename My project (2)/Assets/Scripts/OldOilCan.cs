using UnityEngine;
using System.Collections;

public class OldOilCan : MonoBehaviour
{
    [SerializeField] private Material darkBrownMaterial;
    [SerializeField] private GameObject oldOilObject;
    [SerializeField] private float processDelay = 0.5f;
    [SerializeField] private GameObject oldCanSpawnPoint;
    [SerializeField] private GameObject newOilCan;
    [SerializeField] private SendMessage messageSystem;

    private Renderer canRenderer;
    private bool hasBeenUsed = false;
    private bool hasReturnedToSpawn = false;

    private void Awake()
    {
        canRenderer = GetComponent<Renderer>();
        newOilCan.tag = "Untagged";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == oldOilObject && !hasBeenUsed)
        {
            hasBeenUsed = true;
            StartCoroutine(DelayedProcessOil());
        }

        if (hasBeenUsed && !hasReturnedToSpawn && other.gameObject == oldCanSpawnPoint)
        {
            hasReturnedToSpawn = true;
            newOilCan.tag = "Grabable";
            Debug.Log("Test");
            messageSystem.UpdateMessage("Drag the new oil can into the oil and fill it for 3 seconds");
        }
    }

    private IEnumerator DelayedProcessOil()
    {
        yield return new WaitForSeconds(processDelay);
        oldOilObject.SetActive(false);
        canRenderer.material = darkBrownMaterial;
        yield return new WaitForSeconds(processDelay);
        messageSystem.UpdateMessage("Put the filled old oil can back to it's place");
    }

    public void ResetCan(Material originalMaterial)
    {
        hasBeenUsed = false;
        hasReturnedToSpawn = false;
        canRenderer.material = originalMaterial;
        oldOilObject.SetActive(true);
        newOilCan.tag = "Untagged";
        messageSystem.UpdateMessage("Drag the Old Oil can into the Old oil");
    }
}