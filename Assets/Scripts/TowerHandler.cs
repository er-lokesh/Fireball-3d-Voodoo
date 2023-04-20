using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerHandler : MonoBehaviour
{
    public TowerGenerator[] towerGenerators;
    public int towerCounter;

    private TowerGenerator currentTower;

    private void Start()
    {
        currentTower = towerGenerators[towerCounter++];
        StartTowerGenerate();
    }

    private void StartTowerGenerate()
    {
        currentTower.GenerateTower();
        currentTower.OnTowerGenerationCompleted += OnTowerGenerationCompleted;
        currentTower.OnFloorFinished += OnFloorFinished;
    }

    private void OnTowerGenerationCompleted()
    {
        Player.instance.playerState = PlayerState.Ready;
    }

    private void OnFloorFinished()
    {
        Player.instance.playerState = PlayerState.Idle;
        currentTower.gameObject.SetActive(false);

        if (towerCounter < towerGenerators.Length)
        {
            currentTower = towerGenerators[towerCounter++];
            Player.instance.MoveToNextTower(currentTower.movePoints, 0, StartTowerGenerate); 
        }
        else
            StageCleared();
    }

    private void StageCleared()
    {
        Debug.Log("stage cleared");
        Player.instance.UpdateHighScore();
        SceneManager.LoadScene("Menu");
    }
}
