using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MEnemigos : MonoBehaviour
{
    private float VMovimiento = 12f;
    public GameObject spawnerObstaculos;

    void Update()
    {
        transform.Translate(Vector3.left * VMovimiento * Time.deltaTime);
        Destroy();
    }

    private void Destroy()
    {
        if (transform.position.y <= -12f)
        {
            Destroy(gameObject);
        }
    }
}
