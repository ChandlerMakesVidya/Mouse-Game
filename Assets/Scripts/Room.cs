using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Room[] adjacentRooms;
    public Transform[] patrolPoints;
    public bool safe;
    [HideInInspector] public BoxCollider bounds;

    private void Awake()
    {
        bounds = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.GM.playerInRoom = this;
        }
    }
}
