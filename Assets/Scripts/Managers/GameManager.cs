using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: Chandler Hummingbird
 * Date Created: Sep 12, 2020
 * Date Modified: Dec 05, 2020
 * Description: The Game Manager manages basic game resources and states. This is a template
 * that should be built upon depending on the game being built.
 */

public class GameManager : MonoBehaviour
{

    #region GM Singleton
    public static GameManager GM;
    private void Awake()
    {
        if(GM != null && GM != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GM = this;
            DontDestroyOnLoad(GM);
        }
    }
    #endregion

    #region Variables
    public enum GameState
    {
        MainMenu,
        Paused,
        Playing,
        Lose,
        Win,
        GameOver
    }
    public GameState gameState = GameState.Playing;
    public bool gameRunning;
    public Player player;
    [Space]
    public static float timer;
    [Space]
    public UIManager UI;
    public float mouseSensitivity;
    [Space]
    public int difficulty;
    public int[] cheeseCountByDifficulty;
    public static int cheeseCount;
    public GameObject cheeseGameObject;
    public Transform[] cheeseLocations;
    [Space]
    public Room playerInRoom;
    #endregion

    void Start()
    {
        if (gameState == GameState.MainMenu)
        {
            UI.mainMenuCanvas.gameObject.SetActive(true);
            LockCursor(false);
        }
        else if (gameState == GameState.Playing && GM == this)
        {
            BeginGame();
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            gameState = GameState.Lose;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }

        switch (gameState)
        {
            case GameState.MainMenu:
                //place stuff here
                break;
            case GameState.Paused:
                //place stuff here
                break;
            case GameState.Playing:
                LockCursor(true);
                UI.cheeseRemainingText.text = "Cheese Remaining: " + cheeseCount;
                break;
            case GameState.Lose:
                LockCursor(false);
                gameState = GameState.GameOver;
                UI.loseMessage.gameObject.SetActive(true);
                UI.winMessage.gameObject.SetActive(false);
                UI.ShowGameOverScreen();
                gameRunning = false;
                break;
            case GameState.Win:
                LockCursor(false);
                gameState = GameState.GameOver;
                UI.loseMessage.gameObject.SetActive(false);
                UI.winMessage.gameObject.SetActive(true);
                UI.ShowGameOverScreen();
                gameRunning = false;
                break;
            case GameState.GameOver:
                break;
        }
    }

    void LockCursor(bool trueOrFalse)
    {
        if (trueOrFalse == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void BeginGame()
    {
        if (!gameRunning)
        {
            UI.InitHUD();
            cheeseLocations = FindObjectOfType<LevelInfo>().cheeseLocations;
            player = FindObjectOfType<Player>();
            cheeseCount = cheeseCountByDifficulty[difficulty];
            print(cheeseCount + " " + (GM == this));
            List<int> pickedLocations = new List<int>();
            int rand;
            for (int i = 0; i < cheeseCount; i++)
            {
                do
                {
                    rand = Random.Range(0, cheeseLocations.Length);
                } while (pickedLocations.Contains(rand));
                pickedLocations.Add(rand);

                Instantiate(cheeseGameObject, cheeseLocations[rand].position, Quaternion.identity);
            }
            gameRunning = true;
        }
    }

    public void RestartCurrentLevel()
    {
        gameState = GameState.Playing;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        if (SceneManager.GetActiveScene().name != "mainMenu")
        {
            SceneManager.LoadScene("mainMenu");
        }
        UI.HideAllCanvases();
        UI.mainMenuCanvas.gameObject.SetActive(true);
        gameState = GameState.MainMenu;
        LockCursor(false);
        gameRunning = false;
    }

    public void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene);
        gameState = GameState.Playing;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (gameState == GameState.Playing)
        {
            BeginGame();
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
