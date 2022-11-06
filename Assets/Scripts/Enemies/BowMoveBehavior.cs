using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMoveBehavior : StateMachineBehaviour
{
    [SerializeField] private float _speedChase;
    private Transform _player;
    private Rigidbody2D _rb;
    private bool _facesRight = true;
    private Vector3 v;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag(Tag.PLAYER).transform;
        _rb = animator.GetComponent<Rigidbody2D>();
        v = _rb.transform.localScale;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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

        if (Vector2.Distance(_rb.position, _player.position) >= 5f) 
            animator.SetBool("isChasing", false);
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
}
