using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MObstaculos : MonoBehaviour
{
    public GameManager gameManager;
    private float VMovimiento = 8f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.left * gameManager.VMovimiento * Time.deltaTime);
        Destroy();
    }

    private void Destroy()
    {
        if(transform.position.x <= -15f){
            Destroy(gameObject);
        }
    }
}
