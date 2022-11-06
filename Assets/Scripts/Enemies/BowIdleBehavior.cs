using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BowIdleBehavior : StateMachineBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private int _damage;
    private float _cooldownTimer = Mathf.Infinity;
    private Transform _player;
    private Rigidbody2D _rb;
    private Vector3 v;
    private Health _playerHealth;
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
            animator.SetBool("isChasing", true);
        }
    }
    private void Attack()
    {
        _playerHealth = _player.transform.GetComponent<Health>();
        _playerHealth.TakeDamage(_damage);
    }
}
