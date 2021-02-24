using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MoleBehaviour : MonoBehaviour
{
    private StateIds currentStateId = StateIds.hide;
    private float yourCounter = 0.0f;
    public GameObject[] flowers;
    public GameObject character;
    public GameObject avacado;
    public int molesRemaining = 10;
    public int flowersRemaining = 21;
    public int moleSplatPos;
    public int activeFlowers = 0;
    public Material deadMole;
    public int deadMoleCounter = 0;
    public float timeCounter = 0.0f;
    private GUIStyle guiStyle = new GUIStyle();

    private void Update()
    {
        int number = 0;

        switch (currentStateId)                                                                                                                                                         //crate a switch statement that controls the states of the mole
        {
            case StateIds.hide:                                                                                                                                                         //the mole is currently underground                                       
                System.Random rnd = new System.Random();
                yourCounter += Time.deltaTime;                                                                                                                                          //start a counter for the time
                number = (int)yourCounter;
                List<int> relativePositions = new List<int>();
                float characterX = character.transform.position.x;
                float characterZ = character.transform.position.z;
                for (int i = 0; i < flowers.Length; i += 1)
                {
                    float x = flowers[i].transform.position.x;
                    float z = flowers[i].transform.position.z;
                    float differenceX = x - characterX;
                    float differenceZ = z - characterZ;

                    if ((differenceX >= 80.0f || differenceX <= -80.0f) || (differenceZ >= 80.0f || differenceZ <= -80))                                                                //this finds all the flowers that are at least acertain distance away from the player as a teleport candidate
                    {
                        if (flowers[i].activeSelf) { relativePositions.Add(i); }
                        

                    }
                }

                int randomLocation = rnd.Next(relativePositions.Count);                                                                                                                 //picks one of those valid flowers at random
                if (yourCounter >= 5.0f)                                                                                                                                                //if youve been hidden for 5 seconds pop up at a random valid location, and reduce the amount of active flowers by 1
                {
                    if (relativePositions.Count == 0) {
                        currentStateId = StateIds.hide;
                        break;
                    }
                    moleSplatPos = relativePositions[randomLocation];
                    flowers[moleSplatPos].SetActive(false);                                                                                                                                                    
                    transform.position = new Vector3(flowers[moleSplatPos].transform.position.x, 5, flowers[moleSplatPos].transform.position.z);
                    
                   if (flowers[moleSplatPos].GetComponent<flower>().flowerDestroyed == false)
                    {
                        flowers[moleSplatPos].GetComponent<flower>().flowerDestroyed = true;
                        flowersRemaining -= 1;
                    }
                    
                    

                    currentStateId = StateIds.show;                                                                                                                                     //then set your state to show and reset the time counter
                    yourCounter = 0.0f;
                }




                break;                                                                                                                                                                  //end each state

            case StateIds.show:                                                                                                                                                         //the mole is currently above ground
                yourCounter += Time.deltaTime;
                number = (int)yourCounter;
                if ((transform.position.z - character.transform.position.z) < 30.0f && (transform.position.z - character.transform.position.z) > -30.0f)
                {
                    if ((transform.position.x - character.transform.position.x) < 30.0f && (transform.position.x - character.transform.position.x) > -30.0f)                            //if the character is within a certain distance teleport to safety, the counter will keep going in this case so 
                    {                                                                                                                                                                   //the state will be updated after less than 5 seconds anyway
                        transform.position = new Vector3(-1000, -50, -1000);
                    }
                }

                if (yourCounter >= 5.0f)
                {
                    transform.position = new Vector3(-1000, -50, -1000);                                                                                                                //if the timer has reached 5 teleport the mole away and update the state to hide, and reset the time counter
                    currentStateId = StateIds.hide;
                    yourCounter = 0.0f;
                }

                double rz = System.Convert.ToDouble(transform.position.z - avacado.transform.position.z);                                                                               //check if the avacado is within a certain distance, if so, kill the mole. killing the mole is actually
                double rx = System.Convert.ToDouble(transform.position.x - avacado.transform.position.x);                                                                               //just teleporting it away and reducing the moles alive counter by 1, then changing the state to dead
                double pyth = (rx * rx) + (rz * rz);
                double q = Math.Sqrt(pyth);
                q = (float)q;
                if (q < 20.0f && (avacado.transform.position.y < 20.0f && avacado.transform.position.y > -5.0f))
                {
                    transform.position = new Vector3(-1000, -50, -1000);
                    
                    currentStateId = StateIds.dead;
                    molesRemaining -= 1;
                }
                break;

            case StateIds.dead:                                                                                                                                                         //the mole is currently dead
                yourCounter += Time.deltaTime;
                flowers[moleSplatPos].SetActive(true);
                flowers[moleSplatPos].transform.rotation = new Quaternion(0,0,0,0);                                                                                                     //bring back the flower, and change its material to be the blood texture, then set it to be flat on the ground 
                flowers[moleSplatPos].transform.position = new Vector3(flowers[moleSplatPos].transform.position.x, 0.1f, flowers[moleSplatPos].transform.position.z);                   //and set its scale to be 1:1:1
                flowers[moleSplatPos].GetComponent<Renderer>().material = deadMole;
                flowers[moleSplatPos].transform.localScale = new Vector3(1, 1, 1);
                
                if (yourCounter >= 5.0f)
                {
                    
                    transform.position = new Vector3(-1000, -50, -1000);                                                                                                                //if hes been dead for 5 seconds then teleport him to the hide position, then reset the time counter and set the state back to hidden
                    currentStateId = StateIds.hide;
                    yourCounter = 0.0f;
                }
                break;
        }
    }

    void OnGUI()                                                                                                                                                                        //this section controls what is displayed on top of the game. it consists of a mole counter, a flower counter
    {                                                                                                                                                                                   //and a win/ loss display
        activeFlowers = 0;
        for (int i = 0; i < flowers.Length; i++)
        {
            if (flowers[i].activeSelf == true)
            {
                if (flowers[i].GetComponent<Renderer>().material.name == "flower (Instance)") {                                                                                         //this works out how many flowers are active by checking how many of the flowers still have a meterial named
                    activeFlowers += 1;                                                                                                                                                 //flower, and excludes the ones with a blood material
                }
                      
            }
        }
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 400, 20), "Moles Remaining: " + molesRemaining.ToString(), guiStyle);
        GUI.Label(new Rect(10, 30, 400, 20), "Flowers Remaining: " + activeFlowers.ToString(), guiStyle);
        float h = (Screen.height / 2) - 200;                                                                                                                                            //find the center of the screen and set the font size
        float w = (Screen.width / 2) - 130;
        guiStyle.fontSize = 60;
        
        if (molesRemaining == 0)                                                                                                                                                        //if there are no moles remaining, show you win, if no flowers, show you lose. then close game after a few seconds
        { 
            GUI.Label(new Rect(w, h, 1000, 1000), "YOU WIN", guiStyle);
            timeCounter += Time.deltaTime;
            if (timeCounter >= 8.0f)
            {

                timeCounter = 0.0f;
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
        }
        if (activeFlowers == 0)
        {
            GUI.Label(new Rect(w, h, 1000, 1000), "YOU LOSE", guiStyle);
            timeCounter += Time.deltaTime;
            if (timeCounter >= 8.0f)
            {

                timeCounter = 0.0f;
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
        }
    }
 
}

public enum StateIds                                                                                                                                                                    //initialise states
{
    show, 
    hide,
    dead
}

