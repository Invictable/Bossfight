using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
  //  private Vector3 shootDir;

    public void Awake()
    {
        Invoke(nameof(TimeOut), 5);
    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.Log("launch");
      //  float moveSpeed = 10f;
      //  transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    void TimeOut()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
