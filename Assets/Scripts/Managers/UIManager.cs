using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Author: Chandler Hummingbird
 * Date Created: Oct 30. 2020
 * Date Modified: Dec. 05, 2020
 * Description: The UI Manager works in tandem with the game manager to load the
 * appropriate canvases at the right times and output data to UI elements.
 */

public class UIManager : MonoBehaviour
{
    public static UIManager UI;
    private void Awake()
    {
        if (UI == null)
        {
            UI = this;
        }
        //destruction of duplicates already handled by game manager

        HideAllCanvases();
    }

    public Canvas mainMenuCanvas;
    public Canvas HUDCanvas;
    public Canvas gameOverCanvas;
    public Canvas creditsCanvas;
    public Canvas howToPlayCanvas;
    public Canvas optionsCanvas;

    [Header("HUD Canvas Settings")]
    public TMP_Text cheeseRemainingText;

    [Header("Game Over Canvas Settings")]
    public TMP_Text winMessage;
    public TMP_Text loseMessage;

    [Header("Main Menu Canvas Settings")]
    public Button quitToDesktopButton;

    [Header("Options Canvas Settings")]
    public Slider sensitivitySlider;
    public TMP_Text sensitivityText;
    public Slider difficultySlider;
    public TMP_Text difficultyText;

    public void HideAllCanvases()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        HUDCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(false);
        howToPlayCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(false);
    }

    public void InitHUD()
    {
        HideAllCanvases();
        HUDCanvas.gameObject.SetActive(true);
    }

    public void ShowGameOverScreen()
    {
        HideAllCanvases();
        gameOverCanvas.gameObject.SetActive(true);
    }

    public void ShowCredits()
    {
        HideAllCanvases();
        creditsCanvas.gameObject.SetActive(true);
    }

    public void ShowHowToPlay()
    {
        HideAllCanvases();
        howToPlayCanvas.gameObject.SetActive(true);
    }

    public void ShowOptions()
    {
        HideAllCanvases();
        optionsCanvas.gameObject.SetActive(true);
    }

    void Start()
    {
        //Remove the ability to quit to desktop in WebGL build, since you're already on the desktop
        //and attempting to quit will freeze the game.
        #if UNITY_WEBGL
        quitToDesktopButton.gameObject.SetActive(false);
        #endif
    }

    void Update()
    {
        //put stuff here
    }

    public void ChangeDifficulty(float value)
    {
        GameManager.GM.difficulty = (int)value;
        switch (value)
        {
            case 0:
                difficultyText.text = "Difficulty: Easy";
                break;
            case 1:
                difficultyText.text = "Difficulty: Medium";
                break;
            case 2:
                difficultyText.text = "Difficulty: Hard";
                break;
        }
    }

    public void ChangeSensitivity(float value)
    {
        GameManager.GM.mouseSensitivity = value;
        sensitivityText.text = "Sensitivity: " + value.ToString("F2");
    }

    float NormalizeFloat(float input, float max)
    {
        if (input == 0f) return 0f;
        return input / max;
    }
}
