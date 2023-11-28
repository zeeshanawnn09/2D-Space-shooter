using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // when we create a private obj we can't change it's value in unity,
    // so help the designers to check the game we use

    [SerializeField]
    private float _speed = 9.0f;


    public float speed = 5.2f;

    [SerializeField]
    private GameObject _laser;

    [SerializeField]
    private float _fireRate = 0.15f;

    [SerializeField]
    private float _canFire = 0f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    private bool _isTripleshotEnabled = false;

    [SerializeField]
    private GameObject _TripleShotPrefab;


    private bool _isSpeedBoostEnabled = false;

    [SerializeField]
    private float _speedboost = 2.5f;

    private bool _isShieldEnabled = false;

    [SerializeField]
    private GameObject _ShieldActive;


    [SerializeField]
    private int _Score = 0;

    private UIManager _UImanager;

    [SerializeField]
    private GameObject _LeftEngine;

    [SerializeField]
    private GameObject _RightEngine;

    [SerializeField]
    private AudioClip _LaserClip;
    
    private AudioSource _AudioSource;



    // Start is called before the first frame update
    void Start()
    {
        //take the current position of the player
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _UImanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _AudioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager is null!");
        }

        if (_UImanager == null)
        {
            Debug.Log("UI Manager is NULL");
        }

        if (_AudioSource == null)
        {
            Debug.Log("Audio Source on player is NULL");
        }

        else
        {
            _AudioSource.clip = _LaserClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Spawn_Laser();
        }

    }

    void Spawn_Laser()
    {

        //Cool down time
        _canFire = Time.time + _fireRate;


        if (_isTripleshotEnabled == true)
        {
            Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            //Identity is used to not rotate the object to keep it in original form
            Instantiate(_laser, transform.position + (_laser.transform.up * 1.02f), Quaternion.identity);
        }

        //Sound is produced after light 
        _AudioSource.Play();

        
    }
    void PlayerMovement()
    {
        //Using keys to move

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        /*To move the player to the right
        transform.Translate(Vector3.right);
           
        if I want to do the same thing that is done above I can also do that by this
        transform.Translate(new Vector3(1, 0, 0));

        but the player is moving very fast so in order to improve that

        transform.Translate(Vector3.right * horizontal * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * vertical * _speed * Time.deltaTime);*/

        Vector3 direction = new Vector3(horizontal, vertical, 0);


        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.0f, 0));

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }



    public void Damage()
    {

        if(_isShieldEnabled == true)
        {
            _isShieldEnabled = false;
            _ShieldActive.SetActive(false);
            return;
        }

        _lives--;

        if(_lives == 2)
        {
            _LeftEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _RightEngine.SetActive(true);
        }
    

        _UImanager.Lives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotEnable()
    {
        _isTripleshotEnabled = true;
        //start power down Coroutine
        StartCoroutine(TriplePowerShotRoutine());

    }

    IEnumerator TriplePowerShotRoutine()
    {
        // wait for 5 seconds
        yield return new WaitForSeconds(5.0f);
        _isTripleshotEnabled = false;
    }

    public void SpeedBoostEnable()
    {
        _isSpeedBoostEnabled = true;
        _speed *= _speedboost;
        StartCoroutine(SpeedBoostRoutine());
    }

    IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostEnabled = false;
        _speed /= _speedboost;
    }

    public void ShieldEnable()
    {
        _isShieldEnabled = true;
        //Enabling shield
        _ShieldActive.SetActive(true);

    }

    public void AddScore()
    {
        _Score += 5;
        _UImanager.Score(_Score);
    }
}
