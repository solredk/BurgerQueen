using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Diagnostics;

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
        Instantiate(newOilPrefab, frituurObject.transform.position, Quaternion.identity);
        canRenderer.material = darkBrownMaterial;
        messageSystem.UpdateMessage("You have completed the replacing the old oil minigame");

        StartCoroutine(DelayedSceneChange());
    }

    private IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(sceneChangeDelay);
        SceneManager.LoadScene(0);
    }

    public void ResetCan()
    {
        hasBeenUsed = false;
        isInFrituur = false;
        triggerTimer = 0f;
        gameObject.SetActive(true);
    }
}