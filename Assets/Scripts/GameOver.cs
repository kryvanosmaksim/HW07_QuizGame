using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    #region Variables

    [Header("UI")]
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private TMP_Text _resultsLabel;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        int correctAnswers = GameData.CorrectAnswers;
        int incorrectAnswers = GameData.IncorrectAnswers;
        _resultsLabel.text = $"Game Over!\nCorrect Answers: {correctAnswers}\nIncorrect Answers: {incorrectAnswers}";
        
        _backToMenuButton.onClick.AddListener(ReturnToMainMenu);

        Debug.Log($"Correct Answers: {correctAnswers}, Incorrect Answers: {incorrectAnswers}");
    }

    #endregion

    #region Private methods

    private void ReturnToMainMenu()
    {
        Debug.Log("Return to Main Menu");
        SceneManager.LoadScene("StartScene");
    }

    #endregion
}