using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missilespawner : NetworkBehaviour
{

    public GameObject m_MissilePrefab;
    public GameObject m_FlashbangPrefab;
    public UIbar m_bar;
    public bool m_MissileExists = false;
    public float m_MaxMana;
    public float m_MissileManaCost;
    public float m_FlashbangManaCost;
    public Transform m_ProjectileSpawnTransform;
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
        if (!isLocalPlayer) return;
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
            MissileInput();
            
        }
        else
        {
            m_MissileExists = false;
        }
        if(Input.GetMouseButtonDown(0) && !m_MissileExists && m_CurrMana - m_MissileManaCost >= 0)
        {
            if (!Physics.Raycast(m_ProjectileSpawnTransform.position, m_ProjectileSpawnTransform.forward, 2))
            {
                CmdSpawnMissile();
                m_CurrMana -= m_MissileManaCost;
            }
        }
        if (Input.GetMouseButtonDown(1) && !m_MissileExists && m_CurrMana - m_FlashbangManaCost >= 0)
        {
            if (!Physics.Raycast(m_ProjectileSpawnTransform.position, m_ProjectileSpawnTransform.forward, 10))
            {
                CmdSpawnFlashbang();
                m_CurrMana -= m_FlashbangManaCost;
            }
        }
    }

    void MissileInput()
    {
        ////---------------------
        //// ROLL INPUT
        ////---------------------
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    m_Missile.transform.Rotate(new Vector3(0, 0, m_Missile.GetComponent<MissileController>().m_Roll * Time.deltaTime));
        //}
        //if (Input.GetKey(KeyCode.E))
        //{
        //    m_Missile.transform.Rotate(new Vector3(0, 0, -m_Missile.GetComponent<MissileController>().m_Roll * Time.deltaTime));
        //}

        ////---------------------
        //// YAW INPUT
        ////---------------------
        //if (Input.GetKey(KeyCode.A))
        //{
        //    m_Missile.transform.Rotate(new Vector3(0, -m_Missile.GetComponent<MissileController>().m_Yaw * Time.deltaTime, 0));
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    m_Missile.transform.Rotate(new Vector3(0, m_Missile.GetComponent<MissileController>().m_Yaw * Time.deltaTime, 0));
        //}
        //m_Missile.transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * m_Missile.GetComponent<MissileController>().m_Yaw * Time.deltaTime, 0));

        ////---------------------
        //// Pitch INPUT
        ////---------------------
        //if (Input.GetKey(KeyCode.W))
        //{
        //    m_Missile.transform.Rotate(new Vector3(-m_Missile.GetComponent<MissileController>().m_Pitch * Time.deltaTime, 0, 0));
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    m_Missile.transform.Rotate(new Vector3(m_Missile.GetComponent<MissileController>().m_Pitch * Time.deltaTime, 0, 0));
        //}
        //m_Missile.transform.Rotate(new Vector3(Input.GetAxisRaw("Mouse Y") * -m_Missile.GetComponent<MissileController>().m_Pitch * Time.deltaTime, 0, 0));
    }


    [Command]
    void CmdSpawnMissile()
    {

        //RpcSpawnMissile();
        m_Missile = Instantiate(m_MissilePrefab, m_ProjectileSpawnTransform.position + (m_ProjectileSpawnTransform.forward * 10), m_ProjectileSpawnTransform.rotation);
        NetworkServer.Spawn(m_Missile);
    }

    [Command]
    void CmdSpawnFlashbang()
    {

        RpcSpawnFlashbang();
    }

    [ClientRpc]
    void RpcSpawnMissile()
    {
        m_Missile = Instantiate(m_MissilePrefab, m_ProjectileSpawnTransform.position + (m_ProjectileSpawnTransform.forward * 10), m_ProjectileSpawnTransform.rotation);
    }

    [ClientRpc]
    void RpcSpawnFlashbang()
    {
        GameObject flashbang = Instantiate(m_FlashbangPrefab, m_ProjectileSpawnTransform.position + (m_ProjectileSpawnTransform.forward * 10), m_ProjectileSpawnTransform.rotation);
    }


}
