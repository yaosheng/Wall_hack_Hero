using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour {

    public List<Enemy> enemyGroup = new List<Enemy>( );
    public int enemyNumber;
    public int thisScene;

    void Start () {
        enemyNumber = enemyGroup.Count;
        Debug.Log("enemyNumber : " + enemyNumber);
        thisScene = SceneManager.GetActiveScene( ).buildIndex;
    }

	void Update () {
	    if(enemyNumber == 0) {
            StartCoroutine(DelayToNextScene( ));
        }
	}

    public void CheckEnemyPosition( )
    {
        foreach(Enemy enemy in enemyGroup) {
            enemy.CheckEnemyPosition( );
        }
    }

    IEnumerator DelayToNextScene( )
    {
        yield return new WaitForSeconds(1.5f);

        if(thisScene < 3) {
            SceneManager.LoadScene(thisScene + 1);
        }
        else {
            SceneManager.LoadScene(1);
        }
    }

}
