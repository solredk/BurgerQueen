using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewOilCan : MonoBehaviour
{
    [SerializeField] private GameObject newOilPrefab;
    [SerializeField] private float requiredTriggerTime = 3f;
    [SerializeField] private GameObject newOilCanSpawnPoint;
    [SerializeField] private GameObject frituurObject;
    [SerializeField] private float sceneChangeDelay = 3f;

    private GameObject currentFrituurObject;
    private float triggerTimer = 0f;
    private bool isInFrituur = false;
    private bool hasBeenUsed = false;
    private bool hasReturnedToSpawn = false;

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
        if (!hasBeenUsed && (other.gameObject.name == "Frituur" || (frituurObject != null && other.gameObject == frituurObject)))
        {
            currentFrituurObject = other.gameObject;
            isInFrituur = true;
            triggerTimer = 0f;
        }

        if (hasBeenUsed && !hasReturnedToSpawn && other.gameObject.name == "NewOilCanSpawnPoint")
        {
            hasReturnedToSpawn = true;
            OnSequenceComplete();
        }

        if (hasBeenUsed && !hasReturnedToSpawn && newOilCanSpawnPoint != null && other.gameObject == newOilCanSpawnPoint)
        {
            hasReturnedToSpawn = true;
            OnSequenceComplete();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.name == "Frituur" || (frituurObject != null && other.gameObject == frituurObject)) && isInFrituur)
        {
            isInFrituur = false;
            triggerTimer = 0f;
            currentFrituurObject = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenUsed && (collision.gameObject.name == "Frituur" || (frituurObject != null && collision.gameObject == frituurObject)))
        {
            currentFrituurObject = collision.gameObject;
            isInFrituur = true;
            triggerTimer = 0f;
        }

        if (hasBeenUsed && !hasReturnedToSpawn && collision.gameObject.name == "NewOilCanSpawnPoint")
        {
            hasReturnedToSpawn = true;
            OnSequenceComplete();
        }

        if (hasBeenUsed && !hasReturnedToSpawn && newOilCanSpawnPoint != null && collision.gameObject == newOilCanSpawnPoint)
        {
            hasReturnedToSpawn = true;
            OnSequenceComplete();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((collision.gameObject.name == "Frituur" || (frituurObject != null && collision.gameObject == frituurObject)) && isInFrituur)
        {
            isInFrituur = false;
            triggerTimer = 0f;
            currentFrituurObject = null;
        }
    }

    private void SpawnNewOil()
    {
        Vector3 spawnPosition;

        if (frituurObject != null)
        {
            spawnPosition = frituurObject.transform.position;
        }
        else if (currentFrituurObject != null)
        {
            spawnPosition = currentFrituurObject.transform.position;
        }
        else
        {
            spawnPosition = transform.position;
        }

        Instantiate(newOilPrefab, spawnPosition, Quaternion.identity);

        
        StartCoroutine(DelayedSceneChange());
    }

    private IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(sceneChangeDelay);
        SceneManager.LoadScene(0);
    }

    private void OnSequenceComplete()
    {

    }

    public void ResetCan()
    {
        hasBeenUsed = false;
        hasReturnedToSpawn = false;
        isInFrituur = false;
        triggerTimer = 0f;
        currentFrituurObject = null;
        gameObject.SetActive(true);
    }
}