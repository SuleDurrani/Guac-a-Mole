using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheepImage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boid;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(boid.transform.position.x, 5.0f, boid.transform.position.z -10.0f);                                                                    //every frame teleport a plane that looks like a sheep to the coordinates of a particular sheep controller
    }                                                                                                                                                                           //this is done to make the sheep always visually face the player 
}
