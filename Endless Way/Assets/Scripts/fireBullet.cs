using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBullet : MonoBehaviour
{
    public GameObject mermiPrefab;      //the bullet that will be porduced
    public Animator animator;           //get animator to make animation for gun

    public float waitingTime;           //how many secong later the bullet will be produced
    

    //in start method , fireMachina will start to fire immedietly 
    private void Start()
    {
        animator.SetTrigger("isFire");      //we start fire anim
        Instantiate(mermiPrefab, transform.position, mermiPrefab.transform.rotation);       //produced bullet
        StartCoroutine(bulletFire());       //call to make bullet after some second
    }           
    
    IEnumerator bulletFire()        //makeing a bullet every after (0.5-1.7)second
    {
        while (true)
        {
            waitingTime = Random.Range(0.5f, 1.7f);
            yield return new WaitForSeconds(waitingTime);
            Fire();
        }
    }
    void Fire()     //fire method to produced bullet
    {
        animator.SetTrigger("isFire");
        Instantiate(mermiPrefab, transform.position, mermiPrefab.transform.rotation);
    }
    
}
