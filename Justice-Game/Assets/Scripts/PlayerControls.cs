using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControls : MonoBehaviour
{
    public LayerMask platformMask;
    public Rigidbody2D rb;
    private CircleCollider2D col;
    private float speed = 5f;
    private float power = 10;
    private float powerDecreaseRate = 3f;
    private int lives;
    public int initHealth = 20;
    public int health;
    public Vector3 respawnPt;
    public Image[] hearts;

    [SerializeField] private int enemiesKilled = 0;
    [SerializeField] public int objectives = 2;
    [SerializeField] public int objectivesDone = 0;
    [SerializeField] private float jumpVelo = 10f;
    [SerializeField] private PowerBarScript powerBar;
    [SerializeField] private DamageBarScript damageBar;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        lives = 3;
        health = initHealth;
        respawnPt = transform.position;
        objectivesDone = 0;
        powerBar.SetSize((float)(power * 0.1));
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleHearts();

        powerBar.SetSize((float)(power * 0.1));
        damageBar.SetSize((float)(health * 0.05));

        if (objectivesDone == objectives)
        {
            // win screen
            Debug.Log("congrats, you won");
            Manager.instance.GameOver(1);
        }

        if (lives == 0)
        {
            // lose screen
            Manager.instance.GameOver(0);
        }

        if (health <= 0)
        {
            Death();
        }
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);

        if (IsGrounded())
        {
            power = 10;
            jumpVelo = 8f;
        }
        else
        {
            power -= powerDecreaseRate * Time.deltaTime;
            //jumpVelo -= powerDecreaseRate * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || power > 0))
        {
            rb.velocity = Vector2.up * jumpVelo;
        }
    }

    private void HandleHearts()
    {
        if(lives < 1)
        {
            hearts[0].enabled = false;
        }
        else if(lives < 2)
        {
            hearts[1].enabled = false;
        }
        else if(lives < 3)
        {
            hearts[2].enabled = false;
        }
        else
        {
            hearts[0].enabled = true;
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].enabled = true;
            }
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(col.bounds.center, col.radius, Vector2.down, .1f, platformMask);
        return raycastHit2D.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Victim"))
        {
            VictimBehavior victim = col.gameObject.GetComponent<VictimBehavior>();
            victim.floatAway = true;

            //victim.gameObject.SetActive(false);
            objectivesDone += 1;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("(player) Collision Enter");
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            //This allows custom damage to player for each enemy, i.e. bigger enemies can cause more damage - travis
            EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
            int ouch = enemy.damageDealt;
            health -= ouch;
            Debug.Log("Player contact with enemy - damage: " + ouch);
        }

    }

    public void Death()
    {
        Debug.Log("dead");
        // some kinda animation? like a bounce then fall? or negative gravity for 5 secs?
        lives -= 1;
        if(lives > 0)
        {
            //respawn
            transform.position = respawnPt;
            health = initHealth;
        }
    }

    public void KilledEnemy(bool boss)
    {
        enemiesKilled += 1;
        if (boss)
        {
            objectivesDone += 1;
        }
    }
}
