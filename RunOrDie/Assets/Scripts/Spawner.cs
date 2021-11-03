using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public int methoddone = 0;
    public GameObject[] transports;
    public GameObject spawnpoint;
    public float speed;
    private float timerspawner;
    private float timeforspawn;
    private GameObject[] clones = new GameObject[1000];
    [SerializeField]
    private MainMenu mainMenu ;
    public int iterator = 0;
    public void Awake()
    {
        switch (ForData.dificulty)
        {
            case "easy":
                timeforspawn = 4f;
                break;
            case "normal":
                timeforspawn = 3f;
                break;
            case "hard":
                timeforspawn = 1f;
                break;
            default:
                break;
        }
    }
    public void Spawn()
    {
        if (methoddone < transports.Length)
        {
            spawnpoint.transform.position = new Vector3(x: Random.RandomRange(-14.41f, 14.69f), y: 214.8f, z: 352.4f);
            clones[iterator] = PhotonNetwork.Instantiate(transports[methoddone].name, spawnpoint.transform.position, Quaternion.identity);
            clones[iterator].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -speed), ForceMode.Impulse); ;
            ++methoddone;
            Destroy(clones[iterator], 15);
            ++iterator;
        }
        else
        {
            methoddone = 0;
            spawnpoint.transform.position = new Vector3(x: Random.RandomRange(-14.41f, 14.69f), y: 214.8f, z: 352.4f);
            clones[iterator] = PhotonNetwork.Instantiate(transports[methoddone].name, spawnpoint.transform.position, Quaternion.identity);
            clones[iterator].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -speed), ForceMode.Impulse); ;
            ++methoddone;
            Destroy(clones[iterator], 15);
            ++iterator;
        }

    }
    public void DeleteAllClones()
    {
        for (int i = 0; i < clones.Length; i++)
        {
            PhotonNetwork.Destroy(clones[i]);
        }
    }
    void FixedUpdate()
    {
        timerspawner += Time.fixedDeltaTime;
        if (timerspawner >= timeforspawn)
        {
            Spawn();
            timerspawner = 0;
        }
    }
}
