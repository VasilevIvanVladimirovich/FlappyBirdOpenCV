using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;
using OpenCvSharp.Tracking;

public class CameraDetectTracking : MonoBehaviour
{
    // Start is called before the first frame update
    WebCamTexture _webCamTexture;
    CascadeClassifier cascade;
    OpenCvSharp.Rect MyFace;
    Tracker tracker = null;

    Mat frame; 
    Rect2d obj;
    Texture2D newtexture = null;

    public float faceY;
    public bool isFaceDetector = false;
    public bool isTracking = false;
    public bool istart = false;
    

    public void startVideo()
    {
        WebCamDevice[] device = WebCamTexture.devices;
        _webCamTexture = new WebCamTexture(device[0].name);
        _webCamTexture.Play();
        cascade = new CascadeClassifier(Application.dataPath +"/" + @"haarcascade_frontalface_default.XML");
        istart = true;
        isFaceDetector = true;
    }

    
    public void tracking()
    {
        isTracking = true;
        isFaceDetector = false;
    }

    public void del()
    {
        _webCamTexture.Stop();
        isFaceDetector = false;
        isTracking = false;
        istart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(istart)
        {
            frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

            if(isFaceDetector)
                findNewFace(frame);

            if(isTracking)
            {
                if(null == tracker)
                {
                    obj = Rect2d.Empty;
                    obj = new Rect2d(MyFace.X, MyFace.Y, MyFace.Width, MyFace.Height);
                    tracker = Tracker.Create(TrackerTypes.MedianFlow);
                    tracker.Init(frame, obj);
                }else if (!tracker.Update(frame, ref obj))
                {
                    obj = Rect2d.Empty;
                }
                if (0 != obj.Width && 0 != obj.Height)
					MyFace = new OpenCvSharp.Rect((int)obj.X, (int)obj.Y, (int)obj.Width, (int)obj.Height);
            }
            display(frame);
        }
    }

    public void DropTracking()
	{
		if (null != tracker)
		{
			tracker.Dispose();
			tracker = null;
		}
	}

    void findNewFace(Mat frame)
     {
        var faces = cascade.DetectMultiScale(frame,1.1,2,HaarDetectionType.ScaleImage);

        if(faces.Length >= 1)
        {
           MyFace = faces[0];
           faceY = faces[0].Y;
        }
    }

    void display(Mat frame)
    {
        if(MyFace != null && isFaceDetector)
        {
            frame.Rectangle(MyFace,new Scalar(250,0,0),2);
        }
        if(MyFace != null && isTracking)
        {
            faceY = MyFace.Y;
            frame.Rectangle(MyFace,new Scalar(0,0,250),2);
        }
        newtexture = OpenCvSharp.Unity.MatToTexture(frame,newtexture);
        GetComponent<Renderer>().material.mainTexture = newtexture;
    }
}
