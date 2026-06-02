using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager I { get; private set; }
    [SerializeField] UIController uiCTRL;
    [SerializeField] bool isTutorialReset = false;

    [SerializeField] TutorialStep[] Steps = new TutorialStep[4]
    {
        new TutorialStep { log = "チュートリアル ステップ1", isCompleted = false},
        new TutorialStep { log = "チュートリアル ステップ2", isCompleted = false},
        new TutorialStep { log = "チュートリアル ステップ3", isCompleted = false},
        new TutorialStep { log = "チュートリアルクリア！ ステップ4", isCompleted = false},
    };

    /// <summary>
    /// チュートリアルのステップ
    /// </summary>
    [System.Serializable]
    public class TutorialStep
    {
        public string log;
        public bool isCompleted;
    }

    void Awake()
    {
        if(I == null) I = this;

        if(isTutorialReset) ResetTutorial();
    }

    void Start()
    {
        NextStep(0);        // 最初は0番からスタート
    }

    /// <summary>
    /// チュートリアルをクリアしたときの処理
    /// </summary>
    public void TutorialStepComplet(int stepCount)
    {
        if(stepCount < 0 || stepCount >= Steps.Length)
        {
            Debug.LogError($"不正なステップ番号です: {stepCount}");
            return;
        }

        if(stepCount == Steps.Length - 1)
        {
            uiCTRL.DisableFocus();      // focusをしているUIを非表示にする
            Debug.Log("チュートリアルをクリアしました");
            ResetTutorial();            //! チュートリアルをリセットする
            return;
        }

        Debug.Log(Steps[stepCount]);
        NextStep(stepCount + 1);
    }

    /// <summary>
    /// 次のステップに移行
    /// </summary>
    void NextStep(int stepCount)
    {
        uiCTRL.Focus(stepCount);
        Debug.Log("次のステップへ移ります");
    }

    /// <summary>
    /// チュートリアルをリセットする
    /// </summary>
    void ResetTutorial()
    {
        foreach (var step in Steps)
        {
            step.isCompleted = false;
        }
    }
}
