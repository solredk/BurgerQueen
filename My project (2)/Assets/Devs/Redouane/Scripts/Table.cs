using UnityEngine;

public enum TableState
{
    Clean,
    Messy,
    Dirty
}
public class Table : MonoBehaviour
{
    private int CleaningProgress = 0;
    public TableState CurrentState = TableState.Dirty;

    public void CleanTable(int cleaningAmount)
    {
        CleaningProgress += cleaningAmount;
        if (CleaningProgress >= 100)
        {
            CurrentState = TableState.Clean;
            CleaningProgress = 100; // Cap at 100
        }
        else if (CleaningProgress >= 50)
        {
            CurrentState = TableState.Messy;
        }
    }
}
