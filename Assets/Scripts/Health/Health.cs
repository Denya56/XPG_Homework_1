using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float _startingHealth;
    public float _currentHealth { get; private set; }
    private Animator _animator;
    private bool _dead;

    private void Awake()
    {
        _currentHealth = _startingHealth;
        _animator = GetComponent<Animator>();
    }
    public void TakeDamage(float _damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - _damage, 0, _startingHealth);

        if(_currentHealth > 0 )
        {
            _animator.SetTrigger("Hurt");
        }
        else
        {
            if (!_dead)
            {
                _animator.SetTrigger("Die");
                GetComponent<PlayerMovement>().enabled = false;
                _dead = true;
                SceneManager.LoadScene("SampleScene");
            }            
        }
    }
}
