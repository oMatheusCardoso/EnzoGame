using UnityEngine;

public class Box : MonoBehaviour
{
    public float jumpFoce;
    public bool isUp;

    public int health = 5;

    public Animator anim;
    public GameObject effect;

    void Update()
    {
        if(health <- 0)
        {
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isUp)
            {
            anim.SetTrigger("hit");
            health--;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpFoce), ForceMode2D.Impulse);
            } else 
            {
            anim.SetTrigger("hit");
            health--;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -jumpFoce), ForceMode2D.Impulse);
            }

        }
    }
}