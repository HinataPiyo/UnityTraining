namespace Systems.UIToolkitDrag_Drop
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UIElements;

    public class ItemController : MonoBehaviour
    {
        UIDocument uiDoc;
        VisualElement container;
        Vector2 dragOffset;
        Button draggingButton;
        bool isDragging;
        List<Button> buttons = new List<Button>();

        void Awake()
        {
            uiDoc = GetComponent<UIDocument>();
            container = uiDoc.rootVisualElement.Q("container");
            buttons = container.Query<Button>(className: "button").ToList();

            // 各記号にドラッグ開始イベントを登録
            for(int i = 0; i < buttons.Count; i++)
            {
                int index = i; // クロージャー対策
                Button button = buttons[index];
                button.text = $"{index + 1}";
                button.RegisterCallback<PointerDownEvent>((evt) =>
                {
                    dragOffset = button.worldBound.position - (Vector2)evt.position;
                    StartPartDrag(button);
                }, TrickleDown.TrickleDown);
            }
        }

        /// <summary>
        /// ドラッグ開始処理。ドラッグする記号を保持して、ドラッグ状態に遷移する
        /// </summary>
        void StartPartDrag(Button button)
        {
            draggingButton = button;
            isDragging = true;
        }

        void Update()
        {
            if(isDragging)
            {
                Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
                Vector2 mousePosition = new Vector2(mouseScreenPosition.x, Screen.height - mouseScreenPosition.y);
                Vector2 containerPosition = container.worldBound.position;

                draggingButton.style.position = Position.Absolute;
                draggingButton.style.left = mousePosition.x + dragOffset.x - containerPosition.x;
                draggingButton.style.top = mousePosition.y + dragOffset.y - containerPosition.y;

                // ドロップ処理
                if(Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    PartDrop(draggingButton);
                    ResetAllButtonMargins();
                    ResetDraggedButtonStyle(draggingButton);
                    isDragging = false;
                    draggingButton = null;
                }
            }
        }

        /// <summary>
        /// ドラッグ終了後、スタイルをリセットして次のドラッグに備える
        /// </summary>
        void ResetDraggedButtonStyle(Button button)
        {
            button.style.position = Position.Relative;
            button.style.left = StyleKeyword.Null;
            button.style.top = StyleKeyword.Null;
            button.style.right = StyleKeyword.Null;
            button.style.bottom = StyleKeyword.Null;
        }

        /// <summary>
        /// 全ての記号のマージンをリセットして、ドラッグ中の記号のスペースを確保する
        /// </summary>
        void ResetAllButtonMargins()
        {
            foreach (var equip in buttons)
            {
                equip.style.marginLeft = StyleKeyword.Null;
                equip.style.marginRight = StyleKeyword.Null;
            }
        }

        /// <summary>
        /// ドラッグしている記号を他の記号の間にドロップする
        /// </summary>
        /// <param name="button"></param>
        void PartDrop(Button button)
        {
            List<Button> dropTargets = container.Query<Button>(className: "button").ToList();
            dropTargets.Remove(button);
            // ドロップターゲットがない場合はドロップ処理を行わない
            if (dropTargets.Count == 0)
            {
                return;
            }

            float dropX = button.worldBound.center.x;
            int insertIndex = dropTargets.Count;

            // ドロップ位置がどの記号の間に入るかを判定
            for (int i = 0; i < dropTargets.Count; i++)
            {
                // ドロップ位置がターゲットの左側にある場合、そのターゲットの前に挿入
                if (dropX < dropTargets[i].worldBound.center.x)
                {
                    insertIndex = i;
                    break;
                }

                // ドロップ位置がターゲットの右側にある場合、そのターゲットの後ろに挿入
                if (i < dropTargets.Count - 1)
                {
                    // 計算式: ドロップ位置がターゲットの右側にあるかつ次のターゲットの左側にある場合、そのターゲットの後ろに挿入
                    float gapCenterX = (dropTargets[i].worldBound.xMax + dropTargets[i + 1].worldBound.xMin) * 0.5f;

                    // ドロップ位置がギャップの左側にある場合、そのターゲットの後ろに挿入
                    if (dropX < gapCenterX)
                    {
                        insertIndex = i + 1;
                        break;
                    }
                }
            }

            button.RemoveFromHierarchy();   // ドロップ前に一度親から外す
            container.Insert(insertIndex, button);       // ドロップ位置に挿入
        }
    }
}