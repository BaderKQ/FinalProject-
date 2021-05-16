using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    GameObject SpawnController;
    public Vector3 SpawnLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("PLAYER FOUND");
        if (collision.gameObject.CompareTag("Player")) PlayerSpawnController.Instance.NewSpawnpoint(SpawnLocation);
    }
}
