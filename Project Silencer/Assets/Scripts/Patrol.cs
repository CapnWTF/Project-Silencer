using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {
    public Transform[] patrolPoints;
    public float moveSpeed;
    private int currentPoint; //where this object is in the world

	// Use this for initialization
	void Start () {
        transform.position = patrolPoints[0].position;
        currentPoint = 0;
        print("There are: " + patrolPoints.Length + " patrol points in this level");
	}

    // Update is called once per frame
    void Update() {
        if (patrolPoints.Length > 1)
         {
            if (transform.position == patrolPoints[currentPoint].position)
            {
                currentPoint++;

            }
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
         }

       }
}
