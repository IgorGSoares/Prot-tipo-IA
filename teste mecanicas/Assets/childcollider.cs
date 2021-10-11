using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childcollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Orb")
        {
            //this.collider2D.attachedRigidbody.SendMessage("OnCollisionEnter2D", collision);
            this.transform.parent.GetComponent<move>().GetRoute(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Bot")
        {
            //this.collider2D.attachedRigidbody.SendMessage("OnCollisionEnter2D", collision);
            if(this.transform.parent.transform.position != collision.gameObject.transform.position)
            {
                this.transform.parent.GetComponent<move>().Flee(collision.gameObject);
                Debug.Log(collision.gameObject.name);
            }
        }
    }
}
