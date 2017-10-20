using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField]
    int Tokens;
    Vector3 movement;
    [SerializeField]
    float speed;

    [SerializeField]
    GameObject[] spawnPrefabs;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("ChangeMovement", 0f, 1.0f);
        InvokeRepeating("PlaceObject", 5f, 5.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeMovement();
    }

    void Update()
    {
        rb.AddForce(movement * Time.deltaTime);
    }

    void PlaceObject()
    {
        if (Tokens > 0)
        {
            GameObject obj = (GameObject)Instantiate(spawnPrefabs[0], transform.position, transform.rotation);
            --Tokens;
            ChangeMovement();

            if (Tokens <= 0)
            {
                if (spawnPrefabs.Length > 1)
                {
                    GameObject nextSpawner = (GameObject)Instantiate(spawnPrefabs[1], transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }


    void ChangeMovement()
    {
        int random = Random.Range(0, 4);

        switch (random)
        {
            case 0:
                movement = new Vector3(-1, 0) * speed;
                break;
            case 1:
                movement = new Vector3(0, -1) * speed;
                break;
            case 2:
                movement = new Vector3(1, 0) * speed;
                break;
            case 3:
                movement = new Vector3(0, 1) * speed;
                break;
        }

    }
}
