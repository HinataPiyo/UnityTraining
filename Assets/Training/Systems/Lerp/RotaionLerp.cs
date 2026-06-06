namespace Systems.LerpTraining
{
    using UnityEngine;
    
    public class RotaionLerp : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float angle = 90f;

        [SerializeField] bool isFlip = false;

        float targetZAngle;

        void Start()
        {
            targetZAngle = transform.eulerAngles.z;
        }

        void Update()
        {
            // Lerpの引数は、現在の値、目標の値、そして時間の割合。
            // Time.deltaTime * speedは、毎フレームの時間にスピードを掛けることで、0から1の範囲の値を生成します。
            var direction = isFlip ? 1 : -1;
            // Quaternion.Eulerは、オイラー角（x, y, z）をクォータニオンに変換するための関数。
            // ここでは、2Dなので、xとyはそのままにして、z軸の回転だけを変更しています。

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetZAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);

            // Quaternion.Angleは、2つのクォータニオンの間の角度を度単位で返す関数。
            // つまり、現在の回転と目標の回転がどれだけ近いかを測ることができる。
            if (Quaternion.Angle(transform.rotation, targetRotation) <= 0.1f)
            {
                targetZAngle += angle * direction;
            }
        }
        
    }
}