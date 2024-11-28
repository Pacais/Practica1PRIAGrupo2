using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private float fuerzaSalto = 500f;

    private Rigidbody2D rb2D;
    private bool estaEnElSuelo = true; 

    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {   
        if (estaEnElSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(new Vector3(0, fuerzaSalto));
            estaEnElSuelo = false;
        }
    }

    
    private void OnCollisionEnter2D(Collision2D col)
    {
       
        if (col.gameObject.CompareTag("Suelo"))
        {
            estaEnElSuelo = true; 
        }
    }
}
