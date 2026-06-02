using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    UIDocument uiDoc;

    Button[] buttons;
    VisualElement focus;

    void Awake()
    {
        uiDoc = GetComponent<UIDocument>();
        buttons = uiDoc.rootVisualElement.Q<VisualElement>("buttons").Query<Button>().ToList().ToArray();
        focus = uiDoc.rootVisualElement.Q<VisualElement>("focus");

        for(int ii = 0; ii < buttons.Length; ii++)
        {
            int intdex = ii;
            buttons[intdex].clicked += () =>
            {
                // チュートリアルを完了したときの処理を登録
                TutorialManager.I.TutorialStepComplet(intdex);
                Debug.Log($"ボタンの番号:{intdex}");
            };
        }
    }

    /// <summary>
    /// ボタンをfocusする
    /// </summary>
    /// <param name="stepCount"></param>
    public void Focus(int stepCount)
    {
        Debug.Log("focusされる番号は" + stepCount);
        if(stepCount < 0 || stepCount >= buttons.Length)
        {
            Debug.LogError($"チュートリアルの数とボタンの数が一致しません。ボタン数:{buttons.Length}");
            return;
        }

        // もしまだfocusしているUIがあれば元に戻す
        CheckFocusTutorialUI();
        
        // focusする
        focus.Add(buttons[stepCount]);
        focus.style.display = DisplayStyle.Flex;
    }

    /// <summary>
    /// 他にUIをfocusしていないか確認する
    /// </summary>
    void CheckFocusTutorialUI()
    {
        VisualElement[] focused = focus.Query<VisualElement>(className: "tutorial").ToList().ToArray();
        if(focused.Length > 0)
        {
            VisualElement buttonContainer = uiDoc.rootVisualElement.Q<VisualElement>("buttons");
            foreach(VisualElement ui in focused)
            {
                Debug.Log(ui.name + "を戻します");
                buttonContainer.Add(ui);
            }
        }
    }

    /// <summary>
    /// focusUIを非表示にする
    /// </summary>
    public void DisableFocus()
    {
        CheckFocusTutorialUI();
        focus.style.display = DisplayStyle.None;
    }
}