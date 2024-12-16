using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MEnemigos : MonoBehaviour
{

    private float speed;
    public GameObject spawnerEnemigos;

    void Update()
    {
        speed = 1.5f * (GameManager.gameManager.VMovimiento);
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        Destroy();
    }

    private void Destroy()
    {
        if (transform.position.x <= -15f)
        {
            Destroy(gameObject);
        }
    }
}
