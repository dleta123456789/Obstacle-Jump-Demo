using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    [Header("SFX")]
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceBackSound;
    public float bounceBackSoundVol = 1.0f;
    private AudioSource playerAudio;

    [Header ("Boundry Limit")]
    public float boundryLimitY=15.0f;
    private bool outOfBound = false;
    private Vector3 currentPostion;
    [Header("Bounce Back")]
    public float bounceBack = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        currentPostion = transform.position;
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && outOfBound==false)
        {
            playerRb.AddForce(Vector3.up * floatForce);
            //Debug.Log("This is running");
        }
        if (transform.position.y > boundryLimitY)
        {
            /*removing forces being applied cause the longer we held space, the longer we dont have to hold space.
             * to do that we simply remove the velocity and angularvelocity 
             * Used help from here:: 
             *  https://answers.unity.com/questions/543399/remove-force-from-moving-object.html
             */
            
            //playerRb.velocity = Vector3.zero;
            //playerRb.angularVelocity= Vector3.zero;
            transform.position = new Vector3(currentPostion.x, boundryLimitY, currentPostion.z);
            outOfBound = true;
        }
        else
        {
            outOfBound = false;
        }
        //Debug.Log("outOfBound: " + outOfBound);
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("Ground") && gameOver!=true)
        {
            playerRb.AddForce(Vector3.up * floatForce* bounceBack,ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceBackSound, bounceBackSoundVol);
        }
    }

}
