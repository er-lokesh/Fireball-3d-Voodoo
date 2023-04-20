using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button restartBtn;

    private void Start()
    {
        scoreText.text = ScoreHandler.Score.ToString();
        restartBtn.onClick.AddListener(OnRestart);
    }

    private void OnRestart()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
