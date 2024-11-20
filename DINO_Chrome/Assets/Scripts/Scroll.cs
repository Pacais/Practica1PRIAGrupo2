using UnityEngine;

public class Scroll : MonoBehaviour
{

    void Update()
    {
        transform.Translate(Vector2.left * GameManager.Instance.GetScrollSpeed() * Time.deltaTime);
    }
}
