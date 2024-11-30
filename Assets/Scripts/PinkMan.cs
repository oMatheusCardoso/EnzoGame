using UnityEngine;

public class PinkMan : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private bool colliding;
    private bool movingRight = true; // Define a direção inicial

    public float speed;
    public Transform rightCol;
    public Transform leftCol;
    public Transform headPoint;
    public LayerMask layer;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;
    public AudioSource killPinkMan;

    private bool playerDestroyed;

    // Start is called before the first execution of Update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimento horizontal
        rig.linearVelocity = new Vector2(speed, rig.linearVelocity.y);

        // Verifica colisão nos pontos laterais
        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if (colliding)
        {
            // Inverte a direção do inimigo
            movingRight = !movingRight;
            transform.localScale = new Vector2(movingRight ? 1 : -1, transform.localScale.y);
            speed = Mathf.Abs(speed) * (movingRight ? 1 : -1);
        }
    }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Player")
            {
            // Calcula a altura do ponto de contato com um pequeno offset
            ContactPoint2D contact = col.contacts[0];
            float height = contact.point.y - (headPoint.position.y - 0.05f); // Offset de 0.05 para corrigir discrepâncias
            //Debug.Log("Altura relativa: " + height);

            // Verifica se o jogador está acima ou próximo do topo do inimigo
            if (height > -0.01f && !playerDestroyed) // Permite pequenas diferenças negativas
            {
                // Reduz a força aplicada ao jogador
                Rigidbody2D playerRb = col.gameObject.GetComponent<Rigidbody2D>();
                playerRb.AddForce(Vector2.up * 5, ForceMode2D.Impulse); // Reduzi força para 5

                // Limita velocidade para evitar voar
                if (playerRb.linearVelocity.y > 5f)
                {
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 5f);
                }

                // Animação e destruição do inimigo
                speed = 0f;
                anim.SetTrigger("die");
                boxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                rig.bodyType = RigidbodyType2D.Kinematic;
                killPinkMan.Play();
                Destroy(gameObject, 0.33f);
            }
            else if (!playerDestroyed) // Jogador tocou o inimigo de lado ou abaixo
            {
                playerDestroyed = true;
                GameController.instance.ShowGameOver();
                Destroy(col.gameObject);
            }
        }
}   

}
