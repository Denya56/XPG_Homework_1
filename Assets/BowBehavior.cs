using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BowBehavior : StateMachineBehaviour
{
    [SerializeField] private float _speedChase;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private int _damage;
    private float _cooldownTimer = Mathf.Infinity;
    private Transform _player;
    private Rigidbody2D _rb;
    private bool _facesRight = true;
    private Vector3 v;
    private Health _playerHealth;
    private bool _isChasing = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag(Tag.PLAYER).transform;
        _rb = animator.GetComponent<Rigidbody2D>();
        v = _rb.transform.localScale;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _cooldownTimer += Time.deltaTime;
        if (Vector2.Distance(_rb.position, _player.position) < 1f)
        {
            if (_cooldownTimer >= _attackCooldown)
            {
                Attack();
                _cooldownTimer = 0;
                animator.SetTrigger("Attack");
            }
        }
        else if (Vector2.Distance(_rb.position, _player.position) < 5f)
        {
            if (_rb.position.x < _player.position.x && !_facesRight)
            {
                Flip();
            }
            if (_rb.position.x > _player.position.x && _facesRight)
            {
                Flip();
            }
            /*if (!_isChasing)
            {
                
                _isChasing = true;
            }*/
            animator.SetTrigger("Chase");
            Chase();
        }
        else animator.ResetTrigger("Chase");
    }

    private void Chase()
    {
        
        Vector2 target = new Vector2(_player.position.x, _player.position.y);
        Vector2 newPosition = Vector2.MoveTowards(_rb.position, target, _speedChase * Time.deltaTime);
        _rb.MovePosition(newPosition);
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

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
