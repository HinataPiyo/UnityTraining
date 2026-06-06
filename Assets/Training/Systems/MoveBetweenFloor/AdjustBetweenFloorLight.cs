namespace Systems.MoveBetweenFloor
{
    using UnityEngine;
    using UnityEngine.Rendering.Universal;

    /// <summary>
    /// 部屋間を移動する時前後の部屋のライトの強さを調節する
    /// </summary>
    public class AdjustBetweenFloorLight : MonoBehaviour
    {
        [SerializeField] Light2D floor_0;
        [SerializeField] Light2D floor_1;
        int isMoveDistance;
        SpriteRenderer playerSprite;

        void OnTriggerEnter2D(Collider2D col)
        {
            playerSprite = col.GetComponentInChildren<SpriteRenderer>();
        }

        void OnTriggerStay2D(Collider2D col)
        {
            if(playerSprite == null) return;

            // プレイヤーがフロアの境界にいるとき、プレイヤーの位置に応じてライトの強さを調整する
            isMoveDistance = transform.position.x < col.transform.position.x ? 1 : -1;
            // プレイヤーがフロアの境界に近いほど両方を半分ずつ点灯し、
            // 離れるほど片側を強くする
            float distanceToMainFloor = Mathf.Abs(transform.position.x - col.transform.position.x);
            float maxDistance = 0.3f; // ライトの強さが0になる距離
            float blend = Mathf.Clamp01(distanceToMainFloor / maxDistance);

            if (isMoveDistance == 1)
            {
                playerSprite.sortingLayerName = "MainObject";
                // プレイヤーが右側にいる場合はメインフロアのライトを点灯
                floor_0.intensity = Mathf.Lerp(0.5f, 1f, blend);
                floor_1.intensity = Mathf.Lerp(0.5f, 0f, blend);
            }
            else
            {
                playerSprite.sortingLayerName = "SubObject";
                // プレイヤーが左側にいる場合はサブフロアのライトを点灯
                floor_0.intensity = Mathf.Lerp(0.5f, 0f, blend);
                floor_1.intensity = Mathf.Lerp(0.5f, 1f, blend);
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if(isMoveDistance == 1)
            {
                // プレイヤーが右側にいる場合はメインフロアのライトを点灯
                floor_0.intensity = 1f;
                floor_1.intensity = 0f;
            }
            else
            {
                // プレイヤーが左側にいる場合はサブフロアのライトを点灯
                floor_0.intensity = 0f;
                floor_1.intensity = 1f;
            }

            playerSprite = null;
        }
    }
}