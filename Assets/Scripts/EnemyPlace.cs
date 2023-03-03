using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlace : MonoBehaviour
{
    [SerializeField] private GameObject failPanel;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ship")
        {
            collision.gameObject.GetComponent<DrawingPath>().ForwardSpeed = 0;
            collision.gameObject.GetComponent<DrawingPath>().enabled = false;
            collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);
            StartCoroutine(time(2));
        }
    }
    IEnumerator time(float time)
    {
        yield return new WaitForSeconds(time);
        failPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
