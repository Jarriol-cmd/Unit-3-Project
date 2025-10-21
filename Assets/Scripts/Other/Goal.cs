using UnityEngine;
using UnityEngine.UIElements;

public class Goal : MonoBehaviour
{

    public float rotationsPerMinute = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0f, 6f * rotationsPerMinute * Time.deltaTime);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


}
