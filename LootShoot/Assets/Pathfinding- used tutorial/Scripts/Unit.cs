using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime = .5f;
    const float pathUpdateMoveThreshhold = .5f;
    const float acceptedSizeDifference = .5f;

    Transform target;
    int targetIndex;
    Gridy grid;

    public int maxJumpHeight = 5;
    public float speed = 0;
    public float turnDst = 5;
    public float turnSpeed = 3;
    public float stoppingDst = 10;
    public bool slowDown = true;
    Path path;
    static List<Vector3[]> paths = new List<Vector3[]>();

    Rigidbody rb;
    Collider coll;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        grid = FindObjectOfType<Gridy>();
        stopwatch.Start();
        FindPath();
        StartCoroutine(UpdatePath());
    }


    void FindPath()
    {
        Vector3[] reusedPath = new Vector3[0];
        foreach (Vector3[] p in paths) //check if there is already a path
        {
            float differenceXStart = Mathf.Abs(transform.position.x - p[0].x); //check how close values are to already existing paths
            float differenceYStart = Mathf.Abs(transform.position.z - p[0].z);

            float differenceXEnd = Mathf.Abs(target.position.x - p[p.Length - 1].x);
            float differenceYEnd = Mathf.Abs(target.position.z - p[p.Length - 1].z);

            if (differenceXStart < acceptedSizeDifference && differenceYStart < acceptedSizeDifference && differenceXEnd < acceptedSizeDifference && differenceYEnd < acceptedSizeDifference)
            {
                reusedPath = p;
            }
        }

        if (reusedPath.Length == 0)
        {
            PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound)); //makes request for a path with all info needed for it
            stopwatch.Restart();
        }
        else
        {
            OnPathFound(reusedPath, true);
        }
    }

    public void OnPathFound(Vector3[] wayPoints, bool pathSuccessful) //magic
    {
        if (pathSuccessful)
        {
            path = new Path(wayPoints, transform.position, turnDst, stoppingDst);

            if (!paths.Contains(path.lookpoints))
            {
                paths.Add(path.lookpoints);
            }
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        if (!pathSuccessful)
            Debug.LogWarning("not found");
    }

    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    System.Diagnostics.Stopwatch stuckWatch = new System.Diagnostics.Stopwatch(); //used for checking if AI is stuck
    IEnumerator UpdatePath()
    {
        //if (Time.timeSinceLevelLoad < .3f) //path gets janky the first frames
        //    yield return new WaitForSeconds(.3f);

        //FindPath();
        /*

        
        stuckWatch.Reset();
        stuckWatch.Start();

        if(stuckWatch.ElapsedMilliseconds > 3000) //checks every 3 seconds if AI is stuck
            {
                float xDist = transform.position.x - lastPos.x;
                float zDist = transform.position.z - lastPos.z;
                float moveSinceLastCheck = xDist * xDist + zDist * zDist;
                
                if (moveSinceLastCheck < 0.5f) //if the AI has not moved 0.5 unit(meter) since the last check, calculate new path as it has likely gotten stuck
                {
                    print("too long since last check, recalculating path");
                    FindPath();
                    break;
                }
                lastPos = transform.position;
                stuckWatch.Restart();
            }
            
        */

        float sqrThreshhold = pathUpdateMoveThreshhold * pathUpdateMoveThreshhold;
        Vector3 oldTargetPos = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime); //dont call loop constantly, so it wont crash
            if ((target.position - oldTargetPos).sqrMagnitude > sqrThreshhold && stopwatch.ElapsedMilliseconds > minPathUpdateTime) //if target has moved more than 0.5 m from old position, find new path to that position
            {
                FindPath(); //pathfinding script ooi when called
                oldTargetPos = target.position;
            }
        }
    }

    Ray frontCheck, topCheck, groundCheck;
    private float timeSinceJump = 0;

    IEnumerator FollowPath() //basiclly same as Update but can be started and stoped. for moving to target along path
    {
        bool grounded = false;
        Vector3 lastPos = transform.position;
        bool followingPath = true;
        int pathIndex = 0;
        //transform.LookAt(path.lookpoints[0]); //face first waypoint

        float speedPercent = 1;

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {

                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    //FindNextPath();
                    break;
                }
                else
                    pathIndex++;
            }

            if (followingPath)
            {
                speedPercent = 1;
                if (pathIndex >= path.slowDownIndex && stoppingDst > 0 && slowDown)
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex /*- 1*/].DistanceFromPoint(pos2D) / stoppingDst + 0.2f); //add to avoid stopping compleatly

                Quaternion targetRotation = Quaternion.LookRotation(path.lookpoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);

                frontCheck = new Ray(transform.position + Vector3.up / 3, transform.forward + Vector3.up * maxJumpHeight / 2);
                topCheck = new Ray(transform.position + Vector3.up * maxJumpHeight, transform.forward);
                groundCheck = new Ray(transform.position + Vector3.up / 2, Vector3.down);
                RaycastHit obstacle;

                timeSinceJump += Time.deltaTime;
                grounded = Physics.Raycast(groundCheck, 1); //check if AI is standing on ground                             

                if (Physics.Raycast(frontCheck.origin, frontCheck.direction, out obstacle, 4, grid.walkableMask.value, QueryTriggerInteraction.Ignore)) //something is in the way
                {
                    if (!Physics.Raycast(topCheck, 5)) //check that the obstacle can be jumped over
                    {
                        if (grounded) //if AI is standing on the ground
                        {
                            if (timeSinceJump > 1) //this is to prevent the AI from jumping more than once per second
                            {
                                rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(maxJumpHeight * Physics.gravity.magnitude * 2), rb.velocity.z);
                                //rb.AddForce(Vector3.up * Mathf.Sqrt(maxJumpHeight * Physics.gravity.magnitude * 2), ForceMode.Impulse);
                                timeSinceJump = 0;
                            }
                        }
                    }
                    else
                    {
                        grid.NodeFromWorldPoint(obstacle.point).walkable = false; //tried jumping, didnt work. set this to unwalkable
                    }
                }

                if (slowDown)
                    transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
            }

            yield return null; //wait one frame
        }
    }

    //void FindNextPath()
    //{
    //    targetIndex = targetIndex == targets.Length - 1 ? 0 : targetIndex + 1;
    //    target = targets[targetIndex]; //new path gets found in update path
    //    FindPath();
    //}

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }
}
