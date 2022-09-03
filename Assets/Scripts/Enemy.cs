using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _sliderUI;

    private bool _isDead = false;
    private float _lifeTime = 2f;


    private void Start() 
    {
        _health = _maxHealth;
        
    }
    private void Update() 
    {
        if (this._isDead)
        {
            _lifeTime -= Time.deltaTime;
            if (_lifeTime < 0)
            {
                Destroy(this.gameObject);
            }
            return; 
        }
            

        _slider.value = _health / _maxHealth;
        if (_health <= 0)
        {
            _sliderUI.SetActive(false);
            _isDead = true;
            this.gameObject.GetComponent<RagdollControl>().MakePhysical();
            Game.KillEnemy();
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
    
}
