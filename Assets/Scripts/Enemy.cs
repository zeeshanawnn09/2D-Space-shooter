using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private int _enemy_speed = 5;

    private Player _player;

    private Animator _animator;

    private AudioSource _audioSource;

  /*  [SerializeField]
    private GameObject _laserPrefab;

    private float _fireRate;
    private float _canFire = -1;*/

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
     
        if (_player == null)
        {
            Debug.Log("Player is NULL");
        }

        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.Log("Animator is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        /*if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;

           GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
           Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            
            for(int i=0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }

        }*/
    }

    void Movement()
    {
        transform.Translate(Vector3.down * _enemy_speed * Time.deltaTime);
        if(transform.position.y <= -5.6f)
        {
            float X_rand = Random.Range(-8.59f, 8.59f);
            transform.position = new Vector3(X_rand, 7.46f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //when enemy collides with laser
        if(other.transform.tag == "Laser")
        {
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddScore();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _enemy_speed = 0;
            //Destroy enemy after 2.8 Second
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
            
        }

        //when enemy collides with player
        if(other.transform.tag == "Player")
        {
            //get the damage component from player
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _enemy_speed = 0;
            _audioSource.Play();

            
            Destroy(this.gameObject, 2.8f);
            
        }
    }
}
