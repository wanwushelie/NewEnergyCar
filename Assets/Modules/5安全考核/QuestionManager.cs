using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// 处理测验逻辑的类
public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class QuestionItem
    {
        public string question; // 题目
        public List<string> options = new List<string>(); // 选项列表
        public int correctAnswerIndex; // 正确答案的索引
        public int score = 5; // 每道题的得分
    }

    public Text questionText; // 显示题目的文本
    public List<Toggle> toggles; // 用于选项的切换
    public Button previousButton; // 上一题按钮
    public Button nextButton; // 下一题按钮
    public Button submitButton; // 提交答案按钮
    public List<QuestionItem> questions = new List<QuestionItem>(); // 问题列表
    private int currentQuestionIndex = 0; // 当前问题的索引

    private List<int> scoreTracker = new List<int>(); // 用于跟踪每道题目的得分情况
    public Text scoreText; // 用于显示分数的文本
    private int totalScore = 0; // 总分数

    // 初始化设置
    void Start()
    {
        DisplayCurrentQuestion();
        // 添加按钮点击事件
        previousButton.onClick.AddListener(LoadPreviousQuestion);
        nextButton.onClick.AddListener(LoadNextQuestion);
        submitButton.onClick.AddListener(SubmitAnswer);

        // 初始化得分跟踪
        scoreTracker = new List<int>(new int[questions.Count]);
    }

    // 显示当前问题及其选项
    void DisplayCurrentQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            QuestionItem currentQuestion = questions[currentQuestionIndex];

            // 为题目前添加序号
            questionText.text = $"{currentQuestionIndex + 1}. {currentQuestion.question}";

            for (int i = 0; i < toggles.Count; i++)
            {
                toggles[i].isOn = false; // 重置每个 Toggle 的状态
                if (i < currentQuestion.options.Count)
                {
                    // 为选项前添加 ABCD
                    char optionLabel = (char)('A' + i);
                    toggles[i].GetComponentInChildren<Text>().text = $"{optionLabel}. {currentQuestion.options[i]}";
                    toggles[i].gameObject.SetActive(true);
                }
                else
                {
                    toggles[i].gameObject.SetActive(false); // 隐藏未使用的 Toggle
                }
            }
        }
    }

    // 加载下一题
    void LoadNextQuestion()
    {
        CheckAnswer();
        currentQuestionIndex++;
        if (currentQuestionIndex >= questions.Count) // 边界检查
        {
            currentQuestionIndex = questions.Count - 1; // 强制设定为最后一题
        }
        DisplayCurrentQuestion();
    }

    // 加载上一题
    void LoadPreviousQuestion()
    {
        if (currentQuestionIndex > 0) // 确保不低于 0
        {
            currentQuestionIndex--;
        }
        DisplayCurrentQuestion();
    }

    // 提交答案并计算分数
    void SubmitAnswer()
    {
        CheckAnswer();
        // 计算总分
        totalScore = 0;
        foreach (int score in scoreTracker)
        {
            totalScore += score; // 统计所有题目的得分
        }

        // 更新分数显示
        scoreText.text = $"得分: {totalScore}";
        //打印分数
        Debug.Log("Total Score: " + totalScore);
    }

    // 检查用户的答案是否正确
    void CheckAnswer()
    {
        // 检查当前问题的正确答案
        QuestionItem currentQuestion = questions[currentQuestionIndex];

        // 找到用户选择的答案
        int selectedAnswerIndex = -1;
        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i].isOn) // 如果这个选项被选中
            {
                selectedAnswerIndex = i;
                break; // 找到后退出循环
            }
        }

        // 如果用户选择了答案，进行评分
        if (selectedAnswerIndex != -1)
        {
            if (selectedAnswerIndex == currentQuestion.correctAnswerIndex)
            {
                // 如果选择的答案正确，增加总分
                scoreTracker[currentQuestionIndex] = currentQuestion.score; // 记录本题得分
            }
        }
    }
}
