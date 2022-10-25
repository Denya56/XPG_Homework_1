using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedW;
    [SerializeField] private float _speedR;
    [SerializeField] private float _dash;
    [SerializeField] private float _jumpAccelerationH;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private LayerMask platformsLayerMask;
    private Camera _cam;
    private float _camHeight;
    private float _camWidth;
    private float _cameraRight;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _cam = Camera.main;
        _camHeight = 2f * _cam.orthographicSize;
        _camWidth = _camHeight * _cam.aspect;
        _cameraRight = _camWidth / 2;
    }

    private void Update()
    {
        float xDisplacement = Input.GetAxis(Axis.HORIZONTAL);
        Vector3 displacementVector = new Vector3(xDisplacement, 0, 0);

        if (Input.GetButton(Axis.RUN))
            transform.Translate(displacementVector * _speedR * Time.deltaTime);
        else if (Input.GetButtonDown(Axis.DASH))
        {
            //Debug.Log(Axis.DASH);
            transform.Translate(displacementVector * _dash * Time.deltaTime);
        }
            
        else
            transform.Translate(displacementVector * _speedW * Time.deltaTime);

        //Debug.Log(cam.transform.position.x - cameraRight);
        if (_rigidbody2D.position.x < _cam.transform.position.x - _cameraRight)
            _rigidbody2D.position = new Vector2(_cam.transform.position.x - _cameraRight, _rigidbody2D.position.y);

        if(xDisplacement > 0.01f)
            transform.localScale = Vector3.one;
        else if (xDisplacement < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        
        if (Input.GetButtonDown(Axis.JUMP) && IsGrounded())
            Jump();

        _animator.SetBool("isWalking", xDisplacement != 0);
        _animator.SetBool("isGrounded", IsGrounded());
;   }

    private void Jump()
    {          
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x + _jumpAccelerationH, _jumpHeight);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }
}