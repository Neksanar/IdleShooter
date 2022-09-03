using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class CharController : MonoBehaviour
{
    [SerializeField]
    private Transform[] _wayPoints; 
    private int _wayPointCounter = 0;

    private NavMeshAgent _navMeshAgent;

    int WAYPOINT_LAYER_MASK = 8;

    [SerializeField]
    private Game _game;

    public Animator characterAnimator;



    void Start()
    {        
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        if (_wayPoints.Length == 0)
        {
            Debug.Log("Destination doesn't exist!");
            return;
        }
    }
 
    void Update()
    {  
        if (_game.IsStart && _game.EnemyCounter == 0)
        {
            characterAnimator.SetBool("IsRunning", true);
            _navMeshAgent.SetDestination(_wayPoints[_wayPointCounter].position);
        }   
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == WAYPOINT_LAYER_MASK)
        {
            characterAnimator.SetBool("IsRunning", false);
            _wayPointCounter++;
            
            if (_wayPointCounter > _wayPoints.Length - 1)
            {
                _wayPointCounter = 0;

                _game.Restart();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }
            _game.SpawnEnemy(transform.position);
        }
    }
}
