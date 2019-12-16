using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public readonly Vector3[] lookpoints;
    public readonly bool[] jump;
    public readonly int[] jumpHight;
    public readonly Line[] turnBoundaries;
    public readonly int finishLineIndex;
    public readonly int slowDownIndex;

    public Path(Vector3[] wayPoints, Vector3 startPos, float turnDst, float stoppingDst)
    {
        lookpoints = wayPoints;
        jump = new bool[lookpoints.Length];
        jumpHight = new int[lookpoints.Length];
        turnBoundaries = new Line[lookpoints.Length];
        finishLineIndex = lookpoints.Length - 1;

        Vector2 previousPoint = V3ToV2(startPos);
        float preHight = lookpoints[0].y;
        for (int i = 0; i < lookpoints.Length; i++)
        {
            if (lookpoints[i].y - preHight > 1f && lookpoints[i].y - preHight < 20f)
            {
                jump[i] = true;
                jumpHight[i] = Mathf.Abs(Mathf.RoundToInt(lookpoints[i].y - preHight));
            }
            else
                jump[i] = false;
            preHight = lookpoints[i].y;

            Vector2 currentPoint = V3ToV2(lookpoints[i]);
            Vector2 dirToCurrentPoint = (currentPoint - previousPoint).normalized;
            Vector2 turnBoundaryPoint = (i == finishLineIndex) ? currentPoint : currentPoint - dirToCurrentPoint * turnDst;
            turnBoundaries[i] = new Line(turnBoundaryPoint, previousPoint - dirToCurrentPoint * turnDst);
            previousPoint = turnBoundaryPoint;
        }

        float dstFromEndPoint = 0;
        for (int i = lookpoints.Length - 1; i > 0; i--)
        {
            dstFromEndPoint += Vector3.Distance(lookpoints[i], lookpoints[i - 1]);
            if (dstFromEndPoint >= stoppingDst)
            {
                slowDownIndex = i;
                break;
            }
        }
    }

    public Vector2 V3ToV2(Vector3 V3)
    {
        return new Vector2(V3.x, V3.z);
    }

    public void DrawWithGizmos()
    {
        Gizmos.color = Color.black;
        foreach (Vector3 p in lookpoints)
        {
            Gizmos.DrawCube(p + Vector3.up, Vector3.one);
        }

        Gizmos.color = Color.white;
        foreach (Line l in turnBoundaries)
        {
            l.DrawWithGizmos(10);
        }
    }
}
