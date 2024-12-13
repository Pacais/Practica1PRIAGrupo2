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
    private bool isSpawningObstaculos;
    private bool isSpawningEnemigos;
    public GameObject obstaculoPrefab;
    public GameObject enemigoPrefab;
    public GameObject spawnerObstaculos;
    public GameObject spawnerEnemigos;
    public GameObject gameOver;
    public TextMeshProUGUI vidaText;
    public GameObject[] vidas;


    public int puntosParaVidaExtra = 1000;
    private int puntosSiguienteVida;


    void Awake(){
        if(gameManager != null && gameManager !=this){
            Destroy(gameObject);
            return;
        }
        gameManager=this;

    }
    void Start()
    {
        isSpawningObstaculos = false;
        isSpawningEnemigos = false;
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
            if (!isSpawningObstaculos)
            {
                float timer = Random.Range(minWaitObstaculos, maxWaitObstaculos);
                Invoke("SpawnObjects", timer);
                isSpawningObstaculos = true;
            }

            if (!isSpawningEnemigos)
            {
                float timer = Random.Range(minWaitEnemigos, maxWaitEnemigos);
                Invoke("SpawnEnemigos", timer);
                isSpawningEnemigos = true;
            }
        }
        PuntosTiempo();
        CambioVelocidad();
    }

    private void SpawnObjects()
    {
        Instantiate(obstaculoPrefab, spawnerObstaculos.transform.position, Quaternion.identity);
        isSpawningObstaculos = false;
    }

    private void SpawnEnemigos()
    {

    float alturaDeseada = 5f; 
    Vector3 nuevaPosicion = new Vector3(
        spawnerObstaculos.transform.position.x,
        alturaDeseada,
        spawnerObstaculos.transform.position.z   
    );

        
    Instantiate(enemigoPrefab, nuevaPosicion, Quaternion.identity);

    isSpawningEnemigos = false;
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

    /*private void ActualizarVidaUI()
    {
        vidas[vidaJugador].SetActive(false);
    }*/

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

