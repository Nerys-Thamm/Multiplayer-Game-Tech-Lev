using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{



    public enum PlayerState
    {
        ALIVE,
        DEAD,
    }

    public CapturePoint.Team m_Team;


    public UnityEvent OnDeath;
    public UIbar m_HealthBar;
    public Image m_DamageOverlay;
    public float m_MaxHealth;
    public PlayerState m_State;
    float m_CurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        m_State = PlayerState.ALIVE;
    }

    // Update is called once per frame
    void Update()
    {
        m_HealthBar.m_FillPercent = m_CurrentHealth / m_MaxHealth;
        Color color = Color.white;
        if ((m_CurrentHealth / m_MaxHealth) < 0.2)
        { 
            color.a = ((Mathf.Sin(Time.time*4) + 1) / 2);
        }
        else
        {
            color.a = 0;
        }
        m_DamageOverlay.color = color;
    }

    public void Damage(float _dmg)
    {
        m_CurrentHealth -= _dmg;
        if(m_CurrentHealth <= 0)
        {
            m_State = PlayerState.DEAD;
            m_CurrentHealth = 0;
            OnDeath.Invoke();
        }
    }
}
