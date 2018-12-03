using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdMotionScript : MonoBehaviour
{
    [SerializeField]
    float speed;
    float vector_y;
    [SerializeField]
    float gravity = 9.8f;
    float default_y;

    private void Start()
    {
        default_y = transform.position.y;
        Jump();
    }

    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y + vector_y * Time.deltaTime, transform.position.z);

        if (default_y > transform.position.y)
        {
            this.transform.position = new Vector3(transform.position.x, default_y, transform.position.z);
        }
        else
        {
            vector_y -= gravity * Time.deltaTime;
        }
    }

    public void Jump()
    {
        this.transform.position = new Vector3(transform.position.x, default_y, transform.position.z);
        vector_y = speed;
    }
}
