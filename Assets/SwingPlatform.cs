using UnityEngine;
using System.Collections;

public class SwingPlatform : MonoBehaviour
{
    //public GameObject followObj;
    public Transform platformPos;


    void Start()
    {

    }
    void Update()
    {
        transform.position = new Vector3(platformPos.position.x, transform.position.y, transform.position.z);
    }
}
