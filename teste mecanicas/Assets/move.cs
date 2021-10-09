using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    public int choose = 0;
    public bool ativo = false;
    public bool findF = false;
    public GameObject vision;
    public GameObject alvo;
    public List<GameObject> list = new List<GameObject>();
    public float currentDist = 5000.0f;
    public int currentIndex = 0;
    void Start()
    {
        //choose = Random.Range(0, 4);
        //ativo = true;
        vision = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(ativo == true && findF == false)
        {
            if (choose == 0)
            {
                transform.position = transform.position + (transform.right * -1 * speed * Time.deltaTime);
            }
            else if (choose == 1)
            {
                transform.position = transform.position + (transform.right * speed * Time.deltaTime);
            }
            else if (choose == 2)
            {
                transform.position = transform.position + (transform.up * speed * Time.deltaTime);
            }
            else if (choose == 3)
            {
                transform.position = transform.position + (transform.up * -1 * speed * Time.deltaTime);
            }

        }
        else if (ativo == false && findF == false)
        {
            choose = Random.Range(0, 4);
            ativo = true;
            StartCoroutine(Reset());
        }
        else if(findF == true)
        {
            for (int i = 0; i < list.Count; i++)
            {
                float d = Vector2.Distance(list[i].transform.position, this.transform.position);
                if(d < currentDist)
                {
                    currentDist = d;
                    currentIndex = i;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, list[currentIndex].transform.position, speed * Time.deltaTime);
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);
        ativo = false;
    }


    public void GetRoute(GameObject obj)
    {
        //alvo = obj;
        if(FindInList(obj) == false)
        {
            list.Add(obj);
            findF = true;
        }

    }

    bool FindInList(GameObject obj)
    {
        bool r = false;
        if(list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i] == obj)
                {
                    r = true;
                    break;
                }

            }
        }
        return r;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(collision.collider.gameObject);
            list.RemoveAt(currentIndex);
            if(list.Count == 0)
            {
                findF = false;
                choose = Random.Range(0, 4);
                ativo = true;
                StartCoroutine(Reset());
            }
        }
    }
}
