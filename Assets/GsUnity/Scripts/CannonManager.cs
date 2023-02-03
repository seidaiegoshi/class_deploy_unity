using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    [SerializeField] Cannon[] cannons;
    [SerializeField] float power;
    [SerializeField] float shotMinInterval;
    [SerializeField] float shotMaxInterval;

    bool isWaitingToShot;


    private void OnTriggerStay(Collider other)
    {

        if (isWaitingToShot) return;


        if (other.CompareTag("Player"))
        {
            isWaitingToShot = true;
            Debug.Log("プレイヤーが来た");
            StartCoroutine(ReadyToShot());
        }
    }

    IEnumerator ReadyToShot()
    {
        float waitingTime = Random.Range(shotMinInterval, shotMaxInterval);
        int cannonIndex = Random.Range(0, cannons.Length);

        yield return new WaitForSeconds(waitingTime);
        cannons[cannonIndex].Shot(power);
        isWaitingToShot = false;
    }
}
