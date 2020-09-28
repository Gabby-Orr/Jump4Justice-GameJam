using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimBehavior : MonoBehaviour
{
    public bool floatAway;
    void Start()
    {
        
    }

    public void Update()
    {
        if (transform.position.y > 30)
        {
            Destroy(this.gameObject);
        }

        if (floatAway)
        {
            transform.position += new Vector3(0, 5*Time.deltaTime, 0);
        }
    }
    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("DeathCeiling"))
    //    {
    //        gameObject.SetActive(false);
           
    //        //Destroy(col.gameObject);
    //    }
    //}
}
