using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashbangCloud : MonoBehaviour
{
    public float m_Lifetime;
    public float m_Speed;
    public float m_Range;
    public ParticleSystem m_particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += this.transform.forward * Time.deltaTime * m_Speed;
        m_Lifetime -= Time.deltaTime;
        if(m_Lifetime <= 1 && m_particles.isPlaying)
        {
            m_particles.Stop();
        }
        if(m_Lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
        RaycastHit[] hits = Physics.SphereCastAll(this.gameObject.transform.position, m_Range, Vector3.up);
        foreach (var hit in hits)
        {
            if(hit.collider.gameObject.CompareTag("Missile"))
            {
                hit.collider.gameObject.GetComponent<Flashbang>().DoFlashbang();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Color color = Color.white;
        color.a = 0.2f;
        Gizmos.color = color;
        Gizmos.DrawSphere(this.gameObject.transform.position, m_Range);
    }
}
