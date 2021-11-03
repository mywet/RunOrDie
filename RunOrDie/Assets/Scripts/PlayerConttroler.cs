using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
public class PlayerConttroler : MonoBehaviour
{
    public PhotonView view;
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumppower;
    [SerializeField]
    private GameObject player;
    public GameObject raggdol;
    private float gravityforce;
    private Vector3 moveVector;
    private JoistickController joistickController;
    [SerializeField]
    private Spawner spawnerscript;
    private CharacterController characterController;
    private Animator animator;
    [SerializeField]
    private PlayerConttroler playerConttroler;
    private GameObject Contrrolers;
    private GameObject Interface;
    private Text text;
    private float timerstart = 0;
    private int countdown = 0;
    [SerializeField]
    private TextMeshPro nickname;
    private Spawner spawner;
    void Start()
    {

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        joistickController = GameObject.FindGameObjectWithTag("joistick").GetComponent<JoistickController>();
        nickname.SetText(view.Owner.NickName);
        Contrrolers = GameObject.FindGameObjectWithTag("contrrolers");
        Interface = GameObject.FindGameObjectWithTag("interface");
        text = GameObject.FindGameObjectWithTag("textstart").GetComponent<Text>();
        Contrrolers.SetActive(false);
        Interface.SetActive(false);
        if (view.IsMine)
            nickname.color = Color.green;
        spawner = GameObject.FindGameObjectWithTag("spawner").GetComponent<Spawner>();
        spawner.enabled = false;
    }
    private void Awake()
    {
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            playerConttroler.enabled = false;
        }
    }
    private void Moving()
    {
        moveVector = Vector3.zero;
        moveVector.x = joistickController.Horizontal() * speed;
        moveVector.z = joistickController.Vertical()* speed;
        
        if (moveVector.x != 0 || moveVector.z != 0)
        {
            animator.SetBool("movingnow", true);
        }
        else
        {
            animator.SetBool("movingnow", false);
        }
        if (Vector3.Angle(Vector3.forward, moveVector) >1f || Vector3.Angle(Vector3.forward , moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector,speed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }
        moveVector.y = gravityforce;
        characterController.Move(moveVector * Time.deltaTime);
        if (characterController.isGrounded)
        {
            animator.SetBool("jump", false);
            
            animator.SetBool("roll", true);
            Invoke("RollAnimFall", 0.01f);
        }
        if (!characterController.isGrounded)
        {
            animator.SetBool("falling", true);
           // animator.SetBool("jump", false);
        }
    }
    private void FixedUpdate()
    {
        Moving();
        Gravity();
        if (PhotonNetwork.PlayerList.Length >= ForData.playerforstart)
        {
            timerstart += Time.deltaTime;
            if (timerstart >= 1 && countdown == 0)
            {
                Interface.SetActive(true);
                text.text = "3";
                countdown++;
            }
            if (timerstart >= 2 && countdown == 1)
            {
                text.text = "2";
                countdown++;
            }
            if (timerstart >= 3 && countdown == 2)
            {
                text.text = "1";
                countdown++;
            }
            if (timerstart >= 4 && countdown == 3)
            {
                text.text = "START";
            }
            if (timerstart >= 5 && countdown == 3)
            {
                countdown++;
                Contrrolers.SetActive(true);
                Interface.SetActive(false);
                spawner.enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {

        }
    }
    public void RollAnimFall()
    {
        animator.SetBool("roll", false);
    }
    private void Gravity()
    {
        if (!characterController.isGrounded)
        {
            gravityforce -= 20f * Time.deltaTime;
        }
        else
        { 
            gravityforce = -1f; 
        }
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            gravityforce = jumppower;
            animator.SetBool("jump", true);
        }
    }
    public void Jump()
    {
        if (characterController.isGrounded)
        {
            gravityforce = jumppower;
            animator.SetBool("jump", true);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {

            var enemyrb = collision.gameObject.GetComponent<Rigidbody>();
            if (enemyrb.velocity.magnitude >= 7)
            {
                var playerpos = player.transform;
                Instantiate(raggdol, playerpos.position, Quaternion.identity);
                Destroy(raggdol);
                Destroy(gameObject);
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "finish")
        {
            Contrrolers.SetActive(false);
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(x: -1.3f, y: -62.03f, z: -89f);
            player.GetComponent<CharacterController>().enabled = true;
            spawner.enabled = false;
        }
    }
}
