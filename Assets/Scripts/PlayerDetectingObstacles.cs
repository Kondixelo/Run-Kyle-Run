using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectingObstacles : MonoBehaviour
{
    public GameObject playerObject;
    private PlayerHealth playerHP;

    public GameSettings gameSets;

    public float zpow;
    private PlayerMovement playerMov;
    private void Start()
    {
        playerMov = gameObject.GetComponent<PlayerMovement>();
        playerHP = playerObject.GetComponent<PlayerHealth>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var damage = gameSets.GetDamge();
        // 1 0 0 prawo X
        // -1 0 0 lewo X

        // 0 -1 0 dol  Y
        // 0  1 0 gora Y

        // 0 0 1 przod Z
        // 0 0 -1 tyl  Z

        float vecX = hit.moveDirection.x;
        float vecY = hit.moveDirection.y;
        float vecZ = hit.moveDirection.z;

        //Debug.Log(vecX + " ###### " + vecY + " ###### " + vecZ);
        //Debug.Log(vecX);
        Debug.Log(" X " + vecX);
        Debug.Log(" Y " + vecY);
        Debug.Log(" Z " + vecZ);
        if (hit.moveDirection.z != 0)
        {
            zpow = hit.moveDirection.z;
        }

        if (vecX > 0.9)
        {
            playerMov.MoveLeft();
            playerHP.TakeDamage(damage);
        }
        if (vecX < -0.9)
        {
            playerMov.MoveRight();
            playerHP.TakeDamage(damage);
        }

        if (hit.collider.CompareTag("Obstacle"))
        {
            if(hit.moveDirection.z > 0.7)
            {
                Destroy(hit.collider.gameObject);
                playerHP.TakeDamage(damage);
            }
        }
        
        if (hit.collider.CompareTag("StrongObstacle"))
        {
            //Debug.Log("DIR: " + hit.moveDirection.z);
            //Debug.Log("Length " + hit.moveLength);
            if (hit.moveDirection.z > 0.7)
            {
                playerHP.dead = true;
                Debug.Log("------==============DEAD==================------");
            }
        }
        
    }
}
