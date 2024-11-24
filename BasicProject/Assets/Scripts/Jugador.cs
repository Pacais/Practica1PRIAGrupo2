using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    public float fuerzaSalto;

    private Rigidbody2D rb2D;
    private bool estaEnElSuelo = true; 

    public int vidas = 3;

    public TextMeshProUGUI vidasText;

    private bool estaMuerto = false;

    public GameObject panelGameOver;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        ActualizarVidas();

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);  
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if (estaEnElSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(new Vector2(0, fuerzaSalto));
            estaEnElSuelo = false;
        }

         if (vidas <= 0 && !estaMuerto)
        {
            Muerte(); 
        }
        
        if (estaMuerto && Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarJuego();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Suelo"))
        {
            estaEnElSuelo = true; 
        }

        if (col.gameObject.CompareTag("Obstaculo"))
        {
        RecibirDanio(1); 
        }
    }

     public void RecibirDanio(int dano)
    {
        if (!estaMuerto) 
        {
            vidas -= dano;
            ActualizarVidas(); 
            if (vidas <= 0)
            {
                vidas = 0;
                Muerte(); 
            }
        }
    }

    private void ActualizarVidas()
    {
        if (vidasText != null)
        {
            vidasText.text = "Vidas: " + vidas.ToString(); 
        }
    }

    private void Muerte()
    {
        estaMuerto = true;
        
        Debug.Log("El jugador ha muerto!");

        Time.timeScale = 0; 

        if (panelGameOver != null)
        {
        panelGameOver.SetActive(true); 
        }
        

    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
