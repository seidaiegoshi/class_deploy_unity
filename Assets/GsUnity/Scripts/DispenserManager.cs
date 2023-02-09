using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserManager : MonoBehaviour
{

    [SerializeField] Dispenser[] dispensers;
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
        int cannonIndex = Random.Range(0, dispensers.Length);

        yield return new WaitForSeconds(waitingTime);
        dispensers[cannonIndex].Shot(power);
        isWaitingToShot = false;
    }
}
