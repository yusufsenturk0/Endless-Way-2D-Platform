using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class bullet : MonoBehaviour
{
    

    public float speed = 3f;

    
    void Update()
    {
        
        transform.Translate(Vector3.right * speed * Time.deltaTime);        //moving with speed after made
    }

    private void OnCollisionEnter2D(Collision2D collision)  //bullet destroy itself after thouch the hero or going out of area
    {
        
        if (collision.gameObject.tag == "Hero")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
           
            Destroy(gameObject);
        }
    }
    
}
