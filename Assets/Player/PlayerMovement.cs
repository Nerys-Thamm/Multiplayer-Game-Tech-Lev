using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController m_Controller;
    public Transform m_Camera;
    public GameObject m_CameraObj;
    public Missilespawner m_spawner;
    public float m_MoveSpeed;
    public float m_LookSensitivity;
    public float m_JumpForce;
    public float m_GravityForce;
    public float m_JumpDuration;
    public AnimationCurve m_JumpCurve;
    public AnimationCurve m_GravityCurve;
    float m_CurrentJumpDuration = 0;

    float m_rotX = 0;
    float m_rotY = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        if(!isLocalPlayer)
        {
            m_CameraObj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        m_CurrentJumpDuration -= Time.deltaTime;

        //this.gameObject.transform.Rotate(new Vector3(yinput * m_Ysensitivity * Time.deltaTime, xinput * m_Xsensitivity * Time.deltaTime, 0));

        //---------------------
        // KEY INPUT
        //---------------------
        
        if(Input.GetKeyDown(KeyCode.Space) && m_Controller.isGrounded)
        {
            m_CurrentJumpDuration = m_JumpDuration;
        }
        
        Vector3 movementvector = new Vector3((-(Input.GetKey(KeyCode.A) ? 1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0)), 0, ((Input.GetKey(KeyCode.W) ? 1 : 0) + -(Input.GetKey(KeyCode.S) ? 1 : 0)));
        movementvector.Normalize();
        

        //---------------------
        // MOUSE INPUT
        //---------------------
        float xinput = Input.GetAxisRaw("Mouse X");
        float yinput = Input.GetAxisRaw("Mouse Y");

        //---------------------
        // APPLY GRAVITY
        //---------------------
        
        movementvector.y = (m_GravityCurve.Evaluate(m_CurrentJumpDuration/m_JumpDuration)*-m_GravityForce) + (m_JumpCurve.Evaluate(m_CurrentJumpDuration / m_JumpDuration)*m_JumpForce);
        

        //---------------------
        // APPLY MOVEMENT
        //---------------------
        if (!m_spawner.m_MissileExists)
        {
            m_Controller.Move(transform.rotation * movementvector * m_MoveSpeed * Time.deltaTime);

            m_rotX += xinput * m_LookSensitivity;
            m_rotY += yinput * m_LookSensitivity;
            m_rotY = Mathf.Clamp(m_rotY, -90f, 90f);
            m_Camera.localRotation = Quaternion.Euler(-m_rotY, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, m_rotX, 0f);
        }
    }
}
