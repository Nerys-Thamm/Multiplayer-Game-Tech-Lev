using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExplosion : MonoBehaviour
{
    public GameObject m_ExplosionPrefab;
    [Tooltip("The radius of the explosion created")] public float m_Range;
    [Tooltip("The damage of the explosion created")] public float m_Damage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Instantiate(m_ExplosionPrefab, this.gameObject.transform.position, this.transform.rotation);
        RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, m_Range, Vector3.up);
        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                float dmgmultiplier = (this.transform.position - hit.collider.gameObject.transform.position).magnitude / m_Range;
                hit.collider.gameObject.GetComponent<Player>().Damage(m_Damage - (m_Damage * Mathf.Clamp( dmgmultiplier, 0, 1)));
            }
        }
        GameObject.Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, m_Range);
    }
}
