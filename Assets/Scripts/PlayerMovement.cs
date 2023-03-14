using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rb2d;
    public float jumpVelocity;
    public float defaultRotation;
    public float jumpRotation;
    public float rotationBoundary;
    public LogicScript logic;
    public bool isAlive = true;
    private State state;
    public PipeSpawnerScript pipeSpawnerScript;

    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }

    void Start()
    {
        state = State.WaitingToStart;
        rb2d.bodyType = RigidbodyType2D.Static;

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        
    }

    // Update is called once per frame  
    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space) == true)
                {
                    rb2d.bodyType = RigidbodyType2D.Dynamic;
                    jumpMechanic();
                    pipeSpawnerScript.spawnPipe();
                    state = State.Playing;
                    logic.destroyPressSpace();
                    
                }                
            
                
                break;
            case State.Playing:
                jumpMechanic();
                break;
        }

        if ((transform.position.y  < -32f || transform.position.y > 32f) && isAlive == true)
        {
            logic.gameOver();
            isAlive = false;
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pipe" && isAlive == true)
        {
            logic.gameOver();
            isAlive = false;
            state = State.Dead;
        }
    }

    public void jumpMechanic()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && isAlive == true)
        {
            rb2d.velocity = Vector2.up * jumpVelocity * Time.fixedDeltaTime;
            transform.Rotate(new Vector3(0, 0, jumpRotation));
            FindAnyObjectByType<SoundManager>().Play("Jump");
            
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -1 * defaultRotation));
        }

        if (transform.rotation.z >= rotationBoundary || transform.rotation.z <= -rotationBoundary)
        {
            transform.rotation = new Quaternion(0, 0, Mathf.Clamp(transform.rotation.z, -rotationBoundary, rotationBoundary), transform.rotation.w);
        }
    }
}
