using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnInterval = 10f;
    float spawnTime;

    public Transform spawnPos;
    public GameObject spawnee;

    void Start()
    {
        spawnTime = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
            spawnTime -= Time.deltaTime;
        if (spawnTime <= 0)
        {
            Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
            spawnTime = spawnInterval;

        }
    }
}
