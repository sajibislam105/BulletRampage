using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action<float,Vector3> BulletGenerateAction;
    public Action PlayerDeadAction;
    public Action<float> PlayerHealthUpdateAction;

    private Rigidbody _rigidbody;
    private Animator _animator;
    [SerializeField] private GameObject _playerBullet;
    [SerializeField] private Joystick _joystick;

    [SerializeField]private float PlayerMoveSpeed = 40f;
    public float PlayerHealth = 100f;
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
            PlayerTouchMovement();    
            //PlayerMovement();
            //PlayerAnimation();
            //CastRay();
           // Fire();
          // FireByTouch();
    }

    void PlayerTouchMovement()
    {
        float moveHorizontal = _joystick.Horizontal;
        float moveVertical = _joystick.Vertical;
        
        // Player Touch movement
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * (PlayerMoveSpeed * Time.deltaTime);
        _rigidbody.MovePosition(transform.position + movement);
        _rigidbody.rotation = Quaternion.LookRotation(movement);
        
        //Animation Movement
        _animator.SetFloat("VelocityZ", moveVertical); //for going forward
        _animator.SetFloat("VelocityX", moveHorizontal);
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
        else if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1f;
        }
        else
        {
            moveVertical = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1f;
        }
        else
        {
            moveHorizontal = 0;
        }

        /*
        if (Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.S) == false)
        {
            var NoMovement = new Vector3(0, 0, 0);
            _rigidbody.MovePosition(transform.position + NoMovement);
        }
        */

        Debug.Log(moveHorizontal +"   "+ moveVertical);
        // Calculate movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * (PlayerMoveSpeed * Time.deltaTime);

        // Apply movement to the rigidbody
        _rigidbody.MovePosition(transform.position + movement);

        var Raymousepos = new Vector3(0,0,0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray,out raycastHit, 100f))
        {
            Raymousepos = raycastHit.point;
            //Debug.DrawRay(ray.origin,ray.direction * 100f ,Color.red);            
        }
        Vector3 aimDirection = (Raymousepos - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(aimDirection);
        //vector3 aimDirection = (Raymousepos - .position).normalized;
        //transform.rotation = Quaternion.LookRotation(aimDirection,Vector3.up);

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

    public void FireByTouch()
    {
        Debug.Log("Button Clicked");
        bulletForceDirection = Vector3.forward;
        BulletGenerateAction?.Invoke(1f,bulletForceDirection);
        
        float raycastDistance = 100f;
      /*
       if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(1);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
               var TouchPosition = hit.point;
               //bulletForceDirection = new Vector3(transform.position.x, 0f, transform.position.z) + new Vector3(1,0,1);
               //bulletForceDirection = (TouchPosition - transform.position).normalized;
            }
            Debug.DrawRay(ray.origin,ray.direction*raycastDistance,Color.red);
            BulletGenerateAction?.Invoke(1f,bulletForceDirection);
        }*/
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
        //Debug.Log(gameObject.name + " is hit by "+ other.gameObject.name);
        if (other.gameObject.name == "EnemyBullet" || other.gameObject.name == "EnemyClone")
        {
            //Debug.Log(gameObject.name + " is hit by bullet");
            PlayerHealth -= 20f;
            PlayerHealthUpdateAction?.Invoke(PlayerHealth);
            Destroy(other.gameObject);  //bullet
            if (PlayerHealth <= 0)
            {
               // Debug.Log("player dead");
                PlayerDeadAction?.Invoke();
               // Debug.Log("invoke called");
            }
        }
    }
}
