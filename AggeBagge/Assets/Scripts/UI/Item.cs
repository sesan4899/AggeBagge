using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite icon = null;

    [Header("Stat Modifiers")]
    public float damage;
    public float speed;
    public float hp;


    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
}
