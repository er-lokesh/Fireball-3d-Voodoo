using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PlayerState
{
    Idle, 
    Walk,
    Ready,
    Dead
}

public class Player : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject bulletGO;
    public GameObject gameOverGO;
    public Transform bulletSpawnPoint;
    public PlayerState playerState = PlayerState.Idle;
    //public Action<Action> OnTowerFinished;
    public static Player instance;

    private int counter;
    private Transform[] movePoints;
    private Action OnMovementDone;
    private int score;

    private float shootTimer = .2f;
    private bool fireAgain = true;

    private void Awake()
    {
        instance = this;
    }

    public void MoveToNextTower(Transform[] movePoints, int index, Action callback)
    {
        playerState = PlayerState.Walk;

        Debug.Log("Move to next tower");
        counter = index;
        this.movePoints = movePoints;
        OnMovementDone = callback;
        MoveBegin(); 
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void UpdateHighScore()
    {
        ScoreHandler.Score = score;
    }

    public void ShowGameOver()
    {
        UpdateHighScore();
        gameOverGO.SetActive(true);
    }

    private void MoveBegin()
    {
        Quaternion targetRotation = Quaternion.LookRotation(movePoints[counter].position - transform.position);
        transform.DORotateQuaternion(targetRotation, 1.5f).OnComplete(() =>
        {
            transform.DOMove(movePoints[counter].position, 2f).OnComplete(CheckNextMove);
        });
    }

    private void CheckNextMove()
    {
        if (++counter < movePoints.Length)
            MoveBegin();
        else
            OnMovementDone?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerState == PlayerState.Ready)
            Shoot();
    }

    private float timer = 0;

    private void Shoot()
    {
        if (fireAgain && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)))
        {
            Instantiate(bulletGO, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            fireAgain = false;
            timer = 0;
        }

        if(timer < shootTimer)
        {
            timer += Time.deltaTime;
            if (timer >= shootTimer)
                fireAgain = true;
        }
    }
}
