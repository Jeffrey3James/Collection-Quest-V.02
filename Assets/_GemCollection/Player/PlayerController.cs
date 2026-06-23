 using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.Cinemachine;
using System.Linq;
using System;
using JetBrains.Annotations;


public class PlayerController : MonoBehaviour
{   
    #region Componenents
    [Header("Components")]
    private Animator animator;
    private Rigidbody rb;
    private Collider col;
    private Vector3 movement;
    private Vector2 look;
    private Transform playerTransform;
    #endregion

    #region Movement Settings
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
  #endregion

  #region JumpSettings
  [SerializeField] private Transform groundChecker;
  [SerializeField] private bool isAirborne;
  [SerializeField] private bool isGrounded;
  [SerializeField] private float jumpForce = 3.5f;
  private float jumpBufferTime;
  private const bool CAN_DOUBLE_JUMP = true; // Before pushing to git set this back to false //
  private int jumpIndex = 0;  
  #endregion

  #region Camera Settings
  [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CinemachineCamera VCamera;
    [Range(0f, 1f)]
    [SerializeField] private float xSensitivity;
    //[Range(0f, 1f)]
    //[SerializeField] private float ySensitivity;
    #endregion

    #region StateMachine
    [Header("State Machine")]
    private StateMachine machine;
    private static readonly int Speed = Animator.StringToHash("Speed");
    #endregion

    #region States
    [Header("States")]
    private IState locomotionState;
    private IState jumpState;
    #endregion

    #region Private Fields
    private float currentSpeed;
    private List<Timer> timerList;
    private List<IState> states = new List<IState>();
    [SerializeField, Required] private InputReader reader;
    private float groundDistance = .1f;
    [SerializeField] private LayerMask groundLayer;  
  #endregion

    #region Properties
    public StateMachine Machine => machine;
    #endregion


    #region Life Cycle
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        playerTransform = transform;
        reader.EnablePlayerActions();
        machine = new StateMachine();

    // Prevent tipping over
    rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = true;


        Setups();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    Debug.Log(animator.runtimeAnimatorController);
    machine.SetState(locomotionState);
  }

    private void Setups()
    {
        SetupLocomotionState();
        SetupJumpState();
    }

    private void Start() {
    reader.Jump += JumpSequence;
  }

  private void JumpSequence () {
    if(!IsGrounded())
      return;
    isAirborne = true;

    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
  }
  public bool IsGrounded () {
    if(isAirborne) return false;
    isGrounded = Physics.Raycast(
     groundChecker.transform.position,
     Vector3.down,
     groundDistance,
     groundLayer);

    return isGrounded;
  }

  private void OnDisable()
    {
        reader.controls.Disable();
    reader.Jump -= JumpSequence;
    }

    private void Update() {
    movement = new Vector3(reader.Direction.x, 0, reader.Direction.y);
    Debug.Log($"Direction raw: {reader.Direction}, controls enabled: {reader.controls.Player.enabled}");
    if(isAirborne && rb.linearVelocity.y < 0) { isAirborne = false; }
    IsGrounded();
    Debug.Log($"forward: {cameraTransform.forward}, right: {cameraTransform.right}, movement: {movement}");
    Debug.Log(reader.Direction);
    // Read Input
   
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        movement = (forward * movement.z + right * movement.x).normalized;

        machine.Update();
        UpdateAnimator();

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
  }

  private void FixedUpdate () {
    machine.FixedUpdate();
    MovePlayer();
    RotatePlayer();
  }
  #endregion

    #region Helpers
  private void Any(IState to, IPredicate condition) => machine.AddAnyTransition(to, condition);

    public IState GetState(string stateName)
    {
        return states.FirstOrDefault(s => s.StateName == stateName);
    }
  #endregion

  #region States Setup


  private void SetupLocomotionState () {
    locomotionState = new LocomotionState(this, animator);

    Any(locomotionState,
        new FuncPredicate(() => IsGrounded()));

    states.Add(locomotionState);
  }

  private void SetupJumpState () {
    jumpState = new JumpState(this, animator);

    Any(jumpState,
        new FuncPredicate(() => !IsGrounded()));

    states.Add(jumpState);
  }
    #endregion

    private void RotatePlayer()
    {

        look = reader.LookDirection;

        if (look.sqrMagnitude > 0)
        {
            float yRotation = rb.rotation.eulerAngles.y + (look.x * (xSensitivity * 3f));
            Quaternion targetRotation = Quaternion.Euler(0f, yRotation, 0f);

            rb.MoveRotation(targetRotation);
        }
    }

    public void MovePlayer()
    {
        Vector3 velocity = movement * moveSpeed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
       
        //Vector3 move = movement * moveSpeed * Time.fixedDeltaTime;
        //rb.MovePosition(rb.position + move);
    }

  private void UpdateAnimator () {
    currentSpeed = movement.magnitude;
    Debug.Log($"Movement: {movement}, Speed: {currentSpeed}");
    animator.SetFloat(Speed, currentSpeed);
  }

  private void OnDrawGizmos () {
    if(groundChecker == null) return;
    Gizmos.color = IsGrounded() ? Color.green : Color.red;
    Gizmos.DrawRay(groundChecker.position, Vector3.down * 0.3f);
  }
}
