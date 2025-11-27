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

    public TableState CurrentState = TableState.Dirty;

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
