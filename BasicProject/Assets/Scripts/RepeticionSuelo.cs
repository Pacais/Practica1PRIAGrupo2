using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeticionSuelo : MonoBehaviour
{
    private float AnchoSuelo;
   

    void Start()
    {
        BoxCollider2D groundCollider = GetComponent<BoxCollider2D>();
        AnchoSuelo = groundCollider.size.x;
    }

    
    void Update()
    {
        if(transform.position.x < -AnchoSuelo)
        {
            transform.Translate(Vector2.right * 2f * AnchoSuelo);
        }
    }
}
