using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int _powerupId;

    [SerializeField]
    private AudioClip _audioClip;


    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -5.6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag == "Player")
        {
            //communicate with player script
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_audioClip,transform.position);
            
            if (player != null)
            {
                switch(_powerupId)
                {
                    case 0:
                        player.TripleShotEnable();
                        break;
                    case 1:
                        player.SpeedBoostEnable();
                        break;
                    case 2:
                        player.ShieldEnable();
                        break;
                    default:
                        UnityEngine.Debug.Log("Default");
                        break;
                }
            }

            Destroy(this.gameObject);

        }
    }

}
