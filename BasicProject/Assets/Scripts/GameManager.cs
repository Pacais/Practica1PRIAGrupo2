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
    public GameObject gameOver;
    public int vidaJugador = 3;
    public TextMeshProUGUI vidaText;
    public GameObject[] vidas;


    void Start()
    {
        isSpawning = false;
        minWait = 1f;
        maxWait = 2.5f;
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
        vidas[vidaJugador].SetActive(false);
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        gameOver.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReiniciarJuego();
        }
    }

    private void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        vidaJugador = 3;
        gameOver.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

    }
}