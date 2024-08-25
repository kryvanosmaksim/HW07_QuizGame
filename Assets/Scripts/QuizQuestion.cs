using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Quiz/Question")]
public class QuizQuestion : ScriptableObject
{
    #region Variables

    public string Answer1;
    public string Answer2;
    public string Answer3;
    public string Answer4;
    public int CorrectAnswer;

    public Sprite QuestionImage;

    #endregion
}