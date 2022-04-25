using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float maxTime = 1;
    private float timer = 0;
    public GameObject pipe;
    public float height;

    public bool isSpawning = false;
    // Start is called before the first frame update
    public void init()
    {
        GameObject newpipe = Instantiate(pipe);
        newpipe.transform.position = transform.position + new Vector3(0,Random.Range(-height,height),0);
        Destroy(newpipe,15);
        isSpawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawning)
        {
            if(timer > maxTime)
            {
                GameObject newpipe = Instantiate(pipe);
                newpipe.transform.position = transform.position + new Vector3(0,Random.Range(-height,height),0);
                Destroy(newpipe,15);
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }
}
