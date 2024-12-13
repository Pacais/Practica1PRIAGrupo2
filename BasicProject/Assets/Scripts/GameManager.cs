using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text puntosTexto;
    public static GameManager gameManager;
    private int puntosSiguienteVida;
    private int puntos;
    public int puntosSegundo = 10;
    public int vidaJugador = 3;
    public int puntosParaVidaExtra = 1000;
    public float VMovimiento = 8f;
    private float minWaitObstaculos;
    private float maxWaitObstaculos;
    private float minWaitEnemigos;
    private float maxWaitEnemigos;
    private float timer;
    private bool isSpawningCrystals;
    private bool isSpawningBats;
    private bool isSpawningSpiders;
    public GameObject crystalPrefab;
    public GameObject batPrefab;
    public GameObject spiderPrefab;
    public GameObject spawnerObstaculos;
    public GameObject spawnerEnemigos;
    public GameObject gameOver;
    public TextMeshProUGUI vidaText;
    public GameObject[] vidas;

    void Awake(){
        if(gameManager != null && gameManager !=this){
            Destroy(gameObject);
            return;
        }
        gameManager=this;

    }
    void Start()
    {
        isSpawningCrystals = false;
        isSpawningBats = false;
        minWaitObstaculos = 1f;
        maxWaitObstaculos = 2.5f;
        minWaitEnemigos = 2f;
        maxWaitEnemigos = 4f;
        puntos = 0;
    }

    void Update()
    {
        if (vidaJugador <= 0)
        {

            GameOver();
        }
        else
        {
            if (!isSpawningCrystals)
            {
                float timer = Random.Range(minWaitObstaculos, maxWaitObstaculos);
                Invoke("SpawnCrystals", timer);
                isSpawningCrystals = true;
            }

            if (!isSpawningBats)
            {
                float timer = Random.Range(minWaitEnemigos, maxWaitEnemigos);
                Invoke("SpawnBats", timer);
                isSpawningBats = true;
            }

            if (!isSpawningSpiders)
            {
                float timer = Random.Range(minWaitEnemigos, maxWaitEnemigos);
                Invoke("SpawnSpiders", timer);
                isSpawningCrystals = true;
            }
        }
        PuntosTiempo();
        CambioVelocidad();
    }

//------------------------------------- Spawner Crystals ------------------------------------------------
    private void SpawnCrystals()
    {
        Instantiate(crystalPrefab, spawnerObstaculos.transform.position, Quaternion.identity);
        isSpawningCrystals = false;
    }

//------------------------------------- Spawner Bats ------------------------------------------------
    private void SpawnBats()
    {
    float alturaDeseada = 5f; 
    Vector3 nuevaPosicion = new Vector3(spawnerObstaculos.transform.position.x, alturaDeseada, spawnerObstaculos.transform.position.z);

    Instantiate(batPrefab, nuevaPosicion, Quaternion.identity);

    isSpawningBats = false;
    }

//------------------------------------- Spawner Spiders ------------------------------------------------
    private void SpawnSpiders()
    {
    float alturaDeseada = 8f; 
    Vector3 nuevaPosicion = new Vector3(spawnerObstaculos.transform.position.x, alturaDeseada, spawnerObstaculos.transform.position.z);

    Instantiate(spiderPrefab, nuevaPosicion, Quaternion.identity);

    isSpawningCrystals = false;
    }

    public void ReducirVida()
    {
        vidaJugador--;

        if (vidaJugador <= 0)
        {
            GameOver();
        }

        RestarVidaUI();
    }



    private void RestarVidaUI()
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

    private void PuntosTiempo()
    {
        timer += Time.deltaTime;
        puntos = (int)(timer * puntosSegundo);


        if (puntos >= puntosSiguienteVida)
        {
            AumentarVida();
            puntosSiguienteVida += puntosParaVidaExtra;
        }
        Debug.Log(puntos);
        puntosTexto.text = string.Format("{0:00000}", puntos);
    }

    public void AumentarVida()
    {
        if (vidaJugador < 3)
        {
            vidas[vidaJugador].SetActive(true);
            vidaJugador++;
            Debug.Log(vidaJugador);
        }
    }
    private void CambioVelocidad()
    {
        if (puntos % 100 == 0 && puntos > 0){
            VMovimiento *= 1.03f;
            Debug.Log(VMovimiento);
        }
    }
}