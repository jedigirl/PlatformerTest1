using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    private Vector3 startPosition;
    private Quaternion startRotation;
    public static int level = 0;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        startRotation = transform.rotation;
	}

    void NextLevel()
    {
        level++; 
        if(level >= SceneManager.sceneCountInBuildSettings) {
            level = 0;
        }
        SceneManager.LoadScene(level);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death")
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            GetComponent<Animator>().Play("LOSE00", -1, 0f);
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
        }
        else if (other.tag == "Checkpoint")
        {
            startPosition = other.transform.position;
            startRotation = other.transform.rotation;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Goal")
        {
            Destroy(other.gameObject);
            GetComponent<Animator>().Play("WIN00", -1, 0f);
            Invoke("NextLevel", 3f);
        }
    }
}
