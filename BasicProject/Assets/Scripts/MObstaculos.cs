using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MObstaculos : MonoBehaviour
{
    private float VMovimiento = 8f;
    public GameObject spawnerObstaculos;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.left * VMovimiento * Time.deltaTime);
        Destroy();
    }

    private void Destroy()
    {
        if(transform.position.x <= -15f){
            Destroy(gameObject);
        }
    }
}
