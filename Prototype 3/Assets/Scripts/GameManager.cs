using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header ("Scoring System")]
    public float score;
    public float boostScore = 2.0f;

    [Header("Game Intro")]
    public Transform gameStartPoint;
    public float lerpSpeed;
    private Animator playerAnim;

    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;
        //game should be paused for intro
        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            if (playerControllerScript.dash == true)
            {
                score += boostScore;
            }
            else
            {
                score++;
            }
        }
        Debug.Log("Score= " + score);
    }
    IEnumerator PlayIntro()
    {

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);
        //set animation to walking
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_f", 0.3f); ;
        //dont play the dirt paritcles
        playerControllerScript.GetComponent<PlayerController>().dirtParticle.Stop();


        //The points between which we will lerp
        Vector3 startPoint = playerControllerScript.transform.position;
        Vector3 endPoint = gameStartPoint.position;
        // Distance of intro area
        float distanceLength = Vector3.Distance(startPoint, endPoint);
        /*Variables which will be used to lerp
         * which will be used to decide where the player will be any given moment of time in the intro
         * distance=speed*time
         * 
        */
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float percentageCovered = distanceCovered / distanceLength;

        while (percentageCovered < 1)
        {
            //update values
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            percentageCovered = distanceCovered / distanceLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPoint, endPoint, percentageCovered);
            yield return null;
        }
        //fix animation speed , start game  and go to running animation
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier",1.0f);
        playerControllerScript.gameOver = false;
        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_f", 1.0f); ;
        playerControllerScript.GetComponent<PlayerController>().dirtParticle.Play();

    }
}
