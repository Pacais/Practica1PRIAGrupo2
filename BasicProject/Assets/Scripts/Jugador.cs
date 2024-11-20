using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float fuerzaSalto;

    private Rigidbody2D rigidbody2D;
    private bool estaEnElSuelo = true; 

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (estaEnElSuelo && Input.GetKeyDown(KeyCode.Space))
        {
           
            rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
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

