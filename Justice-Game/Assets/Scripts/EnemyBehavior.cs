using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public float verticalSpeed;
    public float horizontalSpeed;
    public bool boss;
    public bool projectileDamage; //Can the enemy incur damage from projectiles?
    public bool headJumpDamage;   //Can the enemy incur damage from a player jumping on its head?
    public int headJumpDamageSuffered; //How much?
    public int damageDealt;        //How much damage does the enemy cause a player on a collision? (Accessed form PlayerControls.cs)
    public int health=10;            //Health of the enemy

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0);
        if (health <= 0)
        {
            //player.KilledEnemy(this.boss);
            Destroy(this.gameObject);

        }
    }

    public void HeadJump(PlayerControls player) {
        if (health - headJumpDamageSuffered <= 0) player.KilledEnemy(boss);
        health -= headJumpDamageSuffered;
    }


    //for now, the enemy changes directions when it collides with something
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        verticalSpeed *= -1;
        horizontalSpeed *= -1;
        //Debug.Log("(Enemy) Collision");

        if(collision.gameObject.tag.Equals("Projectile")){
            //TODO: Get damage from projectile itself, similar to how the enemy deals damage to the player
            health-=2;
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag.Equals("Player")) {
            health-=headJumpDamageSuffered;
        }
    }
    */
}
