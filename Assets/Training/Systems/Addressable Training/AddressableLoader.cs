using UnityEngine;
using UnityEngine.AddressableAssets; // 必須
using UnityEngine.ResourceManagement.AsyncOperations; // 必須

public class AddressableLoader : MonoBehaviour
{
    // 方法①：文字列（アドレス名）で指定する場合
    [SerializeField] private string addressName = "address";

    // 方法②：インスペクター上で安全にアセットを指定したい場合（推奨）
    [SerializeField] private AssetReference assetReference;

    void Start()
    {
        // 今回は「方法①：文字列」を使ってロードと生成を同時に行う
        // Addressables.InstantiateAsync(addressName).Completed += OnLoadCompleted;

        // 「方法②：AssetReference」を使う場合は以下のように書きます
        if(assetReference != null)
            assetReference.InstantiateAsync().Completed += OnLoadCompleted;
    }

    /// <summary>
    /// 読み込みが完了したときに呼ばれるコールバックメソッド
    /// </summary>
    /// <param name="handle"></param>
    void OnLoadCompleted(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("アセットの読み込みと生成に成功しました！");
            handle.Result.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-2f, 2f)); // ランダムな位置に配置
        }
        else
        {
            Debug.LogError("アセットの読み込みに失敗しました。");
        }
    }
}