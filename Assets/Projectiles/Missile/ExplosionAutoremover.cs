using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAutoremover : NetworkBehaviour
{

    public float m_RemoveTime;
    public GameObject m_CameraObj;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            m_CameraObj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_RemoveTime -= Time.deltaTime;
        if(m_RemoveTime < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
