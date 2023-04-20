using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button startBtn;

    private void Start()
    {
        scoreText.text = ScoreHandler.Score.ToString();
        startBtn.onClick.AddListener(OnStart);
    }

    private void OnStart()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
