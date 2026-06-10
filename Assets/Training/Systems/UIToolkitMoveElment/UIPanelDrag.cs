namespace Systems.UIToolkitMoveElement
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;

    public class UIPanelDrag : MonoBehaviour
    {
        [SerializeField] Color normalColor;
        [SerializeField] Color hoverColor;
        [SerializeField] Color pressedColor;

        UIDocument uiDoc;
        VisualElement targetPanel;

        bool isDragging = false;
        Vector2 dragOffset;

        void Awake()
        {
            uiDoc = GetComponent<UIDocument>();
            targetPanel = uiDoc.rootVisualElement.Q<VisualElement>("target-drag-panel");

            // 押下したときの処理
            targetPanel.RegisterCallback<PointerDownEvent>( evt => {
                if(evt.button == 0)
                {
                    dragOffset = targetPanel.worldBound.position - (Vector2)evt.position;
                    targetPanel.style.backgroundColor = new StyleColor(pressedColor);
                    isDragging = true;
                } 
            });

            // UI内に侵入したときの処理
            targetPanel.RegisterCallback<PointerEnterEvent>( evt => {
                targetPanel.style.backgroundColor = new StyleColor(hoverColor);
            });

            // UI外に出たときの処理
            targetPanel.RegisterCallback<PointerOutEvent>( evt => {
                targetPanel.style.backgroundColor = new StyleColor(normalColor);
            });
        }

        void Update()
        {
            Drag();
        }

        /// <summary>
        /// 押下中にカーソルの位置に合わせて移動させる処理
        /// </summary>
        void Drag()
        {
            if(!isDragging) return;

            // なぜpositionまでじゃなくてReadValue()まで必要
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            Vector2 mousePosition = new Vector2(mouseScreenPosition.x, Screen.height - mouseScreenPosition.y);
            targetPanel.style.left = mousePosition.x + dragOffset.x;
            targetPanel.style.top = mousePosition.y + dragOffset.y;

            // ドラッグ終了の処理
            if(Mouse.current.leftButton.wasReleasedThisFrame)
            {
                // Element内にカーソルがあるか確認する
                if(targetPanel.worldBound.Contains(mousePosition))
                {
                    targetPanel.style.backgroundColor = new StyleColor(hoverColor);
                }
                else
                {
                    targetPanel.style.backgroundColor = new StyleColor(normalColor);
                }

                isDragging = false;
            }
        }

    }
}