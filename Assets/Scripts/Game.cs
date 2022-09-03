using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour
{
    [SerializeField] private bool _isStart = false;
    public bool IsStart => _isStart;

    [SerializeField]  private Canvas _startMenu;

    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private int _enemyAmountPerWave = 3;

    private static int _enemyCounter = 0;

    public int EnemyCounter => _enemyCounter;

    void Update()
    {
        #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space) && _isStart == false)
        #endif
        #if UNITY_ANDROID
           // if (Input.touchCount > 0 && _isStart == false)
        #endif

        {
            _isStart = true;
            _startMenu.gameObject.SetActive(false);
        } 
    }

    public void SpawnEnemy(Vector3 position)
    {
        Vector3 newPosition;
        for (int i = 1; i < _enemyAmountPerWave + 1; i++)
        {
            float offsetX;
            if (i%2 == 0)
            {
                offsetX = - i;
            } else offsetX = i;
            newPosition = new Vector3(position.x + offsetX,
                                                position.y,
                                                position.z + Random.Range(10f, 15f));
            GameObject enemy = Instantiate(_enemyPrefab, newPosition, Quaternion.identity);
            enemy.transform.LookAt(position);
        }
        _enemyCounter = _enemyAmountPerWave;
    }

    public static void KillEnemy()
    {
        _enemyCounter--;
    }

    public void Restart()
    {
        _isStart = false;
        _enemyCounter = 0;
        _startMenu.gameObject.SetActive(true);
    }
}
