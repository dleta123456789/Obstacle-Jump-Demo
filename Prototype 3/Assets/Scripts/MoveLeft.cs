using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    private PlayerController playerControllerScript;
    [Header("Object Movement")]
    public float speed = 30.0f;

    [Header("Object Destory")]
    public float leftBound = -15;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        //move objects left if game is not over.
        //transform.Translate(Vector3.left * Time.deltaTime * speed );
        
        if (playerControllerScript.gameOver == false)
        {
            if (playerControllerScript.dash == true)
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed * 2);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
        
        
        DestoryObjects();
    }
    void DestoryObjects()
    {
        //Destroy objects on reachingthe boundry.
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

}
