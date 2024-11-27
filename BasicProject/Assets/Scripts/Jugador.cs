using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float fuerzaSalto;
    private Rigidbody2D rd2D;
    private bool estaEnElSuelo = true;

    private GameManager gameManager;  

    // Start is called before the first frame update
    void Start()
    {
        rd2D = GetComponent<Rigidbody2D>();

        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (estaEnElSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rd2D.AddForce(new Vector2(0, fuerzaSalto));
            estaEnElSuelo = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Suelo"))
        {
            estaEnElSuelo = true;
        }
        else if (col.gameObject.CompareTag("Obstaculo"))
        {
            
            gameManager.ReducirVida();
        }
    }
}
