using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScrypt : MonoBehaviour
{
    public GameManager gameManager;
    public CameraDetectTracking faceDetector;

    public float velocity = 1;
    float lastY = 0;
    float speed = 10f;
    public bool isfly;

    // Start is called before the first frame update
    public void birdInit()
    {
        faceDetector = (CameraDetectTracking)FindObjectOfType(typeof(CameraDetectTracking));
        isfly = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isfly)
        {
            float step = speed * Time.deltaTime;
            float norm = Mathf.Clamp(faceDetector.faceY - lastY, -10, 10);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,transform.position.y - norm ,transform.position.z),step);
            lastY = faceDetector.faceY;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.gameOver();
    }

}
