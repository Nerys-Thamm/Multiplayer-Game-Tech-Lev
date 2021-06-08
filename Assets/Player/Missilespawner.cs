using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missilespawner : MonoBehaviour
{

    public GameObject m_MissilePrefab;
    public GameObject m_FlashbangPrefab;
    public UIbar m_bar;
    public bool m_MissileExists = false;
    public float m_MaxMana;
    public float m_MissileManaCost;
    public float m_FlashbangManaCost;
    float m_CurrMana;
    GameObject m_Missile;
    // Start is called before the first frame update
    void Start()
    {
        m_CurrMana = m_MaxMana;
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrMana += Time.deltaTime;
        if(m_CurrMana > m_MaxMana)
        {
            m_CurrMana = m_MaxMana;
        }
        else if(m_CurrMana < 0)
        {
            m_CurrMana = 0;
        }
        m_bar.m_FillPercent = m_CurrMana/m_MaxMana;
        if(m_Missile)
        {
            m_MissileExists = true;
        }
        else
        {
            m_MissileExists = false;
        }
        if(Input.GetMouseButtonDown(0) && !m_MissileExists && m_CurrMana - m_MissileManaCost >= 0)
        {
            if (!Physics.Raycast(this.transform.position, this.transform.forward, 10))
            {
                m_Missile = Instantiate(m_MissilePrefab, this.transform.position + (this.transform.forward * 10), this.transform.rotation);
                m_CurrMana -= m_MissileManaCost;
            }
        }
        if (Input.GetMouseButtonDown(1) && !m_MissileExists && m_CurrMana - m_FlashbangManaCost >= 0)
        {
            if (!Physics.Raycast(this.transform.position, this.transform.forward, 10))
            {
                Instantiate(m_FlashbangPrefab, this.transform.position + (this.transform.forward * 10), this.transform.rotation);
                m_CurrMana -= m_FlashbangManaCost;
            }
        }
    }
}
