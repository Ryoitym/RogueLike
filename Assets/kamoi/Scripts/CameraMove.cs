using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    /// <summary>
    /// 現在のカメラ位置
    /// </summary>
    enum CameraPos
    {
        UnderLeft, UnderRight, UpperLeft, UpperRight
    }

    /// <summary>
    /// カメラを動かす方向
    /// </summary>
    enum Direction
    {
        Left, Right, Up, Down
    }

    public Transform player;
    CameraPos currentPosition;
    private bool isMoving = false;

    void Start()
    {
        currentPosition = CameraPos.UnderLeft;
    }

    void Update()
    {
        Detection();
    }

    /// <summary>
    /// プレイヤーが端っこに到達したら検出
    /// </summary>
    void Detection()
    {
        if ((player.position.x > 7) && ((currentPosition == CameraPos.UnderLeft) || currentPosition == CameraPos.UpperLeft))
        {
            Move(Direction.Right);
        }
        if ((player.position.y > 7) && ((currentPosition == CameraPos.UnderLeft) || currentPosition == CameraPos.UnderRight))
        {
            Move(Direction.Up);
        }
        if ((player.position.x < 8) && ((currentPosition == CameraPos.UnderRight) || currentPosition == CameraPos.UpperRight))
        {
            Move(Direction.Left);
        }
        if ((player.position.y < 8) && ((currentPosition == CameraPos.UpperLeft) || currentPosition == CameraPos.UpperRight))
        {
            Move(Direction.Down);
        }
    }

    /// <summary>
    /// 指示された方向にカメラを動かす
    /// </summary>
    /// <param name="direction">指示する方向</param>
    void Move(Direction direction)
    {
        if (isMoving) return;   //カメラ移動中は無視する
        switch (direction)
        {
            case Direction.Right:
                //transform.position += new Vector3(8, 0, 0);
                StartCoroutine(Scroll(new Vector3(8, 0, 0))); Debug.Log("Right");
                if (currentPosition == CameraPos.UnderLeft)
                {
                    currentPosition = CameraPos.UnderRight;
                }
                else
                {
                    currentPosition = CameraPos.UpperRight;
                }
                break;
            case Direction.Up:
                //transform.position += new Vector3(0, 8, 0);
                StartCoroutine(Scroll(new Vector3(0, 8, 0))); Debug.Log("Up");

                if (currentPosition == CameraPos.UnderLeft)
                {
                    currentPosition = CameraPos.UpperLeft;
                }
                else
                {
                    currentPosition = CameraPos.UpperRight;
                }
                break;
            case Direction.Left:
                //transform.position += new Vector3(-8, 0, 0);
                StartCoroutine(Scroll(new Vector3(-8, 0, 0))); Debug.Log("Left");
                if (currentPosition == CameraPos.UnderRight)
                {
                    currentPosition = CameraPos.UnderLeft;
                }
                else
                {
                    currentPosition = CameraPos.UpperLeft;
                }
                break;
            case Direction.Down:
                //transform.position += new Vector3(0, -8, 0);
                StartCoroutine(Scroll(new Vector3(0, -8, 0))); Debug.Log("Down");
                if (currentPosition == CameraPos.UpperLeft)
                {
                    currentPosition = CameraPos.UnderLeft;
                }
                else
                {
                    currentPosition = CameraPos.UnderRight;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// スクロール用コルーチン
    /// </summary>
    /// <param name="totalMoveMent">総移動量</param>
    /// <returns></returns>
    IEnumerator Scroll(Vector3 totalMoveMent)
    {
        Vector3 firstPosition = transform.position;
        Vector3 totalAbs = totalMoveMent;
        if (totalAbs.x < 0 || totalAbs.y < 0)
        {
            totalAbs.x *= -1;
            totalAbs.y *= -1;
        }
        isMoving = true;
        //Vector3 endPosition = totalMoveMent + transform.position;
        //Debug.Log("Init total" + totalMoveMent);
        //Debug.Log("Init move" + move);
        //Debug.Log("Init firstPos" + firstPosition);
        //Debug.Log("Init endPos" + endPosition);

        while (true)
        {
            transform.position += totalMoveMent * Time.deltaTime;
            //Debug.Log("条件" + Mathf.Abs(transform.position.x - firstPosition.x)+":"+totalAbs.x);
            if ((Mathf.Abs(transform.position.x - firstPosition.x) > totalAbs.x) || (Mathf.Abs(transform.position.y - firstPosition.y) > totalAbs.y))
            {
                //Debug.Log("コルーチン終了");
                isMoving = false;
                yield break;
            }
            yield return null;
        }
    }
}
