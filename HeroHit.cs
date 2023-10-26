using UnityEngine;
using System.Collections;

public class HeroHit : MonoBehaviour {

    public TestHero hero;
    private EnemyManager _enemyManager;

    void Start( )
    {
        _enemyManager = GameObject.FindObjectOfType(typeof(EnemyManager)) as EnemyManager;
    }

    void OnTriggerEnter2D( Collider2D other )
    {
        if(other.tag == "Enemy") {
            if(Vector3.Dot(other.transform.right, transform.right) > 0) {
                Debug.Log("hit back");
                other.gameObject.SetActive(false);
                _enemyManager.enemyNumber -= 1;
                Debug.Log("enemyNumber :" + _enemyManager.enemyNumber);
            }
            else {
                Debug.Log("hit front");
                hero.nowBlood -= 1;
                other.gameObject.SetActive(false);
                _enemyManager.enemyNumber -= 1;
                Debug.Log("enemyNumber :" + _enemyManager.enemyNumber);
            }
        }
    }
}
