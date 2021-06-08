using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashbang : MonoBehaviour
{
    public AnimationCurve m_OpacityCurve;
    public Image m_FlashbangImage;
    public ParticleSystem m_FlashbangParticles;
    public float m_duration;
    float m_currentduration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DoFlashbang();
        }
        Color color = Color.white;
        if (m_currentduration > 0)
        {
            m_currentduration -= Time.deltaTime;
            
            color.a = m_OpacityCurve.Evaluate(m_currentduration / m_duration);
        }
        else
        {
            color.a = 0;
            if(m_FlashbangParticles.isPlaying)
            {
                m_FlashbangParticles.Stop();
            }
        }
        m_FlashbangImage.color = color;
    }

    public void DoFlashbang()
    {
        m_currentduration = m_duration;
        m_FlashbangParticles.Play();
    }
}
