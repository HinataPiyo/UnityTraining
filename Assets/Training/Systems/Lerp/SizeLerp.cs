namespace Systems.LerpTraining
{
    using UnityEngine;
    
    public class SizeLerp : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] float targetScale = 2f;

        bool isComplete = false;
        float originalScale;
        void Start()
        {
            originalScale = transform.localScale.z;
        }

        void Update()
        {
            // Lerpの引数は、現在の値、目標の値、そして時間の割合。
            // Time.deltaTime * speedは、毎フレームの時間にスピード
            if(transform.localScale.x >= targetScale * 0.99f) isComplete = false;
            else if(transform.localScale.x <= originalScale) isComplete = true;
            int direction = isComplete ? 1 : -1;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * targetScale * direction, Time.deltaTime * speed);
        }
    }
}