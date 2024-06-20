using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject mouseTargetPrefab;

    private GameObject mouseTarget;
    private Vector3 rotationTarget;
    private Rigidbody rb;
    private float footstepThreshold = 0.36f;
    private float footstepTimer = 0f;

    private void Awake()
    {
        mouseTarget = Instantiate(mouseTargetPrefab);
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameManager.OnGameRestarted += GameManager_OnGameRestarted;
    }

    private void Update()
    {
        HandleMovement();
        HandleMousePositionOrStick();
        HandleLook();
    }

    private void HandleMovement()
    {
        Vector2 move = InputManager.Instance.GetMove();
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        rb.velocity = Vector3.zero;

        if(move != Vector2.zero)
        {
            footstepTimer += Time.deltaTime;

            if(footstepTimer >= footstepThreshold)
            {
                AudioManager.Instance.PlayFootstepSound();
                footstepTimer = 0f;
            }
        }
    }

    private void HandleMousePositionOrStick()
    {
        Vector2 look = InputManager.Instance.GetLook();
        if (InputManager.Instance.GetIsGamepad())
        {
            Vector3 direction = new Vector3(look.x, 0f, look.y);
            if(direction.magnitude > 0.1f)
            {
                rotationTarget = transform.position + direction;
                mouseTarget.transform.position = rotationTarget;
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(look);
            RaycastHit[] hitArray = Physics.RaycastAll(ray);

            foreach(RaycastHit hit in hitArray)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    rotationTarget = hit.point;
                    mouseTarget.transform.position = rotationTarget;
                }
            }
        }
    }

    private void HandleLook()
    {
        Vector3 lookPosition = rotationTarget - transform.position;
        lookPosition.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookPosition);

        if(lookPosition.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed);
        }
    }

    private void SetPlayerLocationToOrigin()
    {
        transform.position = Vector3.zero;
    }

    private void GameManager_OnGameRestarted()
    {
        SetPlayerLocationToOrigin();
    }

}
