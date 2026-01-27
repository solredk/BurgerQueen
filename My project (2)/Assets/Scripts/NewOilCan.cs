using UnityEngine;
using System.Collections;

public class NewOilCan : MonoBehaviour
{
    [SerializeField] private GameObject newOilPrefab;
    [SerializeField] private Material darkBrownMaterial;
    [SerializeField] private float requiredTriggerTime = 3f;
    [SerializeField] private GameObject frituurObject;
    [SerializeField] private float sceneChangeDelay = 3f;
    [SerializeField] private SendMessage messageSystem;

    private Renderer canRenderer;
    private float triggerTimer = 0f;
    private bool isInFrituur = false;
    private bool hasBeenUsed = false;
    private bool isBeingHeld = false;

    private LineRenderer frituurHighlight;
    private LineRenderer selfHighlight;

    private void Start()
    {
        canRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (isInFrituur && !hasBeenUsed)
        {
            triggerTimer += Time.deltaTime;

            if (triggerTimer >= requiredTriggerTime)
            {
                SpawnNewOil();
                hasBeenUsed = true;
            }
        }

        UpdateHighlights();
    }

    private void UpdateHighlights()
    {
        // Only show highlights when can is Grabable and not yet used
        bool isGrabable = gameObject.CompareTag("Grabable");

        if (!hasBeenUsed && isGrabable)
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

            // Show frituur target
            if (frituurObject != null)
            {
                ShowHighlight(frituurObject, ref frituurHighlight);
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
        HideHighlight(ref frituurHighlight);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenUsed && other.gameObject == frituurObject)
        {
            isInFrituur = true;
            triggerTimer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == frituurObject && isInFrituur)
        {
            isInFrituur = false;
            triggerTimer = 0f;
        }
    }

    private void SpawnNewOil()
    {
        HideAllHighlights();
        Instantiate(newOilPrefab, frituurObject.transform.position, Quaternion.identity);
        canRenderer.material = darkBrownMaterial;
        messageSystem.UpdateMessage("You have completed the replacing the old oil minigame");

        StartCoroutine(DelayedSceneChange());
    }

    private IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(sceneChangeDelay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SetHeld(bool held)
    {
        isBeingHeld = held;
    }

    public void ResetCan()
    {
        hasBeenUsed = false;
        isInFrituur = false;
        triggerTimer = 0f;
        gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        HideAllHighlights();
    }
}   