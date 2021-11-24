using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController3rdPerson : MonoBehaviour
{
    // ~ Xandy, pode mudar o código como quiser. Essa versão é só pra ter como boneco de teste. 
    private CharacterController characterController;
    private Animator animator;

    [Header("Camera related stuff")]
    [SerializeField] private Transform camTransform; // we get this value from the current active camera on the clearshot system
    
    //[SerializeField] CinemachineClearShot clearShot; // testando pra ver o que muda
    [SerializeField] CinemachineBrain brain;
    
    private Vector3 lastCamAngle; // ~ when testing, I found that maintaining the same direction for moving forward is great, but for turning around not so much.
    private float camEulerX, camEulerY, camEulerZ;

    private bool canMove = true; // make method to set this value from events

    [Header("Ground Check stuff")]
    [SerializeField] float gravity = -13.0f;
    float velocityY = 0.0f;                    // Will change if the player falls, jumps or something like that.

    private float actualWalkSpeed;  // player speed;
    private float walkSpeed = 1.6f;   // default speed for the player walking
    private float runSpeed = 3.0f;  // default speed for the player running
  
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;

    private Vector3 targetDir = Vector3.zero;
    private Vector3 currentDir = Vector3.zero;
    private Vector3 currentDirVelocity = Vector3.zero;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("States")]
    private bool isRunning, isWalking; // isWalking indicates if the player is moving.

    // local temporário pra var de soundSource
    [SerializeField] private GameObject soundSource;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        actualWalkSpeed = walkSpeed;
        canMove = true;
    }


    private void OnEnable()
    {
        // inscrição nos eventos ocorre aqui
        Invoke("SubscribeToDelegates", 1f);            // usando isso por enquanto pq por algum motivo o Enable desse script roda antes do Awake do GameManager.

        // Dialogue.playerDuringDialogueOn -= PlayerDuringDialogueOn;
        // Dialogue.playerDuringDialogueOff -= PlayerDuringDialogueOff;
        // Dialogue.playerDuringDialogueOn += PlayerDuringDialogueOn;
        // Dialogue.playerDuringDialogueOff += PlayerDuringDialogueOff;


        // cinemachineBrain.m_CameraCutEvent = UpdateActiveCam;

        /*
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        */
    }

    private void SubscribeToDelegates()     //ta sendo usado com Invoke
    {
        GameManager.instance.removePlayerControlEvent -= TurnPlayerControllerOff;
        GameManager.instance.returnPlayerControlEvent -= TurnPlayerControllerOn;
        GameManager.instance.removePlayerControlEvent += TurnPlayerControllerOff;
        GameManager.instance.returnPlayerControlEvent += TurnPlayerControllerOn;
    }

    private void OnDisable()
    {

    }

    private void UpdateActiveCam()
    {

        //camTransform = clearShot.LiveChild.VirtualCameraGameObject.transform;

        //CinemachineShake.Instance.UpdateActualCam(camTransform.GetComponent<CinemachineVirtualCamera>());
        //print(clearShot.LiveChild.VirtualCameraGameObject.name);

        camTransform = brain.ActiveVirtualCamera.VirtualCameraGameObject.transform;
        //camTransform = clearShot.LiveChild.VirtualCameraGameObject.transform;
        //if(camTransform)
        //CinemachineShake.Instance.UpdateActualCam(camTransform.GetComponent<CinemachineVirtualCamera>());
       
    }

    public bool GetCanMove()
    {
        return canMove;

    }

    private void Update()
    {
        if (canMove)
        {
            HandleInputs();
            SmoothWalkSpeed();
            UpdateMovement();
        }
        else
        {
            actualWalkSpeed = Mathf.Lerp(actualWalkSpeed, 0, Time.deltaTime * 6f);
            isRunning = false;
            currentDir = Vector2.zero;
            if (actualWalkSpeed < .1f)
            {
                this.enabled = false;
                // print("PCtrl OF");
            }
        }

        // Animation
        animator.SetBool("isWalkingX", isWalking); // arrumar depois
        animator.SetFloat("Velocity", actualWalkSpeed);

        //Debug.LogWarning(actualWalkSpeed);
    }

    private void HandleInputs()
    {
        // Inputs stuff
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0);

        isWalking = hasHorizontalInput || hasVerticalInput;

        if (Input.GetAxis("Horizontal") >= 0 || Input.GetAxis("Vertical") >= 0) { UpdateCollider(); }

        if (Input.GetButton("Run"))
        {
            isRunning = true;
        }
        else isRunning = false;

        targetDir = new Vector3(horizontal, 0, vertical).normalized;
        //Pass raw values to make a smooth transition
        currentDir = Vector3.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (!isWalking)
        {
            isRunning = false;
            UpdateActiveCam();
        }
    }

    private void UpdateCollider()
    {
        //print("Ainda não implementado");
        //if (!isCrouched)
        //{
        //    characterController.stepOffset = 0.150f;

        //    characterController.center = new Vector3(0, 0.0775f, 0);
        //    characterController.radius = 0.03f;
        //    characterController.height = 0.155f;
        //}
        //else
        //{
        //    characterController.stepOffset = 0.150f; // ~ the value dont change cause the player was becoming unable to go up stairs while crouched.
        //    characterController.center = new Vector3(0, 0.0775f, 0.02f);
        //    characterController.radius = 0.033f;
        //    characterController.height = 0.155f;
        //}
    }

    private void UpdateMovement()
    {
        //Apply gravity
        if (characterController.isGrounded) velocityY = 0.0f;
        velocityY += gravity * Time.deltaTime;

        if (isWalking)
        {
            if (currentDir.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(currentDir.x, currentDir.z) * Mathf.Rad2Deg + lastCamAngle.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                Vector3 velocity = (moveDir * actualWalkSpeed) + Vector3.up * velocityY;
               
                characterController.Move(velocity * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        camEulerX = camTransform.localEulerAngles.x;
        camEulerY = camTransform.localEulerAngles.y;
        camEulerZ = camTransform.localEulerAngles.z;

        if (!isWalking) // player stopped moving
        {
            lastCamAngle = new Vector3(camEulerX, camEulerY, camEulerZ);
        }
    }

    private void SmoothWalkSpeed() // make velocity changes in a smooth way, getting the smooth currentDir
    {
        if (!isWalking)
        {
            actualWalkSpeed = Mathf.Lerp(actualWalkSpeed, 0, Time.deltaTime * 10f);
            return;
        }
        else
        {
            if (!isRunning)
            {
               // if (currentDir.y > -0.0001f && currentDir.y < 0.0001f) //From any state to -> To idle stand state
                   // actualWalkSpeed = Mathf.Lerp(actualWalkSpeed, 0, Time.deltaTime * 10f);

                 if (currentDir.y > 0f) //From any state to -> To walk forward stand state
                    actualWalkSpeed = Mathf.Lerp(actualWalkSpeed, walkSpeed, Time.deltaTime * 10f);

                actualWalkSpeed = Mathf.Lerp(actualWalkSpeed, walkSpeed, Time.deltaTime * 50f);
            }
            else
            {
                if (currentDir.y > 0f) // If the player is standing and running
                    actualWalkSpeed = Mathf.Lerp(actualWalkSpeed, runSpeed, Time.deltaTime * 2f);

                actualWalkSpeed = Mathf.Lerp(actualWalkSpeed, runSpeed, Time.deltaTime * 2f);
            }
        } 
    }

    private void TurnPlayerControllerOn()
    {
        canMove = true;
        this.enabled = true;
        print("Player controller on");
    }

    private void TurnPlayerControllerOff()
    {
        canMove = false;
        this.enabled = false;
        print("PLayer controller off");
    }

    public void ActivateSoundSource()
    {
        //if(isRunning)
        //soundSource.SetActive(true);
    }
}
