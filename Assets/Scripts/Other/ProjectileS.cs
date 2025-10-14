using UnityEngine;

public class ProjectileS : MonoBehaviour
{
    
    float timer;
    Rigidbody2D RB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        timer = 4;
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
            timer = 4;
        }
    }
}
