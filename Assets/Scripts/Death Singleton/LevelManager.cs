using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    private int currentLevelIndex = 0;
    private const string CurrentLevelKey = "CurrentLevel";

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("LevelManager");
                    instance = singletonObject.AddComponent<LevelManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }

        // Load the current level index from PlayerPrefs when the game starts
        LoadCurrentLevelIndex();
    }

    private void LoadCurrentLevelIndex()
    {
        if (PlayerPrefs.HasKey(CurrentLevelKey))
        {
            currentLevelIndex = PlayerPrefs.GetInt(CurrentLevelKey);
        }
        else
        {
            // Default to level 0 if no saved level index is found
            currentLevelIndex = 0;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
        currentLevelIndex = levelIndex;
        SaveCurrentLevelIndex();
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(currentLevelIndex);
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }

    private void SaveCurrentLevelIndex()
    {
        PlayerPrefs.SetInt(CurrentLevelKey, currentLevelIndex);
        PlayerPrefs.Save(); // Save the changes immediately
    }

    public void SetCurrentLevelIndex(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        SaveCurrentLevelIndex();
    }
}