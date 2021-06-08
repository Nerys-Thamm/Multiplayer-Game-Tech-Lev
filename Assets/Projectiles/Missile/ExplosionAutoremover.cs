using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAutoremover : MonoBehaviour
{

    public float m_RemoveTime;

    // Start is called before the first frame update
    void Start()
    {
        
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
