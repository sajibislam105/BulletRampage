using System;
using OpenCover.Framework.Model;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action<float,Vector3> BulletGenerateAction;
    public Action PlayerDeadAction;
    public Action<float> PlayerHealthUpdateAction;

    private Rigidbody _rigidbody;
    private Animator _animator;
    [SerializeField] private GameObject _playerBullet;
    
    private float PlayerMoveSpeed = 40f;
    private float PlayerHealth = 100f;
    private float startTime;
    private float survivedTime;
    
    private float velocityZ = 0.0f;
    private float velocityX = 0.0f;

    private Vector3 clickPosition;
    public Vector3 bulletForceDirection;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerBullet.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {       
            PlayerMovement();
            PlayerAnimation();
            CastRay();
            Fire();
    }

    void PlayerMovement()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // Check for input
        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1f;
        }

        // Calculate movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * (PlayerMoveSpeed * Time.deltaTime);

        // Apply movement to the rigidbody
        _rigidbody.MovePosition(transform.position + movement);
        
        //Topdown Rotation
        /*
         Vector3 TargetDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _rigidbody.position;
         float angle = Mathf.Atan2(TargetDirection.z, TargetDirection.x) * Mathf.Rad2Deg - 90f;
         _rigidbody.rotation = Quaternion.Euler(angle,0,angle);
         */
    }
    void PlayerAnimation()
    {
        // for going forward animation
        var forward = Input.GetKey(KeyCode.W);
        if (forward)
        {
            velocityZ += 1f * Time.deltaTime;
        }
        // for decreasing forward
        else if (!forward && velocityZ >= 0f)
        {
            velocityZ -= 1f * Time.deltaTime;
        }

        // for going backward animation
        var backward = Input.GetKey(KeyCode.S);
        if (backward)
        {
            velocityZ -= 1f * Time.deltaTime;
        }
        // for decreasing backward
        else if (!backward && velocityZ <= 0f)
        {
            velocityZ += 1f * Time.deltaTime;
        }

        //for going left animation
        var left = Input.GetKey(KeyCode.A);
        if (left)
        {
            velocityX -= 1f * Time.deltaTime;
        }
        //for decreasing left
        else if (!left && velocityX <= 0f)
        {
            velocityX += 3f * Time.deltaTime;
        }

        //for going right animation
        var right = Input.GetKey(KeyCode.D);
        if (right)
        {
            velocityX += 1f * Time.deltaTime;
        }
        //for decreasing right 
        else if (!right && velocityX >= 0f)
        {
            velocityX -= 3f * Time.deltaTime;
        }

        _animator.SetFloat("VelocityZ", velocityZ); //for going forward
        _animator.SetFloat("VelocityX", velocityX);
    }
    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //fire
            BulletGenerateAction?.Invoke(1f,bulletForceDirection);
        }
    }
    void CastRay()
    {
        float raycastDistance = 100f;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                //var offset = transform.position + new Vector3(0,0f,0);
                bulletForceDirection = (hit.point - transform.position).normalized;
                clickPosition = hit.point;
                //Debug.Log("Hit object: " + hit.collider.gameObject.name + " Hit position:  " + clickPosition);
                //Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green,3f);
                //Debug.DrawRay(transform.position, bulletForceDirection * raycastDistance, Color.yellow, 3f);
            }
            else
            {
               // Debug.Log("Raycast hit nothing.");
               // Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red,3f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " is hit by "+ other.gameObject.name);
        if (other.gameObject.name == "EnemyBullet" || other.gameObject.name == "EnemyClone")
        {
            //Debug.Log(gameObject.name + " is hit by bullet");
            PlayerHealth -= 20f;
            PlayerHealthUpdateAction?.Invoke(PlayerHealth);
            Destroy(other.gameObject);  //bullet
            if (PlayerHealth <= 0)
            {
                Debug.Log("player dead");
                PlayerDeadAction?.Invoke();
                Debug.Log("invoke called");
            }
        }
    }
}
