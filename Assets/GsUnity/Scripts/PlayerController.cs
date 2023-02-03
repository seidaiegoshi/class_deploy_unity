using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f; // 移動スピード設定用の変数を用意
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] Transform foot;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip jumpSe;

    PlayerInput input; // アタッチしたPlayer Inputコンポーネントを格納する変数
    InputAction moveInput; // 作成したGameActionsのMoveに関するActionsを格納する変数
    InputAction jumpInput; // 作成したGameActionsのMoveに関するActionsを格納する変数
    InputAction RaiseHandRInput;

    //CharacterController characterController;
    Rigidbody rb;
    Vector3 moveDirection;
    float distanceToGround;
    bool isGrounded;

    Camera cam;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //Vector3 pos = transform.position; // 現在のpositionを取得
        //pos.x = 1; // 取得した値のxに1fを代入
        //transform.position = pos; // positionに戻す
        input = GetComponent < PlayerInput>(); // アタッチしたPlayerInputコンポーネントを取得して変数へ格納
        moveInput = input.actions["Move"]; // GameActionsのMoveに関するActionsを格納
        jumpInput = input.actions["Jump"];
        RaiseHandRInput = input.actions["RaiseHandR"];
        //characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        distanceToGround = transform.position.y - foot.position.y + 0.1f;

        cam = Camera.main;

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckGrounded();
        animator.SetBool("isGrounded", isGrounded);
        Vector2 moveInputValue = moveInput.ReadValue<Vector2>(); // ReadValueメソッドで入力値を取得

        Vector3 camForward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
        Vector3 verticalValue = moveInputValue.y * camForward;
        Vector3 holizontalValue = moveInputValue.x * cam.transform.right;
        moveDirection = verticalValue + holizontalValue;

        animator.SetFloat("Speed", moveDirection.magnitude);
        //moveDirection = new Vector3(moveInputValue.x, 0, moveInputValue.y);
        //Debug.Log(moveInputValue); // どんな値が取得できるか確認

        //characterController.Move(Time.deltaTime * moveSpeed * new Vector3(moveInputValue.x, 0, moveInputValue.y));

        //Vector3 pos = transform.position;
        //pos.x += moveInputValue.x * Time.deltaTime * moveSpeed;
        //pos.z += moveInputValue.y * Time.deltaTime * moveSpeed;
        //transform.position = pos;

        if (moveDirection != Vector3.zero) {
            Rotate();
        }


        
        if (isGrounded && jumpInput.WasPressedThisFrame())
        {
            Jump();
        }

        if (RaiseHandRInput.WasPressedThisFrame())
        {
            RaiseHandR();
        }

    }

    void FixedUpdate()
    {
        Move();

    }

    // 移動
    void Move()
    {
        float currentSpeed = rb.velocity.magnitude;
        if (currentSpeed > maxSpeed) return;

        rb.AddForce(moveDirection * moveSpeed, ForceMode.VelocityChange);
    }

    // 回転
    void Rotate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // ジャンプ
    void Jump()
    {
        animator.SetTrigger("Jump");
        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
        audioSource.PlayOneShot(jumpSe);
    }

    // 右手を上げる
    void RaiseHandR()
    {
        animator.SetTrigger("RaiseHandR");
    }


    bool CheckGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }
}
