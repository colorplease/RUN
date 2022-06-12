using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;
    public float walkSpeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    public bool isSprinting;
    bool canMove = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode mapKey = KeyCode.R;
    public KeyCode weaponKey = KeyCode.Mouse0;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    [SerializeField]bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public Rigidbody rb;

    [Header("Blind")]
    public float blindSpeed;
    public PixelatedCamera pixelatedCamera;
    public float bright;

    [Header("Shake")]
    [SerializeField]CameraShake cameraShake;
    [SerializeField]Transform enemy;

    [Header("Map")]
    [SerializeField]GameObject map;
    [SerializeField]GameObject mapOpenAnimation;
    [SerializeField]GameObject mapOpenText;
    [SerializeField]Camera mapCamera;
    public bool mapOpen;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        RenderSettings.ambientIntensity = bright;
    }

    private void Update()
    {

        MyInput();
        SpeedControl();
        Blind();
        ShakeControl();
        CheckIfPlayerScrewed();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    void ShakeControl()
    {
        float dist = Vector3.Distance(transform.position, enemy.position);
        cameraShake.power = 1/dist * 0.1f;
    }

    void CheckIfPlayerScrewed()
    {
        if (enemy.gameObject.GetComponentInChildren<CreatureAIRedux>().chase)
        {
            MapClose();
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void Blind()
    {
        if (isSprinting)
        {
            pixelatedCamera.screenScaleFactor += blindSpeed * Time.fixedDeltaTime;
        }
        else
        {
            
            if (pixelatedCamera.screenScaleFactor > 5f)
            {
                pixelatedCamera.screenScaleFactor -= Time.fixedDeltaTime * blindSpeed * 2;
            }

        }
    }

    

    private void MyInput()
    {
        if (canMove)
        {
             horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKey(sprintKey))
        {
            isSprinting = true;
            moveSpeed = sprintSpeed;
        }
        else
        {
            isSprinting = false;
            moveSpeed = walkSpeed;
        }
        }

        if (Input.GetKeyDown(mapKey))
        {
            print("hey");
            if (mapOpen)
            {
                MapClose();
            }
            else
            {
                MapOpen();
            }
        }
    }

    void MapOpen()
    {
        StartCoroutine(mapOpenAnim());
    }

    public void MapClose()
    {
        canMove = true;
        mapOpen = false;
        map.SetActive(false);
        mapOpenAnimation.SetActive(false);
        mapCamera.gameObject.SetActive(false);
        StopAllCoroutines();
    }

    IEnumerator mapOpenAnim()
    {
        canMove = false;
        mapOpen = true;
        mapOpenAnimation.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        mapCamera.gameObject.SetActive(true);
        map.SetActive(true);
    }

    private void MovePlayer()
    {
        if (canMove)
        {
             // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Creature")
        {
            other.gameObject.GetComponent<CreatureAIRedux>().chase = true;
        }
    }
}