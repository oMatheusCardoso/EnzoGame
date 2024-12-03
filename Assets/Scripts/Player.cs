using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{

    public float Speed;
    public float JumpForce;
    public bool isJumping;
    public bool doubleJump;
    public AudioSource jumpSound;
    public GameObject onScreenControls;

    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;

    private Rigidbody2D rig;
    private Animator anim;
    private bool moveLeft;
    private bool moveRight;

    bool isBlowing;
    bool onTrampoline;
    bool controlsActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

      // Configura os listeners dos botões usando EventTrigger
        AddEventTrigger(leftButton.gameObject, EventTriggerType.PointerDown, () => moveLeft = true);
        AddEventTrigger(leftButton.gameObject, EventTriggerType.PointerUp, () => moveLeft = false);
        AddEventTrigger(rightButton.gameObject, EventTriggerType.PointerDown, () => moveRight = true);
        AddEventTrigger(rightButton.gameObject, EventTriggerType.PointerUp, () => moveRight = false);
        jumpButton.onClick.AddListener(Jump);
        onScreenControls.SetActive(controlsActive);
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            controlsActive = false; 
            onScreenControls.SetActive(controlsActive);
        }

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            controlsActive = true; 
            onScreenControls.SetActive(controlsActive);
        }

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed || moveLeft) 
        { 
            Move(-1); 
        } 
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed || moveRight) 
        { 
            Move(1); 
        } 
        else 
        { 
            Move(0);
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }
        
    }

    public void FixedUpdate(){
        //Move();
    }

    // Movimento horizontal do personagem
    void Move(float direction)
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        //transform.position += movement * Time.deltaTime * Speed; //move o personagem sem usar a fisica

        //float movement = Input.GetAxisRaw("Horizontal"); //Movimento horizontal do personagem
        float movement = direction * Speed; 

        rig.linearVelocity = new Vector2(movement, rig.linearVelocity.y);

        if (movement > 0)
        {
            anim.SetBool("walk", true); //Chama a animação do personagem quando andar.
            transform.eulerAngles = new Vector3(0f, 0f, 0f); //Rotação padrão do personagem.
        }

        if (movement < 0)
        {
            anim.SetBool("walk", true); //Chama a animação do personagem quando andar.
            transform.eulerAngles = new Vector3(0f, 180f, 0f); //Muda a rotação do personagem para esquerda.
        }

        if (movement == 0) 
        {
            anim.SetBool("walk", false); //Desativa a animação do personagem quando parado.
        }
    }

    // Pulo do personagem
    void Jump()
    {
        
        if (!isJumping && !isBlowing && !onTrampoline) //Primeiro pulo
        {
            jumpSound.Play();
            rig.AddForce(new Vector2 (0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jump", true);
            isJumping = true;
            doubleJump = true;
        }
        else
        {
            if (doubleJump && !isBlowing && !onTrampoline) //Verifica se pulou e deixa pular mais uma vez (Pulo duplo)
            {
                jumpSound.Play();
                rig.AddForce(new Vector2 (0f, JumpForce), ForceMode2D.Impulse);
                anim.SetBool("jump", true);
                doubleJump = false;
            }
        }
    }

    //Verificar se está pulando para não ficar pulando infinitamente
    void OnCollisionEnter2D(Collision2D collision) //Verificar se está encostando em algo.
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if (collision.gameObject.tag == "Spike")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "saw") 
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject); 
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 11)
        {
            isBlowing = true;
        }
        if(collider.gameObject.layer == 12)
        {
            onTrampoline = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 11)
        {
            isBlowing = false;
        }
        if(collider.gameObject.layer == 12)
        {
            onTrampoline = false;
        }
    }

    void AddEventTrigger(GameObject obj, EventTriggerType type, System.Action action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener((eventData) => { action(); });
        trigger.triggers.Add(entry);
    }

}
