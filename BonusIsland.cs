using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusIsland : MonoBehaviour
{
    [SerializeField] int bonusValue;
    public Vector3 playerEndPos;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = playerEndPos;

            GameManager.Instance.Congratulation(GameManager.Instance.playerPlace, bonusValue);
        }
    }

    private void Start()
    {
        playerEndPos = PlayerPosition(this.transform);
    }
    public Vector3 PlayerPosition(Transform thisTransform)
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
         y += transform.localScale.y / 2;
        Vector3 IslandMiddlePointPosition = new Vector3(x, y, z);
        return IslandMiddlePointPosition;
    }
}
