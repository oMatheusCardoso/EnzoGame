using UnityEngine;

public class Strawberry : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public GameObject collected;
    public int Score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) //Colisor do morango
    {
        if (collider.gameObject.tag == "Player")// Deleta o morango e faz o efeito de coleta.
        {
            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);

            GameController.instance.totalScore += Score; //score
            GameController.instance.UpdateScoreText(); //score do Game Controller

            Destroy(gameObject, 0.25f);
        }
    }
}
