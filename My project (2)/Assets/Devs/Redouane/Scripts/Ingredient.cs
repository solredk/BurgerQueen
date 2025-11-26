using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public enum StorageType
    {
        Warehouse,
        Freezer
    }
    public StorageType storageType;
    public string ingredientName;
    public int quantity;
    public Sprite icon;
    public string Description;
    
}
