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
    private float minWait = 1f;
    private float maxWait = 3f;
    private float timer;
    public float VMovimiento = 8f;
    private bool isSpawning = false;
    public GameObject crystalPrefab;
    public GameObject batPrefab;
    public GameObject CrystalBatPrefab;
    public GameObject spiderPrefab;
    public GameObject BatSpiderPrefab;
    public GameObject CrystalSpiderPrefab;
    public GameObject spawnerObstaculos;
    public GameObject gameOver;
    public GameObject[] obstaculos;
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
             if (!isSpawning){
                float timer = Random.Range(minWait, maxWait);
                Invoke("Spawn", timer);
                isSpawning = true;
            }
        }
        PuntosTiempo();
        CambioVelocidad();
    }
//----------------------------------------- SPAWN OBSTACULOS -------------------------------------------------------
    private void Spawn()
    {
        
        int probabilidad = Random.Range(1,100);
        int indice = Probabilidades(probabilidad);
        indice = SpawnPuntos(indice);
        Instantiate(obstaculos[indice], obstaculos[indice].transform.position, Quaternion.identity);
        isSpawning = false;

    }
    private int Probabilidades(int probabilidad)
    {
        switch(probabilidad)    // Probabilidad de que aparezcan cada uno de los prefabs del array (obstaculos)
        {
            case <= 10: //10%
                return 5; 
            case <= 40: //30%
                return 0;
            case <= 60: //20%
                return 1;
            case <= 70: // 10%
                return 2;
            case <= 80: // 10%
                return 4;
            case <= 100: // 20%
                return 3;
            default:
                return 0;               
        }
    }

    private int SpawnPuntos(int indice){    // Limitar el spawn de obstaculos por puntos
        if(puntos < 100){
            indice = 0;
        }
        else if(puntos < 200){
            indice = Random.Range(0, 1);
        }
        else if(puntos < 300){
            indice = Random.Range(0, 3);
        }
        return indice;
    }

//----------------------------------------- PERDER VIDAS -------------------------------------------------------
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

//----------------------------------------- GAME OVER -------------------------------------------------------
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

//----------------------------------------- VIDAS EXTRA -------------------------------------------------------
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