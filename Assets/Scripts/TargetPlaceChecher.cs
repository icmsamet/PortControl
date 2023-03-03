using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TargetPlaceChecher : MonoBehaviour
{
    [SerializeField] private GameObject targetShip;
    [SerializeField] private Transform targetPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetShip.gameObject)
        {
            targetShip.GetComponent<DrawingPath>().ForwardSpeed = 0;
            targetShip.transform.DOMove(targetPoint.position, 3, false);
            targetShip.transform.DORotate(new Vector3(0,0,-90), 2, RotateMode.Fast).SetDelay(2);
            targetShip.transform.DOMoveY(targetPoint.position.y - 0.45f, 2).SetDelay(4);
            Destroy(targetShip.transform.GetChild(0).gameObject, 6);
            targetShip.transform.DOScale(0, 2).SetDelay(6);
            Destroy(targetShip,8);
        }
    }
}
