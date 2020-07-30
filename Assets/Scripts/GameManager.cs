using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float screenHeightInUnits;
    private float screenWidthInUnits;
    private float halfScreenWidthInUnits;

    public float ScreenHeightInUnits { get => screenHeightInUnits; }
    public float ScreenWidthInUnits { get => screenWidthInUnits; }
    public float HalfScreenWidthInUnits { get => halfScreenWidthInUnits; set => halfScreenWidthInUnits = value; }

    private void Awake()
    {
        if (GameObject.FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        screenHeightInUnits = Camera.main.orthographicSize * 2f;
        screenWidthInUnits = screenHeightInUnits * (Screen.width / (float)Screen.height);
        HalfScreenWidthInUnits = screenWidthInUnits / 2f;
    }

    private void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadFirstLevel()
    {
        LoadLevel(1);
    }

    public void LoadStartMenu()
    {
        LoadLevel(0);
    }
}
