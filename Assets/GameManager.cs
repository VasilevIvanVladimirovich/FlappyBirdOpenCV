using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startGameCanvas;
    public GameObject overGameCanvas;
    public GameObject getReadyCanvas;
    public GameObject scoreCanvas;
    public GameObject fon;
    public GameObject cameraPlane;
    public BirdScrypt bird;


    public Score score;
    public CameraDetectTracking cameraDetectTracking;
    public PipeSpawner pipespawner;
   
    void Start()
    {
        Score.score = 0;
        scoreCanvas.SetActive(false);
        startGameCanvas.SetActive(true);
        fon.SetActive(true);
    }

    public void pressButtonStartCalibFaceDetector()
    {
        getReadyCanvas.SetActive(true);
        overGameCanvas.SetActive(false);
        startGameCanvas.SetActive(false);
        fon.SetActive(false);
        cameraPlane.SetActive(true);
        cameraDetectTracking.startVideo();
    }

    public void pressbuttonTrackingAndGoGame()
    {
        getReadyCanvas.SetActive(false);
        cameraDetectTracking.tracking();
        bird.birdInit();
        pipespawner.init();
        Time.timeScale = 1;
        //fon.SetActive(true);
        scoreCanvas.SetActive(true);
        //cameraPlane.SetActive(false);
    }

    public void gameOver()
    {
        Time.timeScale = 0;
        cameraDetectTracking.DropTracking();
        overGameCanvas.SetActive(true);
    }

    public void replay()
    {
        SceneManager.LoadScene(0);
        cameraDetectTracking.del();
        Start();
    }


}
