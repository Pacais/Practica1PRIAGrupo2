using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private float fuerzaSalto = 500f;
    private Rigidbody2D rb2D;
    private bool estaEnElSuelo = true;
    private GameManager gameManager;
    private Animator animator;
    private bool esInvulnerable = false; // Indica si el jugador esta en estado invulnerable
    private float duracionInvulnerable = 1.0f; // Duracion de la invulnerabilidad
    private SpriteRenderer spriteRenderer; // Para manejar el parpadeo del sprite
    private Collider2D jugadorCollider; // Collider del jugador

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtenemos el SpriteRenderer del jugador
        jugadorCollider = GetComponent<Collider2D>(); // Obtenemos el Collider2D del jugador
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (estaEnElSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Jumping", true);
            rb2D.AddForce(new Vector3(0, fuerzaSalto));
            estaEnElSuelo = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Suelo"))
        {
            animator.SetBool("Jumping", false);
            estaEnElSuelo = true;
        }
        else if (col.gameObject.CompareTag("Obstaculo"))
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

        // Restaurar las colisiones con obst�culos despu�s de la invulnerabilidad
        IgnorarColisionesConObstaculos(false);

        esInvulnerable = false; // Desactivar estado invulnerable
    }

    private void IgnorarColisionesConObstaculos(bool ignorar)
    {
        // Obtener todos los objetos con tag "Obstaculo"
        GameObject[] obstaculos = GameObject.FindGameObjectsWithTag("Obstaculo");

        foreach (var obstaculo in obstaculos)
        {
            // Ignorar o restaurar la colisi�n entre el jugador y los obst�culos
            if (obstaculo.TryGetComponent(out Collider2D colliderObstaculo))
            {
                Physics2D.IgnoreCollision(colliderObstaculo, jugadorCollider, ignorar);
            }
        }
    }
}