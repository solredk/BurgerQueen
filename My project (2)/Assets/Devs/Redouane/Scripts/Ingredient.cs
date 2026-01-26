using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public enum StorageType
    {
        Warehouse,
        Freezer
    }

    public string Name;
    public string Description;
    public int price;

    public int ID;

    public GameObject Prefab;
    public StorageType storageType;

    public Sprite Icon;    
    public int Quantity;
}
