using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public int lives = 3;
    public GameObject ship;
    // Start is called before the first frame update
    void Start()
    {
        lives++;
        SpawnShip();
    }

    // Update is called once per frame
    public void SpawnShip()
    {
        if (lives > 0) 
        {
            lives--;
            StartCoroutine(SpawnRoutine());
        }

        else Debug.Log("GAME OVER");
    }   

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(1);
        GameObject newShip = Instantiate(ship, transform.position, transform.rotation);
        newShip.GetComponent<Ship>().spawn = this;
    }

}
