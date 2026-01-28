using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class NewOilCan : MonoBehaviour
{
    [SerializeField] private GameObject newOilObject;
    [SerializeField] private Material darkBrownMaterial;
    [SerializeField] private float requiredTriggerTime = 3f;
    [SerializeField] private GameObject frituurObject;
    [SerializeField] private GameObject newOilCanSpawnPoint;
    [SerializeField] private float sceneChangeDelay = 3f;
    [SerializeField] private SendMessage messageSystem;

    [Header("Events")]
    public UnityEvent OnOilPlaced;
    public UnityEvent OnCanPlaced;
    public UnityEvent OnTaskCompleted;

    private Renderer canRenderer;
    private float triggerTimer = 0f;
    private bool isInFrituur = false;
    private bool hasFilledOil = false;
    private bool hasReturnedToSpawn = false;
    private bool isBeingHeld = false;
    private bool canBeGrabbed = true;
    private bool isInSpawnPoint = false;

    private LineRenderer frituurHighlight;
    private LineRenderer selfHighlight;
    private LineRenderer spawnPointHighlight;

    private void Start()
    {
        canRenderer = GetComponent<Renderer>();
        if (newOilObject != null)
        {
            newOilObject.SetActive(false);
        }

        if (newOilCanSpawnPoint == null)
        {
            Debug.LogError("[NewOilCan] newOilCanSpawnPoint is not assigned!");
        }
    }

    private void Update()
    {
        if (isInFrituur && !hasFilledOil)
        {
            triggerTimer += Time.deltaTime;

            if (triggerTimer >= requiredTriggerTime)
            {
                EnableNewOil();
                hasFilledOil = true;
                canBeGrabbed = true; // Allow grabbing again after filling
            }
        }

        UpdateHighlights();
        CheckPlacement();
    }

    private void CheckPlacement()
    {
        if (hasFilledOil && !hasReturnedToSpawn && isInSpawnPoint && !isBeingHeld)
        {
            hasReturnedToSpawn = true;
            canBeGrabbed = false;
            HideAllHighlights();

            OnCanPlaced.Invoke();
            OnTaskCompleted.Invoke();

            messageSystem.UpdateMessage("You have completed the replacing the old oil minigame");

            StartCoroutine(DelayedSceneChange());
        }
    }

    private void UpdateHighlights()
    {
        bool isGrabable = gameObject.CompareTag("Grabable");

        // Phase 1: Before filling - show can + frituur
        if (!hasFilledOil && isGrabable)
        {
            if (!isBeingHeld)
            {
                ShowHighlight(gameObject, ref selfHighlight);
            }
            else
            {
                HideHighlight(ref selfHighlight);
            }

            if (frituurObject != null)
            {
                ShowHighlight(frituurObject, ref frituurHighlight);
            }

            HideHighlight(ref spawnPointHighlight);
        }
        // Phase 2: After filling - show BOTH can AND spawn point
        else if (hasFilledOil && !hasReturnedToSpawn)
        {
            // Always show spawn point
            if (newOilCanSpawnPoint != null)
            {
                ShowHighlight(newOilCanSpawnPoint, ref spawnPointHighlight);
            }

            // Show can when not being held
            if (!isBeingHeld)
            {
                ShowHighlight(gameObject, ref selfHighlight);
            }
            else
            {
                HideHighlight(ref selfHighlight);
            }

            HideHighlight(ref frituurHighlight);
        }
        // Phase 3: Complete - hide all
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
        HideHighlight(ref frituurHighlight);
        HideHighlight(ref spawnPointHighlight);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasFilledOil && other.gameObject == frituurObject)
        {
            isInFrituur = true;
            triggerTimer = 0f;
            canBeGrabbed = false;
        }

        if (hasFilledOil && !hasReturnedToSpawn && other.gameObject == newOilCanSpawnPoint)
        {
            isInSpawnPoint = true;
            canBeGrabbed = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Only block grabbing during filling phase
        if (!hasFilledOil && other.gameObject == frituurObject)
        {
            canBeGrabbed = false;
        }

        if (hasFilledOil && !hasReturnedToSpawn && other.gameObject == newOilCanSpawnPoint)
        {
            isInSpawnPoint = true;
            canBeGrabbed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == frituurObject)
        {
            isInFrituur = false;
            triggerTimer = 0f;
            // After filling, always allow grabbing when exiting frituur
            if (hasFilledOil)
            {
                canBeGrabbed = true;
            }
        }

        if (other.gameObject == newOilCanSpawnPoint)
        {
            isInSpawnPoint = false;
            if (!hasReturnedToSpawn)
            {
                canBeGrabbed = true;
            }
        }
    }

    private void EnableNewOil()
    {
        if (newOilObject != null)
        {
            newOilObject.SetActive(true);
        }

        canRenderer.material = darkBrownMaterial;

        OnOilPlaced.Invoke();

        messageSystem.UpdateMessage("Put the new oil can back to its place");
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

    public bool CanBeGrabbed()
    {
        return canBeGrabbed;
    }

    public void ResetCan()
    {
        hasFilledOil = false;
        hasReturnedToSpawn = false;
        isInFrituur = false;
        isInSpawnPoint = false;
        triggerTimer = 0f;
        canBeGrabbed = true;
        gameObject.SetActive(true);

        if (newOilObject != null)
        {
            newOilObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        HideAllHighlights();
    }
}