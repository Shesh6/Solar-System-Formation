using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BodyScript : MonoBehaviour
{

    public Vector2 initialDir = new Vector2(0f, 0f);
    public float initialV = 1f;
    private Manager manager;
    public int myIndex;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        myIndex = manager.bodies.IndexOf(this.gameObject.GetComponent<Rigidbody2D>());
        MassToScale();
        this.GetComponent<CircleCollider2D>().radius = 0.5f;
        Rigidbody2D rb = this.gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        rb.velocity = initialDir * initialV;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.gameObject.GetComponent<Rigidbody2D>().velocity);
        if ((this.gameObject.transform.position.x - GameObject.Find("Star").transform.position.x > manager.maxDist) || (this.gameObject.transform.position.y - GameObject.Find("Star").transform.position.y > manager.maxDist)
            || (this.gameObject.transform.position.x - GameObject.Find("Star").transform.position.x < -manager.maxDist) || (this.gameObject.transform.position.y - GameObject.Find("Star").transform.position.y < -manager.maxDist))
        {
            manager.SendMessage("RemoveBody", this.gameObject.GetComponent<Rigidbody2D>());
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.tag == "Gravity") && (coll.gameObject != null))
        {
            Rigidbody2D collBody = coll.gameObject.GetComponent<Rigidbody2D>();
            if (this.gameObject.GetComponent<Rigidbody2D>().mass > collBody.mass)
            {
                //CrossSectionCalc(coll.gameObject);
                Merge(collBody);
            }
            else if (this.gameObject.GetComponent<Rigidbody2D>().mass == collBody.mass)
            {
                if (this.myIndex > coll.GetComponent<BodyScript>().myIndex)
                {
                    //CrossSectionCalc(coll.gameObject);
                    Merge(collBody);
                }
            }
        }
    }

    void CrossSectionCalc(Rigidbody2D body)
    {
        float rnd = Random.value;
        Debug.Log(rnd);
        if (rnd < manager.Pmerge)
        {
            //Debug.Log("Merge");
            Merge(body);
        }
        else //bounce
        {
            //Debug.Log("Bounce");
            return;
        }
    }

    void Merge(Rigidbody2D eaten)
    {
        manager.SendMessage("RemoveBody", eaten);
        this.gameObject.GetComponent<Rigidbody2D>().velocity = (((this.gameObject.GetComponent<Rigidbody2D>().velocity * this.gameObject.GetComponent<Rigidbody2D>().mass)
            + (eaten.velocity * eaten.mass))
            / (eaten.mass + this.gameObject.GetComponent<Rigidbody2D>().mass));
        this.gameObject.GetComponent<Rigidbody2D>().mass += eaten.mass;
        MassToScale();
        eaten = null;
    }

    void MassToScale()
    {
        float massToScale = ((2 * Mathf.Sqrt(this.gameObject.GetComponent<Rigidbody2D>().mass)) / (Mathf.Sqrt(Mathf.PI)));
        this.transform.localScale = new Vector3(massToScale, massToScale, 1);
    }
}
