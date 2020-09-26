using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHatBehavior : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag.Equals("Player")) {
            EnemyBehavior parent = this.transform.parent.GetComponent<EnemyBehavior>();
            parent.HeadJump();
        }
    }
}
