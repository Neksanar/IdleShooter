using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _lifeTime = 3f;
    private float _currentTime;

    [SerializeField] private float _damage = 50f;

    private void Update() 
    {
        if (this.gameObject.activeInHierarchy)
            _currentTime += Time.deltaTime;

        if (_currentTime > _lifeTime)
        {
            _currentTime = 0;
            this.gameObject.SetActive(false);
        } 
            
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.transform.gameObject.tag == "Enemy")
        {   
            other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            this.gameObject.SetActive(false);
        }
    }
}
