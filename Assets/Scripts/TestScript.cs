using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Floor[] floors;
    public float speed;


    void Start()
    {
        floors = new Floor[5]; 
        for(int i = 0; i < floors.Length; i++)
        {
            floors[i] = new Floor();
            if (i == 0)
            {
                floors[i].peopleCount = 0;
            }
            else
            {
                floors[i] .peopleCount = Random.Range(0, 10);
            }
            Debug.Log("Floor ¹" + (i + 1) + " = " + floors[i].peopleCount);
        }
    }


    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(moveHorizontal, 0, moveVertical);

        transform.position = transform.position + movementVector * speed * Time.deltaTime;

    }
}
