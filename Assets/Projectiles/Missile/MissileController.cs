using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : NetworkBehaviour
{
    [Header("References")]
    [Tooltip("Object representing the player look direction")] public Transform m_LookDirectionObj;
    [Header("Missile Options")]
    [Tooltip("The amount of fuel, in seconds, that the projectile has.")] public float m_FuelAmount;
    [Tooltip("The amount of force applied by the forward Thruster")] public float m_Thrust;
    [Header("Controls")]
    [Tooltip("Pitch sensitivity")] public float m_Pitch;
    [Tooltip("Roll sensitivity")] public float m_Roll;
    [Tooltip("Yaw sensitivity")] public float m_Yaw;

    Rigidbody m_rigidbody;
    public GameObject m_CameraObj;


    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (m_FuelAmount > 0)
        {
            m_FuelAmount -= Time.deltaTime;
            m_rigidbody.AddForce(m_LookDirectionObj.forward * m_Thrust, ForceMode.Force);
        }
        else
        {
            m_rigidbody.useGravity = true;
            m_rigidbody.AddForce(m_LookDirectionObj.forward * (m_Thrust/2), ForceMode.Force);
        }
        if (!isLocalPlayer) return;
        float xinput = Input.GetAxisRaw("Mouse X");
        float yinput = Input.GetAxisRaw("Mouse Y");

        this.gameObject.transform.Rotate(new Vector3(yinput * m_Pitch * Time.deltaTime, xinput * m_Yaw * Time.deltaTime, 0));

        //---------------------
        // ROLL INPUT
        //---------------------
        if (Input.GetKey(KeyCode.Q))
        {
            this.gameObject.transform.Rotate(new Vector3(0, 0, m_Roll * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.E))
        {
            this.gameObject.transform.Rotate(new Vector3(0, 0, -m_Roll * Time.deltaTime));
        }

        //---------------------
        // YAW INPUT
        //---------------------
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Rotate(new Vector3(0, -m_Yaw * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Rotate(new Vector3(0, m_Yaw * Time.deltaTime, 0));
        }
        this.gameObject.transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * m_Yaw * Time.deltaTime, 0));

        //---------------------
        // Pitch INPUT
        //---------------------
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.Rotate(new Vector3(-m_Pitch * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.Rotate(new Vector3(m_Pitch * Time.deltaTime, 0, 0));
        }
        this.gameObject.transform.Rotate(new Vector3(Input.GetAxisRaw("Mouse Y") * -m_Pitch * Time.deltaTime, 0, 0));
    }
}
