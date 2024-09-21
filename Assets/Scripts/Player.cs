using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


//Player Prefab
/* Allows the game to be played by using movement and forces.*/


public class Player : MonoBehaviour
{
    [SerializeField] public int speed = 10; //Movement speed
    [SerializeField] private int jumpForce = 20;
    private int jumps = 1;

    private Rigidbody2D rb;
    private bool OTG = false; //Acronym for "On The Ground"

    private Animator animator;

    [SerializeField] private GameObject[] coins;
    [SerializeField] private int coinsLeft;
    [SerializeField] private TextMeshProUGUI coinsText;

    //Awake is called frame 0 before starting everything
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Gets the component of the Players Rigidbody2D
        animator = GetComponent<Animator>(); //Gets the component of the Players Animator2D
        coins = GameObject.FindGameObjectsWithTag("Coin"); //Gets in an array the number of coins in the scene
        
    }

    // Start is called before the first frame update
    void Start()
    {
        coinCount();
    }

    // Update is called once per frame
    void Update()
    {

        //Jumping mechanic
        if (Input.GetKeyDown(KeyCode.W) && OTG == true) //Checks if the player is on the ground. This avoids the Air Jump Option.
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

        //Movement mechanic
        float hInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(hInput * speed, rb.velocity.y, 0); //Allows the player move right or left.

        if (hInput > 0)
        {
            animator.SetBool("IsWalkingR", true);
        }
        if (hInput == 0)
        {
            animator.SetBool("IsWalkingR", false);
            animator.SetBool("IsWalkingL", false);
        }
        if (hInput < 0)
        {
            animator.SetBool("IsWalkingL", true);
        }

        coinsText.text = "Art left to progress: " + coinsLeft.ToString();

    }

    private void coinCount()
    {
        if (SceneManager.GetActiveScene().name == "level1")
            coinsLeft = 1;
        else if (SceneManager.GetActiveScene().name == "level2")
            coinsLeft = 4;
        else if (SceneManager.GetActiveScene().name == "level3")
            coinsLeft = 7;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InvWall") && OTG == false) //Checks collision with invisible walls and cancels the speed, so the player doesn't get glued on the wall.
        {
            speed = 0;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InvWall") && OTG == true) //Checks if the player falled to the ground and reactivates the speed if so.
        {
            speed = 10;
        }

        if (collision.gameObject.CompareTag("Ground")) //Checks ground collision and enables OTG.
        {
            OTG = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) //Checks ground collision and disables OTG.
        {
            OTG = false;
            jumps--;
        }

        if (collision.gameObject.CompareTag("InvWall") && OTG == false)
        {
            speed = 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        { 
            coinsLeft--;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Coin") && OTG == false) //Checks coin jump
        {
            rb.AddForce(Vector3.up * 5, ForceMode2D.Impulse);
        }
        if (collision.gameObject.CompareTag("Coin") && rb.velocity.y <= 0) //Checks coin bounce
        {
            rb.AddForce(Vector3.up * 25, ForceMode2D.Impulse);
        }
    }

}
