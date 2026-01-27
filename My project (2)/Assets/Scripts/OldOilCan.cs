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
    private bool isBeingHeld = false;

    private LineRenderer oldOilHighlight;
    private LineRenderer spawnPointHighlight;
    private LineRenderer selfHighlight;

    private void Awake()
    {
        canRenderer = GetComponent<Renderer>();
        newOilCan.tag = "Untagged";
    }

    private void Update()
    {
        UpdateHighlights();
    }

    private void UpdateHighlights()
    {
        // Step 1: Not used yet - show Old Oil Can (self) and Old Oil target
        if (!hasBeenUsed)
        {
            // Show self highlight if not being held
            if (!isBeingHeld)
            {
                ShowHighlight(gameObject, ref selfHighlight);
            }
            else
            {
                HideHighlight(ref selfHighlight);
            }

            // Show old oil target
            if (oldOilObject != null && oldOilObject.activeInHierarchy)
            {
                ShowHighlight(oldOilObject, ref oldOilHighlight);
            }

            // Hide spawn point
            HideHighlight(ref spawnPointHighlight);
        }
        // Step 2: Used but not returned - show spawn point only
        else if (hasBeenUsed && !hasReturnedToSpawn)
        {
            HideHighlight(ref selfHighlight);
            HideHighlight(ref oldOilHighlight);

            if (oldCanSpawnPoint != null)
            {
                ShowHighlight(oldCanSpawnPoint, ref spawnPointHighlight);
            }
        }
        // Step 3: Completed - hide everything
        else
        {
            HideAllHighlights();
        }
    }

    private void ShowHighlight(GameObject target, ref LineRenderer highlight)
    {
        Collider col = target.GetComponent<Collider>();
        if (col == null) return;

        if (highlight == null)
        {
            GameObject lineObj = new GameObject("Highlight_" + target.name);
            highlight = lineObj.AddComponent<LineRenderer>();
            highlight.material = new Material(Shader.Find("Sprites/Default"));
            highlight.startColor = Color.green;
            highlight.endColor = Color.green;
            highlight.startWidth = 0.05f;
            highlight.endWidth = 0.05f;
            highlight.positionCount = 16;
            highlight.loop = true;
        }

        Vector3 center = col.bounds.center;
        Vector3 size = col.bounds.size;

        Vector3[] corners = new Vector3[16];
        corners[0] = center + new Vector3(-size.x, -size.y, -size.z) * 0.5f;
        corners[1] = center + new Vector3(size.x, -size.y, -size.z) * 0.5f;
        corners[2] = center + new Vector3(size.x, -size.y, size.z) * 0.5f;
        corners[3] = center + new Vector3(-size.x, -size.y, size.z) * 0.5f;
        corners[4] = center + new Vector3(-size.x, -size.y, -size.z) * 0.5f;
        corners[5] = center + new Vector3(-size.x, size.y, -size.z) * 0.5f;
        corners[6] = center + new Vector3(size.x, size.y, -size.z) * 0.5f;
        corners[7] = center + new Vector3(size.x, -size.y, -size.z) * 0.5f;
        corners[8] = center + new Vector3(size.x, size.y, -size.z) * 0.5f;
        corners[9] = center + new Vector3(size.x, size.y, size.z) * 0.5f;
        corners[10] = center + new Vector3(size.x, -size.y, size.z) * 0.5f;
        corners[11] = center + new Vector3(size.x, size.y, size.z) * 0.5f;
        corners[12] = center + new Vector3(-size.x, size.y, size.z) * 0.5f;
        corners[13] = center + new Vector3(-size.x, -size.y, size.z) * 0.5f;
        corners[14] = center + new Vector3(-size.x, size.y, size.z) * 0.5f;
        corners[15] = center + new Vector3(-size.x, size.y, -size.z) * 0.5f;

        highlight.SetPositions(corners);
    }

    private void HideHighlight(ref LineRenderer highlight)
    {
        if (highlight != null)
        {
            Destroy(highlight.gameObject);
            highlight = null;
        }
    }

    private void HideAllHighlights()
    {
        HideHighlight(ref selfHighlight);
        HideHighlight(ref oldOilHighlight);
        HideHighlight(ref spawnPointHighlight);
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
            HideAllHighlights();
            newOilCan.tag = "Grabable";
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

    public void SetHeld(bool held)
    {
        isBeingHeld = held;
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

    private void OnDestroy()
    {
        HideAllHighlights();
    }
}