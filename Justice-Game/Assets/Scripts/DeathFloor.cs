using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //Destroy(col.gameObject);
            col.GetComponent<PlayerControls>().Death();
        }
    }
}
