using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{


    [SerializeField] float Speed = 1.0f;
    [SerializeField] AudioSource spikeBallSFX;

    float dir = 1f;

    public float leftZ, rightZ;

    private Rigidbody mRigidbody = null;

    protected void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {

        transform.eulerAngles += dir * new Vector3(0, 0, Speed);
        //Debug.Log(transform.eulerAngles.z + "/" + leftZ);
        if (dir > 0 && transform.eulerAngles.z < 180 && transform.eulerAngles.z >= rightZ)
        {
            dir = -1;
        }
        else if (dir < 0 && transform.eulerAngles.z > 180 && (transform.eulerAngles.z - 360) <= leftZ)
        {
            dir = 1;
        }

        if (dir > 0 && transform.eulerAngles.z <= 0)
        {

            spikeBallSFX.Play();
        }
        if (dir < 0 && transform.eulerAngles.z <= 0)
        {

            spikeBallSFX.Play();
        }


    }



}
