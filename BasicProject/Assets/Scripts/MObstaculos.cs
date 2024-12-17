using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MObstaculos : MonoBehaviour
{
    private float speed;

    void Update()
    {
        speed = GameManager.gameManager.VMovimiento;
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