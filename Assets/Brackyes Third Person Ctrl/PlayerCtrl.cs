using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public CharacterController characterController;
    Camera camera;

    public float moveSpeed = 6f;
    public float turnSpeed = 0.1f;
    float currentVelocity;

    float H;
    float V;

    Animator anim;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        Movement();
        UpdateAnimation();
    }

    private void Movement()
    {
        H = Input.GetAxisRaw("Horizontal");
        V = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(H, 0, V).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, turnSpeed);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            characterController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void UpdateAnimation()
    {
        float animMove = Mathf.Clamp01(Mathf.Abs(H) + Mathf.Abs(V));
        anim.SetFloat("Vertical", animMove);
    }
}
