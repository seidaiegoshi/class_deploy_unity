using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] Transform spawnPoint; // リスポーンする場所

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Playerのポジションをスポーンポイントのポジションと同じにする 
            other.transform.position = spawnPoint.position;
        }
    }
}
