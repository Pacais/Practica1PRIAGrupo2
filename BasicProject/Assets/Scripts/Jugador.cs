using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer; // Para manejar el parpadeo del sprite
    private Animator animator;
    public PolygonCollider2D jugadorCollider; // Collider del jugador
    public BoxCollider2D crouchCollider; // Collider del jugador agachado
    private int saltosDisponibles = 2;
    private float fuerzaSalto = 25f;
    private float duracionInvulnerable = 1.0f; // Duracion de la invulnerabilidad
    private bool isCrouching = false;
    private bool esInvulnerable = false; // Indica si el jugador esta en estado invulnerable

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtenemos el SpriteRenderer del jugador
        jugadorCollider = GetComponent<PolygonCollider2D>(); // Obtenemos el Collider2D del jugador
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //--------------------------------------- Salto --------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Space) && saltosDisponibles > 0)
        {
            animator.SetBool("Jumping", true);  // Activamos la animación con el bool de unity
            rb2D.AddForce(Vector3.up * fuerzaSalto, ForceMode2D.Impulse); // Indicamos la direción del salto y la potencia de este. Ponemos ForceMode2D.Force para que sea una cantidad fija.
            saltosDisponibles--;
        }

        //--------------------------------------- Agacharse ------------------------------------------------------
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)    // Activamos la animación mientras esté pulsada la tecla y no esté agachado
        {
            // Activamos el collider agachado y desactivamos el normal
            crouchCollider.enabled = true;
            jugadorCollider.enabled = false;
            animator.SetBool("Crouching", true);
            isCrouching = true; // Ponemos que está agachado a true
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))    // Cuando sueltas la tecla (Input.GetKeyUp) se desactiva la animacion
        {
            animator.SetBool("Crouching", false);
            // Dejamos el collider agachado desactivado y activamos el normal
            crouchCollider.enabled = false;
            jugadorCollider.enabled = true;
            isCrouching = false;    // Ponemos que está agachado a false, para que al soltar la tecla pueda volver a agacharse o saltar
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Suelo"))
        {
            animator.SetBool("Jumping", false);
            saltosDisponibles = 2;
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

        // Activar inmediatamente la animación de golpeado correspondiente
        if (isCrouching)
        {
            animator.ResetTrigger("GolpeadoRunning");
            animator.SetTrigger("GolpeadoCrouching");
            Debug.Log("Jugador golpeado mientras está agachado");
        }
        else
        {
            animator.ResetTrigger("GolpeadoCrouching");
            animator.SetTrigger("GolpeadoRunning");
            Debug.Log("Jugador golpeado mientras está corriendo");
        }

        // Parpadeo del sprite durante la invulnerabilidad
        for (float i = 0; i < duracionInvulnerable; i += 0.2f)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        // Restaurar las colisiones con obstáculos después de la invulnerabilidad
        IgnorarColisionesConObstaculos(false);

        // Restaurar animación normal al finalizar la invulnerabilidad
        if (isCrouching)
        {
            animator.ResetTrigger("GolpeadoCrouching");
            animator.SetBool("Crouching", true); // Restaurar animación de agachado
            Debug.Log("Restaurando animación de agachado");
        }
        else
        {
            animator.ResetTrigger("GolpeadoRunning");
            animator.SetBool("Crouching", false); // Asegurarse de que no esté agachado
            animator.Play("DragonRun"); 
            Debug.Log("Restaurando animación de correr");
        }

        esInvulnerable = false; // Desactivar estado invulnerable
        Debug.Log("Invulnerabilidad terminada");
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
                Physics2D.IgnoreCollision(colliderObstaculo, crouchCollider, ignorar);
                Physics2D.IgnoreCollision(colliderObstaculo, jugadorCollider, ignorar);
            }
        }
    }
}