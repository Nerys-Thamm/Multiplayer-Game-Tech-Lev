using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureDot : MonoBehaviour
{
    public CapturePoint Point;
    public Image CaptureImg;
    public Image CurrentImage;
    
   
    public enum Team
    {
        A,
        B,
        NONE,
    }

    public Team m_CurrentTeam;
    public Team m_CapturingTeam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CurrentImage.color = Point.CurrentImage.color;
        CaptureImg.color = Point.CaptureImg.color;
      
        CaptureImg.fillAmount = Point.CapturePercent;
        
    }
}
