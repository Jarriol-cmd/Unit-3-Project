using UnityEngine;
using UnityEngine.UIElements;

public class FunnySpin : MonoBehaviour
{

    public float rotationsPerMinute = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(6f * rotationsPerMinute * Time.deltaTime, 6f * rotationsPerMinute * Time.deltaTime, 6f * rotationsPerMinute * Time.deltaTime);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Destroy(gameObject);
    }


}
