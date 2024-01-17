using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    public List<TextMeshProUGUI> bestScoreTexts;
    public List<TextMeshProUGUI> waveNumberTexts; // Lista de TextMesh Pro UIs para mostrar el número de wave

    private int score = 0;
    private int bestScore = 0;
    private int waveNumber = 1; // Número de wave actual

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadBestScore();
        UpdateBestScoreUI();
        UpdateWaveUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();

        if (score > bestScore)
        {
            bestScore = score;
            SaveBestScore();
            UpdateBestScoreUI();
        }

        // Verifica si el puntaje es un múltiplo de 100
        if (score % 100 == 0)
        {
            // Aumenta el número de wave
            waveNumber++;
            UpdateWaveUI();
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void UpdateBestScoreUI()
    {
        foreach (var textMesh in bestScoreTexts)
        {
            if (textMesh != null)
            {
                textMesh.text = "Best Score: " + bestScore.ToString();
            }
        }
    }

    // Función para actualizar el número de wave en la UI
    private void UpdateWaveUI()
    {
        foreach (var textMesh in waveNumberTexts)
        {
            if (textMesh != null)
            {
                textMesh.text = "Wave: " + waveNumber.ToString();
            }
        }
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
    }

    private void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    public int GetScore()
    {
        return score;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }
}
