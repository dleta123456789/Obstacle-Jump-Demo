using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce=1000.0f;
    public float gravityModifier = 1.0f;

    [Header ("Game States")]
    public bool isOnGround = true;
    public bool gameOver = false;

    [Header("VFX")]
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private Animator playerAnim;

    [Header("SFX")]
    public AudioClip jumpSound;
    public float jumpSoundVol;
    public AudioClip crashSound;
    public float crashSoundVol;
    public bool crashed = false;
    private AudioSource playerAudio;

    [Header("Movement")]
    public int jumpNumber = 0;
    public bool dash = false;
    public KeyCode dashKey;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }
     void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && (isOnGround==true || jumpNumber<2))
        {
            playerAudio.PlayOneShot(jumpSound, jumpSoundVol);
            if (jumpNumber > 0)
            {
                playerRb.AddForce(Vector3.up * jumpForce*0.7f, ForceMode.Impulse);

            }
            else
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            }
            isOnGround = false;
            dirtParticle.Stop();
            playerAnim.SetTrigger("Jump_trig");
            jumpNumber++;
        }
        if (Input.GetKey(dashKey))
        {
            Debug.Log("Dashing");
            dash = true;
            //playerAnim.speed = 2.0f;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
            //score = score + 2;
        }
        else
        {
            dash = false;
            //playerAnim.speed = 1.0f;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
            //score = score + 1;
        }
        //Debug.Log("Score= "+ score);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if player colliding with ground or obstacles
        isOnGround = true;
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            jumpNumber = 0;
            isOnGround = true;
            dirtParticle.Play();
        }
        //if obstacle end the game
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (gameOver == false)
            {
                gameOver = true;
                Debug.Log("Game Over");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(crashSound, crashSoundVol);
                explosionParticle.Play();
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                //Debug.Log("Final Score= " + score);
            }

        }
    }
}
