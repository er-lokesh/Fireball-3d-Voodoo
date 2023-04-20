using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private List<Transform> obstaclesGO;
    [SerializeField] private TowerGenerator towerGenerator;
    public bool isTowerReady;

    private void Start()
    {
        towerGenerator.OnTowerGenerationCompleted += OnTowerGenerationCompleted;
    }

    private void OnTowerGenerationCompleted()
    {
        foreach (var go in obstaclesGO)
            go.gameObject.SetActive(true);

        isTowerReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTowerReady)
            Rotate();
    }

    private void Rotate()
    {
        transform.DORotate(Vector3.up * 360, 10f, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
    }
}
