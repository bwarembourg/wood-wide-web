using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * speed, 0, 0);
        if (transform.position.x > 12f)
        {
            transform.position = new Vector3(-12f, transform.position.y, transform.position.z);
        } 
    }
}
