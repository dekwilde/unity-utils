using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;             // Pontuação atual
    public Text scoreText;           // Referência ao texto da UI

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
    public void AddScore(int num)
    {
        score += num;
        UpdateScoreText();
    }

    public void RemoveScore(int num)
    {
        score -= num;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
