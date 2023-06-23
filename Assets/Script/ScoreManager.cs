using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Đối tượng TextMeshProUGUI để hiển thị điểm số
    private int score; // Biến lưu trữ điểm số
    
    public GameOverScreen GameOverScreen;
    

    
    
    private void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        AudioManager.instance.PlaySound(AudioManager.instance.score, 1f);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
        
        
    }

    public void GameOver()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
    if (playerController != null)
    {
        playerController.isGameOver = true;
    }
        GameOverScreen.Setup(score);

    }
    
}
