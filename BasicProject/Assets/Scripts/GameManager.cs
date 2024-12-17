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
    private float minWait = 1.5f;
    private float maxWait = 4f;
    private float timer;
    public float VMovimiento = 8f;
    private bool isSpawningCrystals = false;
    private bool isSpawningCrystalBat = false;
    private bool isSpawningBats = false;
    private bool isSpawningSpiders = false;
    private bool isSpawningBatSpider = false;
    private bool isSpawningCrystalSpider = false;
    public GameObject crystalPrefab;
    public GameObject batPrefab;
    public GameObject CrystalBatPrefab;
    public GameObject spiderPrefab;
    public GameObject BatSpiderPrefab;
    public GameObject CrystalSpiderPrefab;
    public GameObject spawnerObstaculos;
    public GameObject gameOver;
    public TextMeshProUGUI vidaText;
    public GameObject[] vidas;

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
            return;
        }
        gameManager = this;
    }

    void Start()
    {
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
                float timer = Random.Range(minWait, maxWait);
                Invoke("SpawnCrystals", timer);
                isSpawningCrystals = true;
            }

            if (!isSpawningBats && puntos > 100)
            {
                float timer = Random.Range(minWait, maxWait);
                Invoke("SpawnBats", timer);
                isSpawningBats = true;
            }

            if (!isSpawningCrystalBat && puntos > 200)
            {
                float timer = Random.Range(minWait, maxWait);
                Invoke("SpawnCrystalBat", timer);
                isSpawningCrystalBat = true;
            }

            if (!isSpawningSpiders && puntos > 300)
            {
                float timer = Random.Range(minWait, maxWait);
                Invoke("SpawnSpiders", timer);
                isSpawningSpiders = true;
            }

            if (!isSpawningBatSpider && puntos > 400)
            {
                float timer = Random.Range(minWait, maxWait);
                Invoke("SpawnBatSpider", timer);
                isSpawningBatSpider = true;
            }

            if (!isSpawningCrystalSpider && puntos > 500)
            {
                float timer = Random.Range(minWait, maxWait);
                Invoke("SpawnCrystalSpider", timer);
                isSpawningCrystalSpider = true;
            }
        }
        PuntosTiempo();
        CambioVelocidad();
    }

    //------------------------------------- Spawner Crystals ------------------------------------------------
    private void SpawnCrystals()
    {
        Instantiate(crystalPrefab, crystalPrefab.transform.position, Quaternion.identity);
        isSpawningCrystals = false;
    }
    //------------------------------------- Crystals & Bats ------------------------------------------------
    private void SpawnCrystalBat()
    {
        Instantiate(CrystalBatPrefab, CrystalBatPrefab.transform.position, Quaternion.identity);
        isSpawningCrystalBat = false;
    }

    //------------------------------------- Spawner Bats --------------------------------------------------
    private void SpawnBats()
    {
        Instantiate(batPrefab, batPrefab.transform.position, Quaternion.identity);
        isSpawningBats = false;
    }

    //------------------------------------- Spawner Spiders ------------------------------------------------
    private void SpawnSpiders()
    {
        Instantiate(spiderPrefab, spiderPrefab.transform.position, Quaternion.identity);
        isSpawningSpiders = false;
    }

    //------------------------------------- Bat & Spider ------------------------------------------------
    private void SpawnBatSpider()
    {
        Instantiate(BatSpiderPrefab, BatSpiderPrefab.transform.position, Quaternion.identity);
        isSpawningBatSpider = false;
    }

    //------------------------------------- Bat & Spider ------------------------------------------------
    private void SpawnCrystalSpider()
    {
        Instantiate(CrystalSpiderPrefab, CrystalSpiderPrefab.transform.position, Quaternion.identity);
        isSpawningCrystalSpider = false;
    }

    //------------------------------------- Perder Vidas ------------------------------------------------
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

    //------------------------------------- Game Over ------------------------------------------------
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
        puntosTexto.text = string.Format("{0:00000}", puntos);
    }

    public void AumentarVida()
    {
        if (vidaJugador < 3)
        {
            vidas[vidaJugador].SetActive(true);
            vidaJugador++;
        }
    }
    private void CambioVelocidad()
    {
        if (puntos % 200 == 0 && puntos > 0)
        {
            VMovimiento *= 1.03f;
        }
    }
}