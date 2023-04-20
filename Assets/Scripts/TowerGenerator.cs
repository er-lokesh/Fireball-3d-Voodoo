using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerGenerator : MonoBehaviour
{
    public GameObject floorObj;
    public GameObject topObj;
    public Transform floorParent;
    public Transform towerBody;
    public Transform[] movePoints;
    public float floorHeight;
    public int totalFloor;
    public Color color1;
    public Color color2;
    public int floorCounter;
    public TMP_Text floorCounterTxt;
    public int pillarAttackCounter;
    public float rotationOffset;
    public float towerMinOffset;

    private int flag = -1;
    private float towerScale;

    public Action OnTowerGenerationCompleted;
    public Action OnFloorFinished;

    private bool isReady;

    private IEnumerator ReduceSome()
    {
        yield return new WaitForSeconds(1);
        floorParent.GetChild(0).gameObject.SetActive(false);

        transform.position -= new Vector3(0, floorParent.GetChild(0).localScale.y, 0);
    }

    public void RemoveTop()
    {
        if (floorCounter > 0)
        {
            floorCounter--;
            floorCounterTxt.text = floorCounter.ToString();
        }
        if (floorCounter == 0)
        {
            topObj.SetActive(false);
            OnFloorFinished?.Invoke();
        }
    }

    private void Update()
    {
        if (isReady)
            towerBody.DORotate(Vector3.up * -360, 15f, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
    }

    public void RemoveFloor()
    {
        towerBody.position -= new Vector3(0, floorParent.GetChild(0).localScale.y, 0);
        floorCounter--;
        floorCounterTxt.text = floorCounter.ToString();
    }

    public TowerGenerator GenerateTower()
    {
        for (int i = 0; i < totalFloor; i++)
        {
            var floor = Instantiate(floorObj, floorParent).GetComponent<Transform>();
            floor.localPosition = new Vector3(0, flag * i * floorHeight - topObj.transform.position.y + 0.11f, 0);
            floor.localRotation = Quaternion.Euler(0, i * rotationOffset, 0);
            floor.GetComponent<MeshRenderer>().material.color = i % 2 == 0 ? color1 : color2;

            //floor.GetComponent<ConstantForce>().force = new Vector3(0, -50.0f, 0);
            //if(i < totalFloor - 1)
            towerScale += floor.localScale.y;
        }
        isReady = true;
        RaiseTowerFromFloor();
        return this;
    }

    private void RaiseTowerFromFloor()
    {
        //var endPosition = new Vector3(transform.position.x, transform.position.y + towerScale, transform.position.z);
        var endPosition = new Vector3(towerBody.position.x, towerBody.position.y + towerScale, towerBody.position.z);
        //transform.DOMove(endPosition, 2f);
        towerBody.DOMove(endPosition, 2f).OnStart(()=> {
            StartCoroutine(BeginCounting());
        });
    }

    private IEnumerator BeginCounting()
    {
        var currentValue = 0.0f;
        var rate = Mathf.Abs(floorCounter - currentValue) / 2f;

        while(currentValue != floorCounter)
        {
            currentValue = Mathf.MoveTowards(currentValue, floorCounter, rate * Time.deltaTime);
            floorCounterTxt.text = ((int)currentValue).ToString();
            yield return null;
        }
        yield return null;
        OnTowerGenerationCompleted?.Invoke();
    }
}
