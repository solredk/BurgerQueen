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

    public int ID;

    public GameObject Prefab;
    public StorageType storageType;

    public int Quantity;
    public Sprite Icon;

    public int MaxQuantity = 100;
    public int PurchaseCost = 10;
}