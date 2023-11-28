using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private int _laser_speed = 8;

  //  private bool _isEnemyLaser = false;
  
    // Update is called once per frame
    void Update()
    {
       
            FireUp();
        
    }

    void FireUp()
    {
        transform.Translate(Vector3.up * _laser_speed * Time.deltaTime);
        if (transform.position.y >= 7.18f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject); //destroy the game object that 'this' script is using
        }
    }

/*    void FireDown()
    {
        transform.Translate(Vector3.down * _laser_speed * Time.deltaTime);
        if (transform.position.y < -8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject); //destroy the game object that 'this' script is using
        }
    }


    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }*/

}
