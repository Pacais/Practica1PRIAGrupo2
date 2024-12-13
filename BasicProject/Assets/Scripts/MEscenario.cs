using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MEscenario : MonoBehaviour
{
    private float speed;
    
    void Update()
    {
        speed = GameManager.gameManager.VMovimiento; 
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Repeticion de suelo
        if(transform.position.x <= -28f)
        {
            transform.Translate(Vector3.right * 28f * 2f);
        }
    }
}