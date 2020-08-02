﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button startGameButton;

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


    }
}