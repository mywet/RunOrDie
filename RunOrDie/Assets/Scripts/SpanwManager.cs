using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpanwManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Spawn;
    [SerializeField]
    private GameObject Player;

    private void Awake()
    {
        Vector3 randompos = Spawn[Random.Range(0, Spawn.Length)].transform.localPosition;
        PhotonNetwork.Instantiate(Player.name, randompos, Quaternion.identity);
    }
}
