using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    #region Variables

    [Header("UI")]
    [SerializeField] private Button _startButton;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        Debug.Log("StartGame Start");
        _startButton.onClick.AddListener(EnterGameClickedCallback);
    }

    #endregion

    #region Private methods

    private void EnterGameClickedCallback()
    {
        Debug.Log("Enter Game");
        SceneManager.LoadScene("GameScene");
    }

    #endregion
}