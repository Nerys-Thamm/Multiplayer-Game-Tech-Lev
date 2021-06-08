using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIbar : MonoBehaviour
{
    public Image Bar;
    [Range(0.0f, 1.0f)]
    public float m_FillPercent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Bar.fillAmount = m_FillPercent;
        
    }
}
