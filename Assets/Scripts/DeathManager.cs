using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    private static DeathManager instance;
    private LevelManager levelManager;

    public static DeathManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DeathManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("DeathManager");
                    instance = singletonObject.AddComponent<DeathManager>();
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
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        levelManager = LevelManager.Instance;
    }

    public void ShowDeathScene()
    {
        
    }

    public void RestartCurrentLevel()
    {
        int currentLevelIndex = levelManager.GetCurrentLevelIndex();
        SceneManager.LoadScene(currentLevelIndex);
    }
}