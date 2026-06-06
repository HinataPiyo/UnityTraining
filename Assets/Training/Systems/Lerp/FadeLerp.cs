namespace Systems.LerpTraining
{
    using UnityEngine;
    
    public class FadeLerp : MonoBehaviour
    {
        [SerializeField] float duration = 1f;
        SpriteRenderer spriteRenderer;
        [SerializeField] Color targetColor;

        bool isComplete = false;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            var color = spriteRenderer.color;
            if(color.a >= 0.99f) isComplete = false;
            else if(color.a <= 0.01f) isComplete = true;

            // Lerpの引数は、現在の値、目標の値、そして時間の割合。
            // Time.deltaTime / durationは、毎フレームの時間を全体の時間で割ることで、0から1の範囲の値を生成します。
            color = Color.Lerp(color, isComplete ? targetColor : Color.clear, Time.deltaTime / duration);
            spriteRenderer.color = color;
        }
    }
}