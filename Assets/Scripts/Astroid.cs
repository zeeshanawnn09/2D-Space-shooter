using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;

    [SerializeField]
    private GameObject _Explosion;

    private SpawnManager _spawnManager;
   
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
      
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        Vector3 roatate = new Vector3(0f, 0f, 90.0f);
        transform.Rotate(roatate * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Laser")
        {
           
            Instantiate(_Explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            _spawnManager.StartSpawning();
            
            Destroy(this.gameObject, 0.25f);
        }
        
    }
}
