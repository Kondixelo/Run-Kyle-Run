using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public List<GameObject> industryPrefabsList;
    public List<GameObject> poolPrefabsList;
    public List<GameObject> currentPrefabsList;

    private int newPrefabNumber;
    private int lastPrefabNumber;

    public int biome;

    private GameSettings gameSets;

    private float speed;
    private float baseSpeed;
    private float runSpeed;

    private float addSpeed;
    private bool runON;
    void Start()
    {
        SetRunStatus(false);
        gameSets = FindObjectOfType<GameSettings>();

        baseSpeed = gameSets.GetBaseSpeed();
        runSpeed = gameSets.GetRunSpeed();
        if (biome == 1)
        {
            currentPrefabsList = industryPrefabsList;
        }
        if (biome == 2)
        {
            currentPrefabsList = poolPrefabsList;
        }

    }


    private void Update()
    {
        if (runON)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                RunSpeed();
            }
            else
            {
                BaseSpeed();
            }
        }
        else
        {
            SetSpeedToZero();
        }

    }


    public float GetGroundSpeed()
    {
        return speed;
    }
    public void RunSpeed()
    {
        if (speed < runSpeed)
        {
            addSpeed += Time.deltaTime / 5;
            speed += addSpeed;
        }
        else
        {
            speed = runSpeed;
            addSpeed = 0;
        }
    }

    public void SetSpeedToZero()
    {
        speed = 0;
    }

    public void SetRunStatus(bool status)
    {
        runON = status;
    }

    public void BaseSpeed()
    {
        speed = baseSpeed;
        addSpeed = 0;
    }

    private void OnTriggerExit(Collider exitInfo)
    {
        if (biome == 1)
        {
            currentPrefabsList = industryPrefabsList;
        }
        if (biome == 2)
        {
            currentPrefabsList = poolPrefabsList;
        }
        Ground_Script ground = exitInfo.GetComponent<Ground_Script>();
        if (ground != null)
        {
            try
            {
                string prefabName = exitInfo.gameObject.name;
                if (prefabName.Length > 7)
                {
                    if (prefabName.Substring(prefabName.Length - 7, 7) == "(Clone)")
                    {
                        lastPrefabNumber = System.Int32.Parse(prefabName.Substring(prefabName.Length - 8, 1));
                    }
                    else
                    {
                        lastPrefabNumber = System.Int32.Parse(prefabName.Substring(prefabName.Length - 1, 1));
                    }
                }
                
                newPrefabNumber = RandomIntExcept(lastPrefabNumber);
            }
            catch (System.FormatException)
            {
                lastPrefabNumber = 0;
                newPrefabNumber = 0;
            }
            finally
            {
                RenderGround(newPrefabNumber);
            }
        }
    }


    public int RandomIntExcept(int except)
    {
        int result = Random.Range(0, currentPrefabsList.Count - 1);
        if (result >= except) result += 1;
        return result;
    }

    private void RenderGround(int prefabNumber)
    {
        Vector3 pos1 = gameObject.transform.position + (new Vector3(0f, 0f, (currentPrefabsList[prefabNumber].transform.localScale.z - gameObject.transform.localScale.z - 1f)));
        Instantiate(currentPrefabsList[prefabNumber], pos1, Quaternion.identity);
    }
}
