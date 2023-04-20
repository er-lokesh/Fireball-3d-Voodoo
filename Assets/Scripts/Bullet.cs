using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody rb;
    Player playerInstance;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = speed * transform.forward;
        playerInstance = Player.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other");
        if (other.tag == "Floor")
        {
            playerInstance.UpdateScore(10);
            other.gameObject.SetActive(false);
            other.transform.GetComponentInParent<TowerGenerator>().RemoveFloor();
            Destroy(gameObject);
        }
        if (other.tag == "Top")
        {
            playerInstance.UpdateScore(15);
            other.transform.GetComponentInParent<TowerGenerator>().RemoveTop();
            Destroy(gameObject);
        }
        if (other.tag == "Obstacle")
        {
            rb.velocity = Vector3.zero;  
            ReturnBallToPlayer();
        }
    }

    private void ReturnBallToPlayer()
    {
        playerInstance.playerState = PlayerState.Dead;

        StartCoroutine(CannonballMovement());

        //rb.AddForce(Player.instance.transform.position, ForceMode.Impulse);
        transform.DOMove(playerInstance.transform.position, 1f).OnComplete(()=>
        {
            Camera.main.transform.parent = null;
            playerInstance.ShowGameOver();
            Destroy(playerInstance.gameObject);
            Destroy(gameObject);
        });

        Debug.Log("Game over");
    }

    IEnumerator CannonballMovement()
    {
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(0f);

        //Vector3 testVector = Player.instance.transform.position;
        //// Calculate distance to target
        //float target_Distance = Vector3.Distance(transform.position, testVector);

        //// Calculate the velocity needed to throw the object to the target at specified angle.
        //float projectile_Velocity = target_Distance / (Mathf.Sin(2 * privateRotation * Mathf.Deg2Rad) / dragDown);

        //// Extract the X  Y componenent of the velocity
        //float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(privateRotation * Mathf.Deg2Rad);
        //float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(privateRotation * Mathf.Deg2Rad);

        //// Calculate flight time.
        //float flightDuration = target_Distance / Vx;

        //// Rotate projectile to face the target.
        //transform.rotation = Quaternion.LookRotation(testVector - transform.position);

        //float elapse_time = 0;

        //while (elapse_time < flightDuration)
        //{
        //    transform.Translate(0, (Vy - (dragDown * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

        //    elapse_time += Time.deltaTime;

        //    yield return null;
        //}
    }

}
