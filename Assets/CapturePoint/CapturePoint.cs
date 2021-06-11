using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapturePoint : MonoBehaviour
{
    public Image CaptureImg;
    public Image CurrentImage;
    public Color ColorA;
    public Color ColorB;
    public Color ColorNone;
    [Range(0,1)]
    public float CapturePercent;
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
        switch (m_CurrentTeam)
        {
            case Team.A:
                CurrentImage.color = ColorA;
                CaptureImg.color = ColorB;
                break;
            case Team.B:
                CurrentImage.color = ColorB;
                CaptureImg.color = ColorA;
                break;
            case Team.NONE:
                switch (m_CapturingTeam)
                {
                    case Team.A:
                        CurrentImage.color = ColorNone;
                        CaptureImg.color = ColorA;
                        break;
                    case Team.B:
                        CurrentImage.color = ColorNone;
                        CaptureImg.color = ColorB;
                        break;
                    default:
                        CurrentImage.color = ColorNone;
                        CaptureImg.color = ColorNone;
                        break;
                }
                break;
            default:
                break;
        }
        CaptureImg.fillAmount = CapturePercent;
        if (CapturePercent >= 1)
        {
            m_CurrentTeam = m_CapturingTeam;
            m_CapturingTeam = Team.NONE;
            CapturePercent = 0;
        }
        CapturePercent -= Time.deltaTime / 40;
        if (CapturePercent < 0)
        {
            CapturePercent = 0;
        }
        if (CapturePercent > 1)
        {
            CapturePercent = 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Player>().m_Team == m_CapturingTeam)
            {
                CapturePercent += Time.deltaTime / 20;
            }
            else if (other.gameObject.GetComponent<Player>().m_Team == m_CurrentTeam)
            {
                CapturePercent -= Time.deltaTime / 30;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Player>().m_Team != m_CurrentTeam && CapturePercent == 0)
            {
                m_CapturingTeam = other.gameObject.GetComponent<Player>().m_Team;
            }
        }
    }
}
