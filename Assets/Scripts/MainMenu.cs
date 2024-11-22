using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelectionPanel;
    public GameObject settingsPanel;

    [Header("Settings")]
    public Slider leftSlider;
    public Slider rightSlider;
    private int volume;
    public TextMeshProUGUI volumeValue;

    // Snake Speed
    public Slider spSliderLeft;
    public Slider spSliderRight;
    private int speed;
    public TextMeshProUGUI snakeSpeed;

    // Sound on/off
    public Image soundOnLeft;
    public Image soundOffLeft;
    public Image soundOnRight;
    public Image soundOffRight;
    private bool muted = false;

    private void Start()
    {
        volume = PlayerPrefs.GetInt("Volume");
        leftSlider.value = volume;
        rightSlider.value = volume;
        BackgroundMusic.audioSource.volume = volume / 10.0f;
        volumeValue.text = volume.ToString();

        // Snake Speed
        speed = PlayerPrefs.GetInt("SnakeSpeed");
        spSliderLeft.value = speed;
        spSliderRight.value = speed;
        snakeSpeed.text = speed.ToString();

        // Sound on/off
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            LoadSound();
        }
        else
        {
            LoadSound();
        }
        if (muted)
            BackgroundMusic.audioSource.Pause();
        else
        {
            if (!BackgroundMusic.audioSource.isPlaying)
                BackgroundMusic.audioSource.Play();
        }
        SoundIconChange();
    }

    public void LevelSelectionScreen()
    {
        levelSelectionPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void SettingsScreen()
    {
        levelSelectionPanel.SetActive(false);
        settingsPanel.SetActive(true);
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

    public void SPLeftSliderChange()
    {
        spSliderRight.value = spSliderLeft.value;
        ChangeSpeed();
    }

    public void SPRightSliderChange()
    {
        spSliderLeft.value = spSliderRight.value;
        ChangeSpeed();
    }

    private void ChangeSpeed()
    {
        speed = (int)spSliderRight.value;
        PlayerPrefs.SetInt("SnakeSpeed", speed);
        snakeSpeed.text = speed.ToString();
    }

    // Sound on/off
    public void SoundOn_Off()
    {
        if (!muted)
        {
            muted = true;
            BackgroundMusic.audioSource.Pause();
        }
        else
        {
            muted = false;
            if (!BackgroundMusic.audioSource.isPlaying)
                BackgroundMusic.audioSource.Play();
        }
        SaveSound();
        SoundIconChange();
    }

    private void SoundIconChange()
    {
        if (muted)
        {
            soundOnLeft.enabled = true;
            soundOffLeft.enabled = false;
            soundOnRight.enabled = true;
            soundOffRight.enabled = false;
        }
        else
        {
            soundOnLeft.enabled = false;
            soundOffLeft.enabled = true;
            soundOnRight.enabled = false;
            soundOffRight.enabled = true;
        }
    }
    private void LoadSound()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void SaveSound()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void StartLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void BackToMenu()
    {
        settingsPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
    }
}
