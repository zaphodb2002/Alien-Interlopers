using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))] 
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float screenHeightInUnits;
    private float screenWidthInUnits;
    private float halfScreenWidthInUnits;

    public float ScreenHeightInUnits { get => screenHeightInUnits; }
    public float ScreenWidthInUnits { get => screenWidthInUnits; }
    public float HalfScreenWidthInUnits { get => halfScreenWidthInUnits; set => halfScreenWidthInUnits = value; }

    public int highScore = 0;

    private AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
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

    public void LoadGameOver()
    {
        LoadLevel(2);
    }

    public void ToggleMusic(bool on)
    {
        if (on)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
