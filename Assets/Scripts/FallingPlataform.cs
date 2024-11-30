using UnityEngine;

public class FallingPlataform : MonoBehaviour
{

    public float fallingTime;

    private TargetJoint2D target;
    private BoxCollider2D boxColl;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") //Checa se está tocando no chão.
        {
            Invoke("Falling", fallingTime);
        }       
    }

    void OnTriggerEnter2D(Collider2D collider) //Colisor do morango
    {
         if (collider.gameObject.layer == 9) //Checa se está tocando no chão.
        {
            Destroy(gameObject);
        }   
    }

    void Falling()
    {
        target.enabled = false;
        boxColl.isTrigger = true;
    }
}
