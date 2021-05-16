using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawnController : MonoBehaviour
{
    public static PlayerSpawnController Instance = null;
    public Vector3 RespawnPoint;
    public GameObject Player;

    public float RespawnTimer = 5f;
    float RespawnTimeLeft = 5f;

    public Text PlayersHealthDisplay;
    public string GameOverText = "Press R to respawn";

    public bool GameInProgress = true;

    void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        RespawnPoint = Player.transform.position;
        SpawnPlayer();
    }

    public void NewSpawnpoint(Vector3 NewSpawnPoint)
    {
        RespawnPoint = NewSpawnPoint;
    }

    public void SpawnPlayer()
    {
        GameInProgress = true;
        RespawnTimeLeft = RespawnTimer;
        Player.transform.position = RespawnPoint;
        Player.SetActive(true);
        Player.SendMessage("Respawn");
    }

    public void PlayerDied()
    {
        RespawnTimeLeft = RespawnTimer;
        GameInProgress = false;
    }

    void Update()
    {
        if (RespawnTimeLeft > 0 && !GameInProgress)
        {
            RespawnTimeLeft -= Time.deltaTime;
            if (RespawnTimeLeft <= 0)
            {
                PlayersHealthDisplay.text = GameOverText;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !GameInProgress && RespawnTimeLeft <= 0)
        {
            SpawnPlayer();
        }
    }
}
