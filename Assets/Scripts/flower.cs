using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flower : MonoBehaviour
{
    public GameObject fruitDetect;                                                                         
    public GameObject flowerDetect;
    public GameObject hitDetect;
    //private Vector3 distance;
    public float myX;
    public float myZ;
    public bool hitCheck = false;
    public bool flowerDestroyed = false;

    void Start()
    {

        myX = transform.position.x;                                                                         //set this particular flowers initial coordinates
        myZ = transform.position.z;
        

    }

    // Update is called once per frame
    void Update()
    {

        float x = fruitDetect.transform.position.x;                                                         //get the coordinates of the avacado every frame
        float y = fruitDetect.transform.position.y;
        float z = fruitDetect.transform.position.z;

        if (x <= myX + 10.0f && x >= myX - 10.0f && z <= myZ + 10.0f && z >= myZ - 10.0f)                   //if the avacado is within 10 in the x/z coodinates and if the y of the avacado is near the ground, and if the flower hasnt been destroyed before
        {
            if (y >= 0f && y <= 20.0f) 
            {
                if (flowerDestroyed == false)
                {
                    flowerDetect.SetActive(false);

                    if (hitCheck == false)
                    {
                        hitDetect.GetComponentInChildren<MoleBehaviour>().flowersRemaining -= 1;            //reduce the flowers remaining counter by 1 the first time this particular flower has benn hit
                        hitCheck = true;
                    }
                    flowerDestroyed = true;
                }
            }          
        }
    }
}
