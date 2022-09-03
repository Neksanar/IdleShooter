using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera mainCamera;

    [SerializeField] private Transform _playerModel;
    [SerializeField] private Transform _weapon;

    public Transform spawnBullet;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _shootCouldown = 0.1f;
    private float _currentTimeCouldown;

    
    [SerializeField] private int _poolCount = 100;
    [SerializeField] private bool _autoExpand = false;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletContainer;


    private Pool<Bullet> _pool;


    public Game _game;

    private void Start() 
    {
        this._pool = new Pool<Bullet>(_bulletPrefab, _poolCount, _bulletContainer);
        this._pool.autoExpand = _autoExpand;
    }

    void Update()
    {
        _currentTimeCouldown += Time.deltaTime;

        #if UNITY_EDITOR
          if (Input.GetMouseButtonDown(0) && _game.IsStart)
        #endif
        #if UNITY_ANDROID
           // if (Input.touchCount > 0 && _game.IsStart)   
        #endif
        {
            if (_currentTimeCouldown > _shootCouldown)
            {
                Shoot();
                _currentTimeCouldown = 0;
            }
            
        }   
    }

    private void Shoot()
    {
        #if UNITY_EDITOR
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        #endif
        #if UNITY_ANDROID
           // Ray ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position); 
        #endif

        

        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 direction = targetPoint - spawnBullet.position;


        _playerModel.LookAt(targetPoint);
        _weapon.LookAt(targetPoint);


        this.CreateBullet(spawnBullet.position, direction, _speed, targetPoint);

    }

    private void CreateBullet(Vector3 position, Vector3 direction, float speed, Vector3 lookAt)
    {
        var bullet = this._pool.GetFreeElement();
        bullet.transform.position = position;
        bullet.GetComponent<Rigidbody>().velocity = direction * speed;
        bullet.transform.LookAt(lookAt);
        
    }
}
