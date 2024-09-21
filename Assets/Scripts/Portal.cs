using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    private bool openDoor = false;

    private Animator animator;





    private void Awake()
    {
        animator = GetComponent<Animator>(); //Gets the component of the Players Animator2D

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Check(bool open)
    {
        openDoor = open;
        animator.SetBool("isActive", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && openDoor) //Checks portal conditions
        {
            if (SceneManager.GetActiveScene().name == "level1") //Checks level
            {
                SceneManager.LoadScene("level2");
            }

            if (SceneManager.GetActiveScene().name == "level2") //Checks level
            {
                SceneManager.LoadScene("level3");
            }

            if (SceneManager.GetActiveScene().name == "level3") //Checks level
            {
                SceneManager.LoadScene("theend");
            }

        }
    }
}
