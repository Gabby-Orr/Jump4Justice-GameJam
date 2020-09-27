using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int objectivesDone;
    public int objectives = 2;
    private int enemiesKilled = 0;

    [SerializeField] private float jumpVelo = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        lives = 3;
        health = initHealth;
        respawnPt = transform.position;
        objectivesDone = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);

        if (health <= 0)
        {
            Death();
        }

        if (IsGrounded())
        {
            power = 10;
            jumpVelo = 8f;
        }
        else
        {
            power -= powerDecreaseRate * Time.deltaTime;
            jumpVelo -= powerDecreaseRate * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || power > 0))
        {
            rb.velocity = Vector2.up * jumpVelo;
        }

        if(objectivesDone == objectives)
        {
            // win screen
            Debug.Log("congrats, you won");
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
            Debug.Log("yay u saved me");
            VictimBehavior victim = col.gameObject.GetComponent<VictimBehavior>();
            victim.gameObject.SetActive(false);
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
            Debug.Log("u died & have: " + lives + " lives");
            transform.position = respawnPt;
            health = initHealth;
        }
        else
        {
            // death screen, try again?
            Debug.Log("u died & have 0 lives");
            gameObject.SetActive(false);
        }
    }

    public void KilledEnemy(bool boss)
    {
        if (boss)
        {
            objectives += 1;
        }
        else
        {
            enemiesKilled += 1;
        }
    }
}
