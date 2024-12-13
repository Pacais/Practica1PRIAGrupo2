using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MEscenario : MonoBehaviour
{
    public GameManager gameManager;
    private float VMovimiento = 8f;

    
    void Update()
    {
        transform.Translate(Vector3.left * gameManager.VMovimiento * Time.deltaTime);

        // Repeticion de suelo
        if(transform.position.x <= -28f)
        {
            transform.Translate(Vector3.right * 28f * 2f);
        }
    }
}
