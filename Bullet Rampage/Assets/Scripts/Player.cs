using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    [SerializeField] private GameObject _9mmBullet;
    
    [SerializeField] private float PlayerMoveSpeed = 5f;
    private float velocityZ = 0.0f;
    private float velocityX = 0.0f;

    private Vector3 clickPosition;
    public Vector3 bulletForceDirection;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerAnimation();
        CastRay();
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
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * PlayerMoveSpeed * Time.deltaTime;

        // Apply movement to the rigidbody
        _rigidbody.MovePosition(transform.position + movement);
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

    void CastRay()
    {
        float raycastDistance = 100f;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                Vector3 DistanceToTarget = hit.point - _9mmBullet.transform.position;
                bulletForceDirection = DistanceToTarget.normalized;
                clickPosition = hit.point;
                //Debug.Log("Hit object: " + hit.collider.gameObject.name + " Hit position:  " + clickPosition);
                //Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green);
                //Debug.DrawRay(transform.position, bulletForceDirection * raycastDistance, Color.yellow);
            }
            else
            {
                //Debug.Log("Raycast hit nothing.");
                //Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
            }
        }
    }
}