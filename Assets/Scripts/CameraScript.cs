using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var temp = transform.position;
        temp.x = player.position.x;
        temp.y = player.position.y;
        transform.position = temp;
    }
}
