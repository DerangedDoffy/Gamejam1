using UnityEngine;
using System.Collections;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform repsawnPoint;

    void OnTriggerEnter(Collider other)
    {
        player.transform.position = repsawnPoint.transform.position;
    }
}
