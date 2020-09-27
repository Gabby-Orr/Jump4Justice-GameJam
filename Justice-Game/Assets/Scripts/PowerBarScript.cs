using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarScript : MonoBehaviour
{

    private Transform bar;

    private void Awake ()
    {
        bar = transform.Find("PBar");
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(4 * sizeNormalized, 0.7f);
    }
}
