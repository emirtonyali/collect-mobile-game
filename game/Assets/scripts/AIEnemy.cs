using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{

    public Transform target; // Player's collector object
    public Transform dropZone; // drop zone
    public float speed; 
    public float waitTime; 
    public float moveTime; 
    public float distanceofArea; //distance between AI and zone

    public LevelEditor levelEditor;
    private bool waiting = false; 
    private float waitTimer = 0f; 
    [SerializeField] private bool movingToDropZone = false; 
    private float moveTimer = 0f; 
    private Vector3 previousTargetPos; 
    [SerializeField] private playerScriptable enemyScriptable;
    public MainMove mainMove;
    private void Start()
    {

        speed = enemyScriptable.speed;

        GetComponent<Renderer>().material.color = enemyScriptable.playerColor;
    }
    void Update()
    {
        if (mainMove.gamePlayable)
        {
            if (target != null)
            {
                if (!waiting && !movingToDropZone)
                {
                    // move to target
                    transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                    // and change rotation
                    Vector3 direction = target.position - transform.position;
                    transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

                    if (Vector3.Distance(transform.position, target.position) < 1f)
                    {
                        waiting = true; // when reach to target wait 
                    }
                }
                else if (waiting)
                {
                    waitTimer += Time.deltaTime;

                    if (waitTimer > waitTime)
                    {
                        waitTimer = 0f;
                        waiting = false;
                        movingToDropZone = true; // after wait  move drop zone
                    }
                }
                else if (movingToDropZone)
                {

                    moveTimer += Time.deltaTime;

                    if (moveTimer > moveTime)
                    {
                        moveTimer = 0f;
                        movingToDropZone = false; 
                        previousTargetPos = target.position; 
                        target = null; 
                    }
                }
            }
            else
            {
                //move to drop zone
                transform.position = Vector3.MoveTowards(transform.position, dropZone.position, speed * Time.deltaTime); 

                // Rotasyonu býrakma bölgesine doðru çevir
                Vector3 direction = dropZone.position - transform.position;
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
                if (Vector3.Distance(transform.position, dropZone.position) < distanceofArea)
                {
                    Debug.Log("dropzonee!");
                    if (levelEditor.cubes.Count > 0)
                    {
                        int i = Random.Range(0, levelEditor.cubes.Count);


                        target = levelEditor.cubes[i].transform;


                        Debug.Log(i);
                    }

                }

            }
        }
       
    }
}
