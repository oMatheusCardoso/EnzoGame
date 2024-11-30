using UnityEngine;

public class Saw : MonoBehaviour
{

    public float speed;
    public float moveTime;
    public bool dirUp;
    public bool dirdown;
    public bool dirRight;
    public bool dirleft;


    //private bool dirRight = true;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (dirRight)
        {
            transform.Translate(Vector2.right*speed*Time.deltaTime); //Verdadeiro a serra vai para a direita.
        }
        if (dirleft)
        {
            transform.Translate(Vector2.left*speed*Time.deltaTime); //Verdadeiro a serra vai para a Esquerda.
        }

        if (dirUp)
        {
            transform.Translate(Vector2.up*speed*Time.deltaTime); //Verdadeiro a serra vai para a direita.
        }
        if (dirdown)
        {
            transform.Translate(Vector2.down*speed*Time.deltaTime); //Verdadeiro a serra vai para a Esquerda.
        }

        timer += Time.deltaTime;
        if (timer >= moveTime)
        {
            dirUp = !dirUp;
            dirdown =!dirdown;
            dirRight =!dirRight;
            dirleft =!dirleft;
            timer = 0f;
        }
    }

}

