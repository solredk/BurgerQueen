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

    public bool cleaning;

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
        DirtyProgress();
    }

    public void DirtyProgress()
    {
        dirtyingCounter += Time.deltaTime;
        if (dirtyingCounter >= 20f)
        {
            CleaningProgress = 0f;
        }
        /*if (CurrentState != TableState.Dirty && dirtyTimer <= 0)
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
        }*/
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
        cleaning = false; ;
    }
}
