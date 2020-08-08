using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button startGameButton;
    [SerializeField] Toggle musicToggle;
    [SerializeField] TextMeshProUGUI score;

    private void Start()
    {
        if (newGameButton != null)
        {
            newGameButton.onClick.AddListener(GameManager.instance.LoadStartMenu);
        }

        if (startGameButton != null)
        {
            startGameButton.onClick.AddListener(GameManager.instance.LoadFirstLevel);
        }
        if (musicToggle != null)
        {
            musicToggle.onValueChanged.AddListener(GameManager.instance.ToggleMusic);
        }

        if (score != null)
        {
            score.text = $"Score: {GameManager.instance.highScore}";
        }

    }
}
