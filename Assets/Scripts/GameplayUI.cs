using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Principal;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public TextMeshPro scoreValueText;
    public TextMeshPro highScoreText;

    private int currentHighScore;
    public GameObject gameOverPanel;
    public Slider leftSlider;
    public Slider rightSlider;
    private int volume;
    public TextMeshProUGUI volumeValue;

    public GameObject pausePanel;
    public static bool isPaused = false;

    private void Awake()
    {
        isPaused = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        currentHighScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = currentHighScore.ToString();
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);

        volume = PlayerPrefs.GetInt("Volume");
        leftSlider.value = volume;
        rightSlider.value = volume;
        BackgroundMusic.audioSource.volume = volume / 10.0f;
        volumeValue.text = volume.ToString();
    }

    public void UpdateScore(int score)
    {
        if (score > currentHighScore)
        {
            currentHighScore = score;
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = currentHighScore.ToString();
        }
        scoreValueText.text = score.ToString();
    }

    public void GameoverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Replay2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LeftSliderChange()
    {
        rightSlider.value = leftSlider.value;
        ChangeVolume();
    }

    public void RightSliderChange()
    {
        leftSlider.value = rightSlider.value;
        ChangeVolume();
    }

    private void ChangeVolume()
    {
        volume = (int)rightSlider.value;
        PlayerPrefs.SetInt("Volume", volume);
        BackgroundMusic.audioSource.volume = volume / 10.0f;
        volumeValue.text = volume.ToString();
    }

    public void PauseMenu()
    {
        isPaused = true;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
    }
}
