using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MEscenario : MonoBehaviour
{
    private float VMovimiento = 8f;
    
    void Start()
    {

    }

    
    void Update()
    {
        transform.Translate(Vector3.left * VMovimiento * Time.deltaTime);

        // Repeticion de suelo
        if(transform.position.x <= -transform.localScale.x)
        {
            transform.Translate(Vector3.right * transform.localScale.x * 2f);
        }
    }
}
