using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwing : MonoBehaviour
{

    public GameObject Character;
    public float thrust = 50.0f;
    public float thrustM = -2.0f;
    public Rigidbody rb2;
    public float count = 5.0f;
    public System.DateTime startTime;
    private Vector3 distance;

    void Start()
    {
        rb2 = GetComponent<Rigidbody>();
    }
    void Update()
    {
        getKeyUp();
        getKeyLeft();
        getKeyRight();
        getKeyDownward();
    }

    private void getKeyUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {


            count = 5.0f;                                                                                                                                       //when the direction is pressed initially give the avacado some initial count
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            startTime = System.DateTime.UtcNow;
            count = count + 0.4f;                                                                                                                               //counter to see how long a drection button being pressed
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))                                                                                                                    //releasing the button releases the avacado
        {
            distance = new Vector3(Character.transform.position.x, Character.transform.position.y + 10.0f, Character.transform.position.z + 5f);                //teleport the avacado to the player, then give it 0 velocity, then add a force in the direction pressed relative to how long the button was held
            transform.position = distance;
            rb2.velocity = Vector3.zero;
            if (count > 145) { count = 145; }

            rb2.AddForce(0, count/4, count, ForceMode.Impulse);
        }
    }
    private void getKeyLeft()                                                                                                                                   //do the exact same for each of the directions, and adjust the direction of the force to the direction
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            count = 5.0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            startTime = System.DateTime.UtcNow;
            count = count + 0.4f;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            distance = new Vector3(Character.transform.position.x, Character.transform.position.y + 10.0f, Character.transform.position.z + 5f);
            transform.position = distance;
            rb2.velocity = Vector3.zero;
            if (count > 145) { count = 145; }
            rb2.AddForce(-count, count / 4, 0, ForceMode.Impulse);
        }
    }
    private void getKeyRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            count = 5.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            startTime = System.DateTime.UtcNow;
            count = count + 0.4f;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            distance = new Vector3(Character.transform.position.x, Character.transform.position.y + 10.0f, Character.transform.position.z + 5f);
            transform.position = distance;
            rb2.velocity = Vector3.zero;
            if (count > 145) { count = 145; }
            rb2.AddForce(count, count / 4, 0, ForceMode.Impulse);
        }
    }
    private void getKeyDownward()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            count = 5.0f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            startTime = System.DateTime.UtcNow;
            count = count + 0.4f;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            distance = new Vector3(Character.transform.position.x, Character.transform.position.y + 10.0f, Character.transform.position.z + 5f);
            transform.position = distance;
            rb2.velocity = Vector3.zero;
            if (count > 145) { count = 145; }
            rb2.AddForce(0, count / 4, -count, ForceMode.Impulse);
        }
    }
}
