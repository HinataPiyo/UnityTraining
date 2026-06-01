namespace Systems.Inventory
{
    using System.Collections.Generic;
    using UnityEngine;

    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager I { get; private set; }
        // 確認用でInspectorに表示
        [Header("インベントリ"), SerializeField] List<Entry> entries = new ();

        /// <summary>
        /// インベントリに保持するデータ
        /// </summary>
        [System.Serializable]
        public class Entry
        {
            public ItemDataSO data;
            public int Count { get; private set; }

            public Entry(ItemDataSO data)
            {
                this.data = data;
                Count++;
            }

            /// <summary>
            /// 所持数を加算用メソッド
            /// </summary>
            public void AddCount() => Count++;

            /// <summary>
            /// 所持数を減らすメソッド
            /// <see langword="true"/> 0より大きければtrueを返す 
            /// <see langword="false"/> 0以下になればfalseを返す
            /// </summary>
            public bool DecCount()
            {
                Count--;
                if(Count <= 0)
                {
                    return false;
                }

                return true;
            }
        }

        void Awake()
        {
            if (I == null) I = this;
        }

        /// <summary>
        /// アイテムを取得したときに呼び出す処理
        /// - 既に取得していたら加算
        /// - 未所持だった場合はデータを追加し+1する
        /// </summary>
        /// <param name="data">インベントリに登録するアイテムのデータ</param>
        public void SetInventory(ItemDataSO data)
        {
            foreach(Entry entry in entries)
            {
                // 既に所持していたら
                if(entry.data == data)
                {
                    entry.AddCount();
                    Debug.Log($"<{data.ItemName}>の加算が完了しました。現在の所持数[{entry.Count}]");
                    return;     // 加算したら処理を抜ける
                }
            }

            // 未所持だったら新規で追加
            Entry e = new Entry(data);
            entries.Add(e);
            Debug.Log($"<{data.ItemName}>の追加が完了しました。");
        }

        /// <summary>
        /// アイテムを取り出す処理
        /// </summary>
        /// <param name="data">取り出すアイテムのデータ</param>
        public void TakeOutItemData(ItemDataSO data)
        {
            for(int ii = entries.Count - 1; ii >= 0; ii--)
            {
                if(entries[ii].data == data)
                {
                    // 所持数を減らしつつまだ所持しているか確認する
                    bool isHas = entries[ii].DecCount();
                    Debug.Log($"<{data.ItemName}>の減算が完了しました。現在の所持数[{entries[ii].Count}]");

                    // 所持数がアイテムが0以下になったらインベントリから消す
                    if(!isHas) entries.Remove(entries[ii]);
                    return;
                }
            }

            Debug.Log($"該当のアイテムはインベントリ内にはありませんでした。[アイテム名] {data.ItemName}");
        }
    }
}