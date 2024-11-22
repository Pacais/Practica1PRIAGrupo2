using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEscenario : MonoBehaviour
{
    private float VMovimiento = 8f;
    private Vector3 currentposition;
    
    void Start()
    {

    }

    
    void Update()
    {
        transform.Translate(Vector3.left * VMovimiento * Time.deltaTime);

        if(transform.position.x <= -transform.localScale.x)
        {
            transform.Translate(Vector3.right * transform.localScale.x * 2f);
        }
    }
}
