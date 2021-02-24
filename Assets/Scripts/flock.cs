using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{
    public GameObject[] boid;                                                                                                                                       //get a reference to all the other sheep, as well as the walls
    public Rigidbody rb3;
    public Renderer r;
    void Start()
    {
        rb3 = GetComponent<Rigidbody>();
        r = GetComponent<Renderer>();
        r.enabled = false;                                                                                                                                          //dont render the actual flock, only show the sheep 
    }

    void Update()
    {
        var shortDistanceAngle = new List<float>();
        var shortDistance = new List<float>();
        float close;
        float myX = System.Math.Abs(transform.position.x);
        float myZ = (transform.position.z);
        for (int i = 0; i < boid.Length; i += 1) {
            float bX = System.Math.Abs(boid[i].transform.position.x);
            float bZ = System.Math.Abs(boid[i].transform.position.z);                                                                                               //if another sheep is within a certain distance of this sheep, work out its distance and add it to a list, and  
            if ((myX - bX < 100.0f) || (myZ - bZ < 100.0f))                                                                                                         //its angle and add that to a list
            {
                close = (float)((float)((float)Math.Atan((myX - bX) / (myZ - bZ))) * 57.2958);                                                                      //this bit just converts the resilt of the pythagoras to a usable degrees out of 360 relative to the Z axis = 0
                if (myZ > bZ) 
                {
                    close += 180.0f;
                }
                if (close < 0.0f) 
                {
                    close += 360.0f;
                }
                shortDistanceAngle.Add(close);
                float rx = myX - bX;
                float rz = myZ - bZ;
                double pyth = (rx * rx) + (rz * rz);
                double q = Math.Sqrt(pyth);
                shortDistance.Add((float)q);
            }
        }
        float average = 2.0f;                                                                                                                                       //set to something small so we never get a null value, but so it also doesnt effect it too much
        for (int i = 0; i < shortDistanceAngle.Count; i++)                                                                                                          //this finds the average angle from all the sheep within the acceptable distance
        {
            average += shortDistanceAngle[i];                                                                                                                       //this just adds them together, find actual average later     

        }
        float last = 500;
        for (int i = 0; i < shortDistance.Count; i++)
        {
            if (shortDistance[i] < last) {                                                                                                                          //finds the distance to the closest other sheep
                last = shortDistance[i];
            }

        }
        average = average / shortDistanceAngle.Count;                                                                                                               //finds the actual average
        average += 180.0f;
        if (average > 360.0f) { average -= 360.0f; }                                                                                                                //finds the opposite to the average angle
        rb3.transform.Rotate(0.0f, average, 0.0f, Space.Self);                                                                                                      //rotate the object to the calculated opposite average angle
        rb3.AddRelativeForce(Vector3.forward * (200.0f * (1/last)));                                                                                                //give the sheep a force at the opposite angle relative to a constant * (1 / closest sheep distance)
    }
}
