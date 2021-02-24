using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    float speed = 1.2f;                                                                                                                      //player speed constant
    public Material[] materials;                                                                                                            //get a reference to all the animation frames rfor all of the directions, as well as a rigid body for the player
    public Material[] materialsLeft;
    public Material[] materialsRight;
    public Material[] materialsFront;
    private MeshRenderer meshRenderer;
    public Rigidbody rb;

    void start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");                                                                                           //gets what direction you are pressing
        float v = Input.GetAxisRaw("Vertical");

        animations();

        Vector3 direction = new Vector3(h, 0, v);                                                                                           //make the direction a vec3, then 
        direction = direction.normalized * speed;                                                                                           //multiply the direction by the speed constant for the player
        rb.MovePosition(transform.position + direction);                                                                                    //move the character in that direction
    }

    void animations()                                                                                                                       
    {

        float yourCounter = 0f;

        if (Input.GetKey(KeyCode.W))                                                                                                        //if you are walking in a direction, after each second it will swap the material between two different materrials to give an animation effect
        {
            
            yourCounter += Time.time * 1.8f;
            int number = (int)yourCounter;
            if (number % 2 == 0)
            {
                GetComponent<Renderer>().material = materials[4];
            }
            else
            {
                GetComponent<Renderer>().material = materials[5];
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            yourCounter = 0f;
            GetComponent<Renderer>().material = materials[0];
        }

        if (Input.GetKey(KeyCode.S))                                                                                                        //do the same for each of the four directions
        {
            yourCounter += Time.time * 1.8f;
            int number = (int)yourCounter;
            if (number % 2 == 0)
            {
                GetComponent<Renderer>().material = materialsFront[0];
            }
            else
            {
                GetComponent<Renderer>().material = materialsFront[1];
            }

        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            yourCounter = 0f;
            GetComponent<Renderer>().material = materialsFront[2];
        }

        if (Input.GetKey(KeyCode.A))
        {
            yourCounter += Time.time * 1.8f;
            int number = (int)yourCounter;
            if (number % 2 == 0)
            {
                GetComponent<Renderer>().material = materialsLeft[0];
            }
            else
            {
                GetComponent<Renderer>().material = materialsLeft[1];
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            yourCounter = 0f;
            GetComponent<Renderer>().material = materialsLeft[2];
        }

        if (Input.GetKey(KeyCode.D))
        {
            yourCounter += Time.time * 1.8f;
            int number = (int)yourCounter;
            if (number % 2 == 0)
            {
                GetComponent<Renderer>().material = materialsRight[0];
            }
            else
            {
                GetComponent<Renderer>().material = materialsRight[1];
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            yourCounter = 0f;
            GetComponent<Renderer>().material = materialsRight[2];
        }
    }
}
