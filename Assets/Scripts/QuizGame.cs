using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizGame : MonoBehaviour
{
    #region Variables

    [Header("Quiz Settings")]
    [SerializeField] private QuizQuestion[] _questions;
    [SerializeField] private Image _questionImage;
    [SerializeField] private Button[] _answerButtons;
    [SerializeField] private TextMeshProUGUI[] _answerTexts;
    [SerializeField] private TextMeshProUGUI _questionNumberText; // Новое поле для отображения номера вопроса

    private int _currentQuestionIndex;
    private int _score;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        // Перемешивание вопросов перед началом игры
        ShuffleQuestions();

        for (int i = 0; i < _answerButtons.Length; i++)
        {
            int index = i;
            _answerButtons[i].onClick.AddListener(() => SelectAnswer(index));
        }

        LoadQuestion();
    }

    #endregion

    #region Private methods

    private void ShuffleQuestions()
    {
        for (int i = 0; i < _questions.Length; i++)
        {
            QuizQuestion temp = _questions[i];
            int randomIndex = Random.Range(i, _questions.Length);
            _questions[i] = _questions[randomIndex];
            _questions[randomIndex] = temp;
        }
    }

    private void EndQuiz()
    {
        int incorrectAnswers = _currentQuestionIndex - _score;
        GameData.CorrectAnswers = _score;
        GameData.IncorrectAnswers = incorrectAnswers;
        SceneManager.LoadScene("GameOverScene");
    }

    private void LoadQuestion()
    {
        if (_currentQuestionIndex < _questions.Length)
        {
            QuizQuestion question = _questions[_currentQuestionIndex];
            _questionImage.sprite = question.QuestionImage;
            _answerTexts[0].text = question.Answer1;
            _answerTexts[1].text = question.Answer2;
            _answerTexts[2].text = question.Answer3;
            _answerTexts[3].text = question.Answer4;

            // Обновление текста номера вопроса
            _questionNumberText.text = $"Question {_currentQuestionIndex + 1}/{_questions.Length}";
        }
    }

    private void SelectAnswer(int answerIndex)
    {
        if (answerIndex + 1 == _questions[_currentQuestionIndex].CorrectAnswer)
        {
            _score++;
        }

        _currentQuestionIndex++;

        if (_currentQuestionIndex < _questions.Length)
        {
            LoadQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    #endregion
}
