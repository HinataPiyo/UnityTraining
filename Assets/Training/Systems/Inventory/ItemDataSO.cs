namespace Systems.Inventory
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "ItemDataSO", menuName = "Systems/Inventory/ItemDataSO")]
    public class ItemDataSO : ScriptableObject
    {
        [SerializeField] string itemName;
        [SerializeField] string discription;

        public string ItemName => itemName;
    }
}