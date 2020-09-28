using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBarScript : MonoBehaviour
{
    private Transform bar;

    private void Awake()
    {
        bar = transform.Find("DBar");
    }

    public void SetSize(float sizeNormalized)
    {
        if(sizeNormalized > 0)
        {
            bar.localScale = new Vector3(4 * sizeNormalized, 0.7f);
        }
    }
}
