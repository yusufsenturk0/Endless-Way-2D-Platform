using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Animator animator;
    public HeroMovement hero;
    public float jumpForce=17.3f;
    // Update is called once per frame
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero"&&!hero.isDead)
        {
           hero.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jumper");
        }
    }
}
