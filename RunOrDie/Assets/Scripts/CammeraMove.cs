using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CammeraMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Transform player;
    private Transform camera;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
        camera = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.tag != "player")
            player = GameObject.FindGameObjectWithTag("player").transform;
        if (player != null)
        {
            var camerapos = camera.position;
            camerapos.z = Mathf.Lerp(camerapos.z, player.position.z - 15, speed);
            camerapos.x = Mathf.Lerp(camerapos.x, player.position.x, speed);
            camerapos.y = Mathf.Lerp(camerapos.y, player.position.y + 5, speed);
            camera.position = camerapos;
        }
    }
}
