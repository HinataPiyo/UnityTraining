namespace Systems.LerpTraining
{
    using UnityEngine;
    
    public class MoveLerp : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float distance = 3f;

        bool isComplete = false;

        void Update()
        {
            // Lerpの引数は、現在の値、目標の値、そして時間の割合。
            // Time.deltaTime * speedは、毎フレームの時間にスピードを掛けることで、0から1の範囲の値を生成します。
            if(transform.position.x >= distance * 0.99f) isComplete = false;
            else if(transform.position.x <= distance * -0.99f) isComplete = true;
            var direction = isComplete ? 1 : -1;
            Vector3 targetPosition = new Vector3(distance * direction, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        }
    }
}