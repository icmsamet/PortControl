using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D),typeof(LineRenderer))]
public class DrawingPath : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    LineRenderer lineRenderer;
    public List<GameObject> WayPoints;
    public float timer = 0,speed, TimeForNextRay,ForwardSpeed,waitTimer = 0;
    int currentWaypoint = 0, wayIndex;
    bool move, touchStartedonPlayer;
    [SerializeField] private GameObject targetPlace,NearFieldArea,Warning;
    void Start()
    {
        StartCoroutine(warningTime());
        rigidbody2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        wayIndex = 1;
        move = false;
        touchStartedonPlayer = false;
    }
    public void OnMouseDown()
    {
        targetPlace.SetActive(true);
        ForwardSpeed = 0;
        lineRenderer.enabled = true;
        touchStartedonPlayer = true;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }
    private void OnMouseUp()
    {
        targetPlace.SetActive(false);
        touchStartedonPlayer = false;
        move = true;
    }
    private void OnBecameVisible()
    {
        Warning.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && timer > TimeForNextRay && touchStartedonPlayer)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject newWayPoint = new GameObject("WayPoint");
            newWayPoint.transform.position = new Vector2(mousePosition.x, mousePosition.y);
            newWayPoint.transform.rotation = Quaternion.Euler(0, 0, newWayPoint.transform.rotation.z);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);
            WayPoints.Add(newWayPoint);
            lineRenderer.positionCount = wayIndex + 1;
            lineRenderer.SetPosition(wayIndex, newWayPoint.transform.position);
            timer = 0;
            wayIndex++;
        }
        if (move)
        {
            lineRenderer.SetPosition(currentWaypoint, -Vector3.forward);
            transform.position = Vector2.MoveTowards(transform.position,
                WayPoints[currentWaypoint].transform.position, speed * Time.deltaTime);
            Vector3 direction = WayPoints[currentWaypoint].transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, -angle);
            if (transform.position == WayPoints[currentWaypoint].transform.position) 
            {
                currentWaypoint++;
            }
            if(currentWaypoint == WayPoints.Count)
            {
                foreach (var item in WayPoints)
                {
                    Destroy(item);
                }
                move = false;
                WayPoints.Clear();
                wayIndex = 1;
                currentWaypoint = 0;
                ForwardSpeed = .5f;
            }
        }
        else if (!move)
        {
            transform.TransformDirection(transform.position += transform.up * ForwardSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ship")
        {
            NearFieldArea.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ship")
        {
            NearFieldArea.SetActive(false);
        }
    }
    private IEnumerator warningTime()
    {
        yield return new WaitForSeconds(1);
        Warning.SetActive(true);
    }
}

