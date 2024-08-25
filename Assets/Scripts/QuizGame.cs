using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class QuizGame : MonoBehaviour
{
    #region Variables

    [Header("Quiz Settings")]
    [SerializeField] private QuizQuestionConfig[] _questions;
    [SerializeField] private Image _questionImage;
    [SerializeField] private Button[] _answerButtons;
    [SerializeField] private TextMeshProUGUI[] _answerTexts;
    [SerializeField] private TextMeshProUGUI _questionNumberText;

    [Header("Heart Settings")]
    [SerializeField] private GameObject[] _hearts;

    [Header("Button Sprites")]
    [SerializeField] private Sprite _correctSprite;
    [SerializeField] private Sprite _incorrectSprite;
    [SerializeField] private Sprite _defaultSprite;
    
    private int _currentQuestionIndex;
    private int _lives;
    private int _score;
    private const float DelaySeconds = 0.45f;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        _lives = _hearts.Length;
        ShuffleQuestions();
        for (int i = 0; i < _answerButtons.Length; i++)
        {
            int index = i;
            _answerButtons[i].onClick.AddListener(() => SelectAnswerClickedCallback(index));
        }
        LoadQuestion();
    }

    #endregion

    #region Private methods

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
            QuizQuestionConfig question = _questions[_currentQuestionIndex];
            _questionImage.sprite = question.QuestionImage;
            _answerTexts[0].text = question.Answer1;
            _answerTexts[1].text = question.Answer2;
            _answerTexts[2].text = question.Answer3;
            _answerTexts[3].text = question.Answer4;
            _questionNumberText.text = $"Question {_currentQuestionIndex + 1}/{_questions.Length}";
            foreach (Button button in _answerButtons)
            {
                button.image.sprite = _defaultSprite;
            }
        }
    }

    private void LoseLife()
    {
        if (_lives > 0)
        {
            _lives--;
            _hearts[_lives].SetActive(false);
        }
    }

    private void SelectAnswerClickedCallback(int answerIndex)
    {
        bool isCorrect = answerIndex + 1 == _questions[_currentQuestionIndex].CorrectAnswer;
        
        if (isCorrect)
        {
            _score++;
        }
        else
        {
            LoseLife();
            ShowCorrectAnswer();
        }

        _answerButtons[answerIndex].image.sprite = isCorrect ? _correctSprite : _incorrectSprite;
        
        StartCoroutine(ProceedToNextQuestionWithDelay());
    }

    private void ShowCorrectAnswer()
    {
        int correctAnswerIndex = _questions[_currentQuestionIndex].CorrectAnswer - 1;
        _answerButtons[correctAnswerIndex].image.sprite = _correctSprite;
    }

    private IEnumerator ProceedToNextQuestionWithDelay()
    {
        yield return new WaitForSeconds(DelaySeconds);
        _currentQuestionIndex++;
        if (_lives > 0 && _currentQuestionIndex < _questions.Length)
        {
            LoadQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    private void ShuffleQuestions()
    {
        for (int i = 0; i < _questions.Length; i++)
        {
            QuizQuestionConfig temp = _questions[i];
            int randomIndex = Random.Range(i, _questions.Length);
            _questions[i] = _questions[randomIndex];
            _questions[randomIndex] = temp;
        }
    }

    #endregion
}
