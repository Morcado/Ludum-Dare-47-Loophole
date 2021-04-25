using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // Modificable stuff for the player
    [SerializeField] private float moveSpeed = 700f;
    [SerializeField] private float jumpHeight = 8f;
    [SerializeField] private bool grounded = true;
    [SerializeField] private AudioSource jumpSFX;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    public static int seconds;
    private float timer = 0.0f;
    private bool[] pickedItems;

    // private SpriteRenderer spriteRend;
    // private Animator animator;
    private Rigidbody player = null;
    private Collider coll;
    // [SerializeField] private LayerMask Foreground;

    private enum State {idle, run, jump, fall, melee, special}; // States of the player
    private State state = State.idle;
    // private bool facingLeft = true;

    // Start is called before the first frame update
    void Start() {
        pickedItems = new bool[3];
        pickedItems[0] = false;
        PlayerPrefs.DeleteAll();
        pickedItems[1] = false;
        pickedItems[2] = false;
        // Get components of the player to later modify them.
        // spriteRend = GetComponent<SpriteRenderer>();
        // animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        timeText.text = timer.ToString("#.##");
        Jump();
        VelocityState();
        Position();
        if (pickedItems[0] && pickedItems[1] && pickedItems[2]) {
            PlayerPrefs.SetFloat("score", timer);
            SceneManager.LoadScene("Menu");
        }
    }

    public void FixedUpdate() {
        Move();
    }

    public void Position() {
        if (player.position.y <= 0) {
            transform.position = new Vector3(player.position.x, 217, player.position.z);
        } else if (player.position.x > 241) {
            transform.position = new Vector3(-177, player.position.y, player.position.z);
        } else if (player.position.x < -177) {
            transform.position = new Vector3(241, player.position.y, player.position.z);
        }
    }

    // Moving the player
    public void Move() {
        // Gets the left or right action pressed
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        float xForce = xInput * moveSpeed * Time.deltaTime;

        Vector2 force = new Vector2(xForce, 0f);
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Horizontal"));
        // Changes player position
        //transform.position += movement * Time.deltaTime * moveSpeed;
        player.AddForce(force);

        // Flips the player sprite if it's moving to the left or right;
        // if (horizontalInput < 0) {
        //     facingLeft = true;
        //     spriteRend.flipX = true;
        // } else if (horizontalInput > 0) {
        //     facingLeft = false;
        //     spriteRend.flipX = false;
        // }

    }

    // /* This controls the state of the player*/
    private void VelocityState() {
        /* If player it's jumping then it changes to the falling state when
        the velocity of player reaches 0 (highest point)*/
        if (state == State.jump) {
            if (player.velocity.y < 0.1f) {
                state = State.fall;
            }
        }
        
        /* Changes the state of falling to idle if it touches the layer foreground (the floor)*/
        else if (state == State.fall) {

        //     if (collider.(Foreground)) {
        //         state = State.idle;
        //     }
        }
        /* If player velocity changes more than epsilon, it's sprite changes to running animation.
        This is ignored when the player it's jumping or falling*/
        else if (Mathf.Abs(player.velocity.x) > Mathf.Epsilon) {
            state = State.run;
        } else {
            state = State.idle;
        }
        // Debug.Log("state:" + (int)state);
    }

    /* This is called every time to check if the player is jumping. it checks on the jump button.*/
    public void Jump() {       
        if (Input.GetButtonDown("Jump") && grounded){
            player.AddForce(new Vector3(0f, jumpHeight, 0f), ForceMode.Impulse); 
            //player.velocity = new Vector3(player.velocity.x, jumpHeight, 0f);
            state = State.jump;
            jumpSFX.Play();
            grounded = false;
        }

    }

    public void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.layer == 8 && !grounded){
            grounded = true;
        }
        if (collision.gameObject.tag == "Chip") {
            pickedItems[0] = true;
        }
        if (collision.gameObject.tag == "Box") {
            pickedItems[1] = true;
        }
        if (collision.gameObject.tag == "Button") {
            pickedItems[2] = true;
        }

     }

    public void OnCollisionExit(Collision collision) {
        // Debug.Log("exit colission");
        // if (collision.gameObject.layer == 8 && grounded) {
        //     grounded = false;
        // }
    }

    public void OncollisionTrigger(Collision collision) {
        Debug.Log(collision.collider.tag);
    }
}