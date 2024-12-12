using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private float fuerzaSalto = 95f;
    private float potenciaSalto = 0;
    bool canJump = true;
    private Rigidbody2D rb2D;
    private bool estaEnElSuelo = true;
    private bool isCrouching = false;
    private GameManager gameManager;
    private Animator animator;
    private bool esInvulnerable = false; // Indica si el jugador esta en estado invulnerable
    private float duracionInvulnerable = 1.0f; // Duracion de la invulnerabilidad
    private SpriteRenderer spriteRenderer; // Para manejar el parpadeo del sprite
    public BoxCollider2D jugadorCollider; // Collider del jugador
    public BoxCollider2D crouchCollider; // Collider del jugador agachado

    void Awake(){
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtenemos el SpriteRenderer del jugador
        jugadorCollider = GetComponent<BoxCollider2D>(); // Obtenemos el Collider2D del jugador
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        potenciaSalto = fuerzaSalto;
        //------------------------------------- Salto variable ---------------------------------------------------
        if (estaEnElSuelo && Input.GetKey(KeyCode.Space) && canJump && !isCrouching)
        {
            animator.SetBool("Jumping", true);  // Activamos la animación con el bool de unity
            rb2D.AddForce(Vector3.up * potenciaSalto, ForceMode2D.Force);   // Indicamos la direción del salto y la potencia de este. Ponemos ForceMode2D.Force para que sea una cantidad fija.
            Invoke("StopJumping", 0.4f);    //Llamamos a StopJumping para que el salto dure el tiempo determinado (0.4s)
        }

        //--------------------------------------- Agacharse ------------------------------------------------------
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)    // Activamos la animación mientras esté pulsada la tecla y no esté agachado
        {
            // Activamos el collider agachado y desactivamos el normal
            crouchCollider.enabled = true;
            jugadorCollider.enabled = false;
            //jugadorCollider.size = new Vector2(3f, 1.5f); // Cambiamos el tamaño del collider al agacharse
            animator.SetBool("Crouching", true);
            isCrouching = true; // Ponemos que está agachado a true
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))    // Cuando sueltas la tecla (Input.GetKeyUp) se desactiva la animacion
        {
            animator.SetBool("Crouching", false);
            // Dejamos el collider agachado desactivado y activamos el normal
            crouchCollider.enabled = false;
            jugadorCollider.enabled = true;
            //jugadorCollider.size = new Vector2(1.8f, 2.8f);   // Volvemos al tamaño normal el collider
            isCrouching = false;    // Ponemos que está agachado a false, para que al soltar la tecla pueda volver a agacharse o saltar
        }
    }
    void StopJumping()
    {
        canJump = false;
        estaEnElSuelo = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Suelo"))
        {
            animator.SetBool("Jumping", false);
            estaEnElSuelo = true;
            canJump = true;
        }
        else if (col.gameObject.CompareTag("Obstaculo"))
        {
            // Solo reducir vida si no es invulnerable
            if (!esInvulnerable)
            {
                gameManager.ReducirVida();
                StartCoroutine(ActivarInvulnerabilidad()); // Activar invulnerabilidad e ignorar todas las colisiones con obstaculos
            }
        }

        else if (col.gameObject.CompareTag("Enemigo"))
        {
            // Solo reducir vida si no es invulnerable
            if (!esInvulnerable)
            {
                gameManager.ReducirVida();
                StartCoroutine(ActivarInvulnerabilidad()); // Activar invulnerabilidad e ignorar todas las colisiones con obst�culos
            }
        }
    }

    private IEnumerator ActivarInvulnerabilidad()
    {
        esInvulnerable = true; // Activar estado invulnerable

        // Ignorar todas las colisiones con objetos que tengan el tag "Obstaculo"
        IgnorarColisionesConObstaculos(true);

        // Parpadeo del sprite durante la invulnerabilidad
        for (float i = 0; i < duracionInvulnerable; i += 0.2f)
        {
            spriteRenderer.enabled = false; // Ocultar sprite
            yield return new WaitForSeconds(0.1f); // Esperar 0.1 segundos
            spriteRenderer.enabled = true; // Mostrar sprite
            yield return new WaitForSeconds(0.1f); // Esperar 0.1 segundos
        }

        // Restaurar las colisiones con obstaculos despues de la invulnerabilidad
        IgnorarColisionesConObstaculos(false);

        esInvulnerable = false; // Desactivar estado invulnerable
    }

    private void IgnorarColisionesConObstaculos(bool ignorar)
    {
        // Obtener todos los objetos con tag "Obstaculo"
        GameObject[] obstaculos = GameObject.FindGameObjectsWithTag("Obstaculo");

        foreach (var obstaculo in obstaculos)
        {
            // Ignorar o restaurar la colision entre el jugador y los obstaculos
            if (obstaculo.TryGetComponent(out Collider2D colliderObstaculo))
            {
                Physics2D.IgnoreCollision(colliderObstaculo, jugadorCollider, ignorar);
            }
        }
    }
}