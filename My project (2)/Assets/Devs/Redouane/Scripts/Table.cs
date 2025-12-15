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

    private float dirtyTimer;

    public TableState CurrentState = TableState.Dirty;

    [Header("materials")]
    [SerializeField] private Material dirtyMaterial;
    [SerializeField] private Material messyMaterial;
    [SerializeField] private Material cleanMaterial;

    private void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
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
        //DirtyProgress();
    }

    private void DirtyProgress()
    {
        if (CurrentState != TableState.Dirty && dirtyTimer <= 0)
        {
            dirtyTimer = Random.Range(30f, 60f);
        }

        if (TableState.Clean == CurrentState)
        {
            dirtyTimer -= Time.deltaTime;
            if (dirtyTimer >= 10f)
            {
                CurrentState = TableState.Messy;
                dirtyTimer = 0f;
            }
        }

        else if (TableState.Messy == CurrentState)
        {
            dirtyTimer += Time.deltaTime;
            if (dirtyTimer >= 10f)
            {
                CurrentState = TableState.Dirty;
                dirtyTimer = 0f;
            }
        }
    }

    public void CleanTable(float cleaningAmount)
    {
        CleaningProgress += cleaningAmount;

        if (CleaningProgress >= 100)
        {
            CurrentState = TableState.Clean;
            CleaningProgress = 100;
        }

        else if (CleaningProgress >= 50)
            CurrentState = TableState.Messy;
    }
}
