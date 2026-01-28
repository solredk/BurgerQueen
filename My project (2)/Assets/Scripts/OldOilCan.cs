using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OldOilCan : MonoBehaviour
{
    [SerializeField] private Material darkBrownMaterial;
    [SerializeField] private GameObject oldOilObject;
    [SerializeField] private float processDelay = 1.2f;
    [SerializeField] private GameObject oldCanSpawnPoint;
    [SerializeField] private GameObject newOilCan;
    [SerializeField] private SendMessage messageSystem;

    [Header("Events")]
    public UnityEvent OnOilCollected;
    public UnityEvent OnCanPlaced;

    private Renderer canRenderer;
    private bool hasBeenUsed = false;
    private bool hasReturnedToSpawn = false;
    private bool isBeingHeld = false;
    private bool canBeGrabbed = true;
    private bool isInSpawnPoint = false;
    private bool isInOldOil = false;
    private float oilContactTimer = 0f;

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
        CheckPlacement();
        CheckOilContact();
    }

    private void CheckOilContact()
    {
        if (isInOldOil && !hasBeenUsed)
        {
            oilContactTimer += Time.deltaTime;

            if (oilContactTimer >= processDelay)
            {
                hasBeenUsed = true;
                OnOilCollected.Invoke();
                StartCoroutine(DelayedProcessOil());
            }
        }
    }

    private void CheckPlacement()
    {
        if (hasBeenUsed && !hasReturnedToSpawn && isInSpawnPoint && !isBeingHeld)
        {
            hasReturnedToSpawn = true;
            canBeGrabbed = false;
            HideAllHighlights();
            newOilCan.tag = "Grabable";
            messageSystem.UpdateMessage("Drag the new oil can into the oil and fill it for 3 seconds");

            OnCanPlaced.Invoke();
        }
    }

    private void UpdateHighlights()
    {
        if (!hasBeenUsed)
        {
            if (!isBeingHeld)
            {
                ShowHighlight(gameObject, ref selfHighlight);
            }
            else
            {
                HideHighlight(ref selfHighlight);
            }

            if (oldOilObject != null && oldOilObject.activeInHierarchy)
            {
                ShowHighlight(oldOilObject, ref oldOilHighlight);
            }

            HideHighlight(ref spawnPointHighlight);
        }
        else if (hasBeenUsed && !hasReturnedToSpawn)
        {
            HideHighlight(ref selfHighlight);
            HideHighlight(ref oldOilHighlight);

            if (oldCanSpawnPoint != null)
            {
                ShowHighlight(oldCanSpawnPoint, ref spawnPointHighlight);
            }
        }
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
        Vector3 size = col.bounds.size * 0.5f;

        Vector3[] corners = GenerateBoxCorners(center, size);
        highlight.SetPositions(corners);
    }

    private Vector3[] GenerateBoxCorners(Vector3 center, Vector3 halfSize)
    {
        Vector3[] corners = new Vector3[16];

        int[] xSigns = { -1, 1, 1, -1, -1, -1, 1, 1, 1, 1, 1, 1, -1, -1, -1, -1 };
        int[] ySigns = { -1, -1, -1, -1, -1, 1, 1, -1, 1, 1, -1, 1, 1, -1, 1, 1 };
        int[] zSigns = { -1, -1, 1, 1, -1, -1, -1, -1, -1, 1, 1, 1, 1, 1, 1, -1 };

        for (int i = 0; i < 16; i++)
        {
            corners[i] = center + new Vector3(
                halfSize.x * xSigns[i],
                halfSize.y * ySigns[i],
                halfSize.z * zSigns[i]
            );
        }

        return corners;
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
            isInOldOil = true;
            oilContactTimer = 0f;
        }

        if (hasBeenUsed && !hasReturnedToSpawn && other.gameObject == oldCanSpawnPoint)
        {
            isInSpawnPoint = true;
            canBeGrabbed = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (hasBeenUsed && !hasReturnedToSpawn && other.gameObject == oldCanSpawnPoint)
        {
            isInSpawnPoint = true;
            canBeGrabbed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == oldOilObject)
        {
            isInOldOil = false;
            oilContactTimer = 0f;
        }

        if (other.gameObject == oldCanSpawnPoint)
        {
            isInSpawnPoint = false;
            if (!hasReturnedToSpawn)
            {
                canBeGrabbed = true;
            }
        }
    }

    private IEnumerator DelayedProcessOil()
    {
        oldOilObject.SetActive(false);
        canRenderer.material = darkBrownMaterial;
        yield return new WaitForSeconds(0.5f);
        messageSystem.UpdateMessage("Put the filled old oil can back to it's place");
    }

    public void SetHeld(bool held)
    {
        isBeingHeld = held;
    }

    public bool CanBeGrabbed()
    {
        return canBeGrabbed;
    }

    public void ResetCan(Material originalMaterial)
    {
        hasBeenUsed = false;
        hasReturnedToSpawn = false;
        isInSpawnPoint = false;
        isInOldOil = false;
        oilContactTimer = 0f;
        canBeGrabbed = true;
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