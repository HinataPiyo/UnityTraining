using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class AddressableSOLoder : MonoBehaviour
{
	[SerializeField] string itemDataLabel = "ItemDataSO";       // Addressableアセットのラベル

	readonly List<ItemDataSO> loadedItems = new List<ItemDataSO>();
	AsyncOperationHandle<IList<ItemDataSO>> loadHandle;
	bool isLoaded;

	void Start()
	{
        // ラベルを指定して複数のItemDataSOをロードする
		loadHandle = Addressables.LoadAssetsAsync<ItemDataSO>(itemDataLabel, OnItemLoaded);
		loadHandle.Completed += OnLoadCompleted;
	}

    /// <summary>
    /// ItemDataSOが1つ読み込まれるたびに呼ばれるコールバックメソッド
    /// </summary>
	void OnItemLoaded(ItemDataSO item)
	{
		if (item == null)
		{
			return;
		}

		loadedItems.Add(item);
	}

    /// <summary>
    /// すべてのItemDataSOの読み込みが完了したときに呼ばれるコールバックメソッド
    /// </summary>
	void OnLoadCompleted(AsyncOperationHandle<IList<ItemDataSO>> handle)
	{
		if (handle.Status == AsyncOperationStatus.Succeeded)
		{
			isLoaded = true;
			Debug.Log($"アイテムデータの読み込みに成功！ {loadedItems.Count} 件のアイテムがロードされました。");

			foreach (var item in loadedItems)
			{
				Debug.Log($"- {item.itemName}");
			}

			return;
		}

		Debug.LogError($"アイテムデータの読み込みに失敗: {handle.OperationException}");
	}

    /// <summary>
    /// このスクリプトが破棄されるときに、ロードしたアセットを解放する
    /// </summary>
	void OnDestroy()
	{
		if (!isLoaded)
		{
			return;
		}

		if (loadHandle.IsValid())
		{
			Addressables.Release(loadHandle);
		}
	}
}