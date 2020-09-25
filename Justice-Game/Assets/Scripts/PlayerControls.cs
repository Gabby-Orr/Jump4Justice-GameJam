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

    [SerializeField] private float jumpVelo = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.position = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);

        //Debug.Log(power);

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
    }

    private bool IsGrounded()
    {
        
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(col.bounds.center, col.radius, Vector2.down, .1f, platformMask);
        return raycastHit2D.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            //whatever happens here

            //This allows custom damage to player for each enemy, i.e. bigger enemies can cause more damage - travis
            EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
            int ouch = enemy.damageDelt;
            Debug.Log("Player contact with enemy - damage: " + ouch);
        }
    }
}
