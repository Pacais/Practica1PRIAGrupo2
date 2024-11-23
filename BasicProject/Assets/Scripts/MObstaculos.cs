using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MObstaculos : MonoBehaviour
{
    private float VMovimiento = 8f;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.left * VMovimiento * Time.deltaTime);
    }
}
