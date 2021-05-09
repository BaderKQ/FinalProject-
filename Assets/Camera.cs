using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject mTrackingObject = null;
    protected void Update()
    {
        if (mTrackingObject != null)
        {
            Vector3 Pos;
            Pos = transform.position;
            if (mTrackingObject.transform.position.y >= -5)
            {
                Pos.x = mTrackingObject.transform.position.x;
                Pos.y = 0;
                //Pos.y = mTrackingObject.transform.position.y;
                //Pos.z = mTrackingObject.transform.position.z;
                transform.position = Pos;
            }

            if (mTrackingObject.transform.position.y < -5)
            {
                Pos.x = mTrackingObject.transform.position.x;
                Pos.y = -10;
                //Pos.z = mTrackingObject.transform.position.z;
                transform.position = Pos;
            }



        }


    }
}