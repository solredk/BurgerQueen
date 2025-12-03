using UnityEngine;
using System.Collections;

public class NewOilCan : MonoBehaviour
{
    [SerializeField] private GameObject newOilPrefab;
    [SerializeField] private float requiredTriggerTime = 3f;
    [SerializeField] private GameObject newOilCanSpawnPoint;

    private GameObject currentFrituurObject;
    private float triggerTimer = 0f;
    private bool isInFrituur = false;
    private bool hasBeenUsed = false;
    private bool hasReturnedToSpawn = false;

    private void Update()
    {
        // Only count timer while inside frituur and not yet used
        if (isInFrituur && !hasBeenUsed)
        {
            triggerTimer += Time.deltaTime;
            Debug.Log($"Pouring oil... {triggerTimer:F1}s / {requiredTriggerTime}s");

            // Spawn new oil after timer completes
            if (triggerTimer >= requiredTriggerTime)
            {
                SpawnNewOil();
                hasBeenUsed = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"NewOilCan triggered by: {other.gameObject.name}");

        // Check for Frituur collision
        if (!hasBeenUsed && other.gameObject.name == "Frituur")
        {
            Debug.Log("NewOilCan entered Frituur! Starting pour timer...");
            currentFrituurObject = other.gameObject;
            isInFrituur = true;
            triggerTimer = 0f; // Reset timer when entering
        }

        // Check for spawn point collision after oil has been used
        if (hasBeenUsed && !hasReturnedToSpawn && other.gameObject.name == "NewOilCanSpawnPoint")
        {
            Debug.Log("New Oil Can returned to spawn point!");
            hasReturnedToSpawn = true;
            OnSequenceComplete();
        }

        // Alternative: Also check by GameObject reference
        if (hasBeenUsed && !hasReturnedToSpawn && newOilCanSpawnPoint != null && other.gameObject == newOilCanSpawnPoint)
        {
            Debug.Log("New Oil Can returned to spawn point (by reference)!");
            hasReturnedToSpawn = true;
            OnSequenceComplete();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"NewOilCan exited trigger: {other.gameObject.name}");

        // Stop timer if exiting Frituur
        if (other.gameObject.name == "Frituur" && isInFrituur)
        {
            Debug.Log("NewOilCan left Frituur! Stopping pour timer.");
            isInFrituur = false;
            triggerTimer = 0f; // Reset timer when exiting
            currentFrituurObject = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"NewOilCan collision with: {collision.gameObject.name}");

        // Check for Frituur collision
        if (!hasBeenUsed && collision.gameObject.name == "Frituur")
        {
            Debug.Log("NewOilCan collision with Frituur! Starting pour timer...");
            currentFrituurObject = collision.gameObject;
            isInFrituur = true;
            triggerTimer = 0f;
        }

        // Check for spawn point collision after oil has been used
        if (hasBeenUsed && !hasReturnedToSpawn && collision.gameObject.name == "NewOilCanSpawnPoint")
        {
            Debug.Log("New Oil Can returned to spawn point!");
            hasReturnedToSpawn = true;
            OnSequenceComplete();
        }

        if (hasBeenUsed && !hasReturnedToSpawn && newOilCanSpawnPoint != null && collision.gameObject == newOilCanSpawnPoint)
        {
            Debug.Log("New Oil Can returned to spawn point (by reference)!");
            hasReturnedToSpawn = true;
            OnSequenceComplete();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Stop timer if exiting Frituur
        if (collision.gameObject.name == "Frituur" && isInFrituur)
        {
            Debug.Log("NewOilCan left Frituur collision! Stopping pour timer.");
            isInFrituur = false;
            triggerTimer = 0f;
            currentFrituurObject = null;
        }
    }

    private void SpawnNewOil()
    {
        Vector3 spawnPosition;

        // Spawn at Frituur position (since that's where the oil goes)
        if (currentFrituurObject != null)
        {
            spawnPosition = currentFrituurObject.transform.position;
        }
        else
        {
            // Fallback to can position
            spawnPosition = transform.position;
        }

        Debug.Log($"Spawning new oil at Frituur position: {spawnPosition}");
        Instantiate(newOilPrefab, spawnPosition, Quaternion.identity);

        Debug.Log("Oil pour complete! Return can to spawn point to finish sequence.");
    }

    private void OnSequenceComplete()
    {
        Debug.Log("New Oil Can sequence completed!");
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