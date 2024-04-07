using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;

    public Transform portal2Transform;
    public Transform portal4Transform;
    public Transform portal6Transform;
    public GameObject musicOn;

    public AudioSource audio;
    public Animator passAnim;
    public GameObject inDoor;

    public bool isDead;                     //codes whene hero is dead
    public float speed = 5f;                //speed of hero
    public bool tersDonmus = false;         //we use it to opposite of hero whene he is going left or righr
    public float jumpForce = 5f;            //speed (force) of jumps
    public bool isGround = true;            //we are use it to check hero is ground so he cant jump twice
    public bool isPlaying=true;
    public float yatayHareket;

    public bool isLeft=false;
    public bool isRight=false;
    public bool isUp=false;
    public bool canJump = true;
    void Update()
    {


        if (!isDead)                    //these codes works whene hero alive because whene hero is dead dead animation will work and controlls shouldnt be work
        {
            //yatayHareket = Input.GetAxisRaw("Horizontal"); // Yatay ok tuþlarýna basýþlarý al


            Vector3 hareketYon = new Vector3(yatayHareket, 0, 0); // Hareket yönünü oluþtur

            transform.Translate(hareketYon * speed * Time.deltaTime); // Karakteri hareket ettir


            //these 2 if statement provide hero walks and flip (look left or right)
            if ((isLeft && tersDonmus == false)||(Input.GetKeyDown(KeyCode.A)&&tersDonmus==false)) 
            {

                transform.localScale = new Vector3((-1 * transform.localScale.x), transform.localScale.y, transform.localScale.z); // Karakterin ölçeðini ters çevir
                tersDonmus = true;
            }
            if ((isRight && tersDonmus == true)|| (Input.GetKeyDown(KeyCode.D) && tersDonmus == true)) 
            {

                tersDonmus = !tersDonmus; 
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            //whene pushing w and isGround hero will jump
            if ((isUp && isGround)|| (Input.GetKeyDown(KeyCode.W) && isGround))
            {
                
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isUp = false;
                isGround = false;
                StartCoroutine(Wait3());


            }

            



            //we start walk animation if hero walks

            if (yatayHareket != 0)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }


            animator.SetBool("isGround", isGround);

            //we start run animation if hero runs
            if (Input.GetKey(KeyCode.LeftShift) && yatayHareket != 0)
            {
                animator.SetBool("isRuning", true);
                speed = 7f;
            }
            else
            {
                if (canJump)
                {
                    animator.SetBool("isRuning", false);
                    speed = 5f;
                }
               
            }

        }




    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        //we make isGround false here to not jump twice
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
        //that code provide to move together hero and transporter
        if (collision.gameObject.tag == "transporter")
        {
            isGround = true;
    
            transform.parent = collision.gameObject.transform;
        }

        //Die whene thouch the "diken" and reestart game after 3 second
        if (collision.gameObject.tag == "Enemy"&&!isDead)
        {

            isDead = true;
            
            animator.SetTrigger("isDead");
            StartCoroutine(Wait());
        }
        if (collision.gameObject.tag == "Bullet"&&!isDead)
        {

            isDead = true;
            
            animator.SetTrigger("isDead");
            StartCoroutine(Wait());
        }

        if (collision.gameObject.tag == "finish")
        {
            StartCoroutine(Wait2());
            
        }

        if (collision.gameObject.tag == "Portal1")
        {
            transform.position = portal2Transform.position;

        }
        if (collision.gameObject.tag == "Portal3")
        {
            transform.position = portal4Transform.position;

        }
        if (collision.gameObject.tag == "Portal5")
        {
            transform.position = portal6Transform.position;

        }
        


    }
    //collecting moneys destroy them
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "money")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Music")
        {
            if (isPlaying)
            {
                isPlaying = false;
                audio.Stop();
                
            }
            else
            {
                isPlaying = true;
                audio.Play();
            }
            
            musicOn.GetComponent<Renderer>().enabled = !musicOn.GetComponent<Renderer>().enabled;


        }
        if (collision.gameObject.tag == "Start")
        {
            StartCoroutine(Wait2());
        }
    }

    //hero leavs transporter they souldnt be move together
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround= false;
        // Asansörden ayrýldýðýnda karakterin ebeveynliðini sýfýrlama
        if (collision.gameObject.tag == "transporter")
        {
            transform.parent = null;
        }
    }
    //whene hero die , here we wait 3 second then delete hero and restart the scene
   IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.6f);
        isDead= false;
        SceneManager.LoadScene("Level1");
    }
    IEnumerator Wait2()
    {
        passAnim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator Wait3()
    {
        
        yield return new WaitForSeconds(0.6f);
        canJump = true;
    }
    public void left()
    {
        yatayHareket = -1;
        isLeft = true;
    }
    public void right()
    {
        yatayHareket = 1;
        isRight = true;
    }
    public void Not()
    {
        isRight = false;
        isLeft= false;
        yatayHareket = 0;
    }

    public void jump()
    {
        if (canJump)
        {
            isUp = true;
            canJump = false;
        }
       
    }
    
}
