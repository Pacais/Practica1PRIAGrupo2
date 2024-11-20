using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEscenario : MonoBehaviour
{
    private float VMovimiento = 8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * VMovimiento * Time.deltaTime);
    }
}
