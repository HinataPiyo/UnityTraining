namespace Systems.Inventory
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    
    public class PlayerEvent : MonoBehaviour
    {
        [SerializeField] ItemDataSO[] database;
        void Update()
        {
            if(Keyboard.current.digit1Key.wasReleasedThisFrame)
            {
                // アイテムを入れる
                if(database[0] != null) InventoryManager.I.SetInventory(database[0]);
            }
            else if(Keyboard.current.digit2Key.wasReleasedThisFrame)
            {
                // アイテムを入れる
                if(database[1] != null) InventoryManager.I.SetInventory(database[1]);
            }

            if(Keyboard.current.digit0Key.wasReleasedThisFrame)
            {
                // アイテムを取り出す
                if(database[0] != null) InventoryManager.I.TakeOutItemData(database[0]);
            }
            else if(Keyboard.current.digit9Key.wasReleasedThisFrame)
            {
                if(database[1] != null) InventoryManager.I.TakeOutItemData(database[1]);
            }

        }
    }
}