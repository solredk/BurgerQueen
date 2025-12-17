using UnityEngine;

public enum TableState
{
    Clean,
    Messy,
    Dirty
}

public class Table : MonoBehaviour
{
    [SerializeField] private float CleaningProgress = 0;

    private float dirtyingCounter;

    public bool isCleaning;

    public TableState CurrentState = TableState.Dirty;

    [Header("materials")]
    [SerializeField] private Material dirtyMaterial;
    [SerializeField] private Material messyMaterial;
    [SerializeField] private Material cleanMaterial;

    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();    
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case TableState.Dirty:
                renderer.material = dirtyMaterial;
                break;
            case TableState.Messy:
                renderer.material = messyMaterial;
                break;
            case TableState.Clean:
                renderer.material = cleanMaterial;
                break;
        }
        CleaningStateCheck();
        if (isCleaning)
        {
            CleanTable(0.1f);
        }

        if (CleaningProgress >= 1 && !isCleaning)
        DirtyProgress();
    }

    public void DirtyProgress()
    {
        dirtyingCounter += Time.deltaTime;
        if (dirtyingCounter >= 20f)
        {
            dirtyingCounter = 0f;
            CleaningProgress = 0f;
        }
    }

    public void CleanTable(float cleaningAmount)
    {
        CleaningProgress += cleaningAmount;
        if (isCleaning)
            dirtyingCounter = 0f;
    }

    private void CleaningStateCheck()
    {
        if (CleaningProgress >= 100)
        {
            CurrentState = TableState.Clean;
            CleaningProgress = 100;
        }

        else if (CleaningProgress >= 50)
            CurrentState = TableState.Messy;

        else if (CleaningProgress <= 0)
            CurrentState = TableState.Dirty;
    }
}
