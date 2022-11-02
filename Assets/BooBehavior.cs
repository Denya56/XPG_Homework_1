using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooBehavior : StateMachineBehaviour
{
    [SerializeField] private float _speedPatrol;
    [SerializeField] private float _speedChase;
    [SerializeField] private float _travelDistance;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private int _damage;
    private float _cooldownTimer = Mathf.Infinity;
    private float _edgeRightX;
    private float _edgeLeftX;
    private Transform _player;
    private Rigidbody2D _rb;
    private bool _facesRight = true;
    private Vector3 v;
    private Health _playerHealth;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag(Tag.PLAYER).transform;
        _rb = animator.GetComponent<Rigidbody2D>();
        _edgeRightX = _rb.transform.position.x;
        _edgeLeftX = _edgeRightX + _travelDistance;
        v = _rb.transform.localScale;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _cooldownTimer += Time.deltaTime;
        if (Vector2.Distance(_rb.position, _player.position) < 1f)
        {
            if (_cooldownTimer >= _attackCooldown)
            {
                _cooldownTimer = 0;
                animator.SetTrigger("Attack");
                Attack();
            }
        }
        else if (Vector2.Distance(_rb.position, _player.position) < 7f)
        {
            if (_rb.position.x < _player.position.x && !_facesRight)
            {
                Flip();
            }
            if (_rb.position.x > _player.position.x && _facesRight)
            {
                Flip();
            }
            Chase();
        }
        else
            Patrol();
        
    }
    private void Chase()
    {
        Vector2 target = new Vector2(_player.position.x, _player.position.y);
        Vector2 newPosition = Vector2.MoveTowards(_rb.position, target, _speedChase * Time.deltaTime);
        _rb.MovePosition(newPosition);
        _edgeRightX = _rb.transform.position.x - 0.1f;
        _edgeLeftX = _edgeRightX + _travelDistance;
    }
    private void Patrol()
    {
        if (_rb.transform.position.x > _edgeLeftX)
        {
            Flip();
        } else if (_rb.transform.position.x < _edgeRightX)
        {
            Flip();
        }
        var dir = _facesRight ? _rb.transform.right : -_rb.transform.right;
        _rb.transform.Translate(dir * _speedPatrol * Time.deltaTime);
    }
    private void Flip()
    {
        _facesRight = !_facesRight;
        v = _rb.transform.localScale;
        v.x *= -1;
        _rb.transform.localScale = v;
    }
    private void Attack()
    {
        _playerHealth = _player.transform.GetComponent<Health>();
        _playerHealth.TakeDamage(_damage);
    }
}
