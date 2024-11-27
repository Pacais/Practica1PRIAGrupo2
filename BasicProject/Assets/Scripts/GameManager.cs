using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    private bool isSpawning;
    private float minWait;
    private float maxWait;
    public GameObject obstaculoPrefab;
    public GameObject spawnerObstaculos;

    public int vidaJugador = 3;  
    public TextMeshProUGUI vidaText;  
    public GameObject gameOverPanel;  


    void Start()
    {
        isSpawning = false;
        minWait = 1f;
        maxWait = 2.5f;

        gameOverPanel.SetActive(false);
        ActualizarVidaUI();
    }

    void Update()
    {
        if (vidaJugador <= 0)
        {
           
            GameOver();
        }
        else
        {
            if (!isSpawning)
            {
                float timer = Random.Range(minWait, maxWait);
                Invoke("SpawnObjects", timer);
                isSpawning = true;
            }

           
            if (Input.GetKeyDown(KeyCode.R) && vidaJugador <= 0)
            {
                ReiniciarJuego();
            }
        }
    }

    private void SpawnObjects()
    {
        Instantiate(obstaculoPrefab, spawnerObstaculos.transform.position, Quaternion.identity);
        isSpawning = false;
    }

    public void ReducirVida()
    {
        vidaJugador--;  

        if (vidaJugador <= 0)
        {
            
            GameOver();
        }

        ActualizarVidaUI();  
    }

    private void ActualizarVidaUI()
    {
        
        vidaText.text = "Vida: " + vidaJugador.ToString();
    }

    private void GameOver()
    {
        
        Time.timeScale = 0f;  
        gameOverPanel.SetActive(true);  
    }

    private void ReiniciarJuego()
    {

      
        Time.timeScale = 1f; 
        vidaJugador = 3;  
        ActualizarVidaUI();  

      
        gameOverPanel.SetActive(false);

        
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
