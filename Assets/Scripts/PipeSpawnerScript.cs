using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Pipe;
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 10;
    public Rigidbody2D Player;

    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.bodyType != RigidbodyType2D.Static)
        {
            if (timer <= spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                spawnPipe();
                timer = 0;
            }
        }
        
  
    }

    public void spawnPipe()
    {
        float lowestpoint = transform.position.y - heightOffset;
        float highestpoint = transform.position.y + heightOffset;
        Instantiate(Pipe, new Vector3 (transform.position.x, Random.Range(lowestpoint, highestpoint)), transform.rotation);
    }
}
