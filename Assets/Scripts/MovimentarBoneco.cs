using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarBoneco : MonoBehaviour
{
    public Transform bonecoTransform;
    public float speed;

    void Start()
    {
        bonecoTransform = gameObject.GetComponent<Transform>();


    }

    void Update()
    {
        Vector3 novaPosicao = bonecoTransform.position;

        if (Input.GetKey(KeyCode.D)) 
        {
            novaPosicao.x += (speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            novaPosicao.x -= (speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            novaPosicao.z -= (speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            novaPosicao.z += (speed * Time.deltaTime);
        }



        bonecoTransform.position = novaPosicao;
    }
}