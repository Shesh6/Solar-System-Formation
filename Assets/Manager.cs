using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public float G = 1f;
    public float timeScale { get; set; }
    public float Pmerge = 0.5f;

    public float starM = 500f;
    public bool starFixed = false;
    public Color starC = Color.white;
    public GameObject BodyPF;
    public List<Rigidbody2D> bodies = new List<Rigidbody2D>();
    public Vector2 Rdist = new Vector2(20,35);
    public Vector2 Rmass = new Vector2 (3,5);
    public Vector2 Rvel = new Vector2(0,2);
    public float Rdev = 0.25f;
    public float Prev = 0.2f;
    public int amount = 20;
    public float maxDist;
    private Text timer;
    public float trailLen { get; set; }

	// Use this for initialization
	void OnEnable () {

        timer = GameObject.Find("Timer").GetComponent<Text>();

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Gravity");
        foreach (GameObject obj in objects){
            bodies.Add(obj.GetComponent<Rigidbody2D>());
        }
        //Debug.Log(bodies.Count);

        GenerateStar();


        for (int i = 0; i < amount; i++ )
            GenerateBody(new Vector2(0, 0));

        timeScale = 1f;
        maxDist = Rdist.y*2;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        foreach (Rigidbody2D i in bodies)
        {
            float imass = i.mass;
            foreach (Rigidbody2D j in bodies)
            {
                if (i != j){
                    Vector2 dir = new Vector2(j.gameObject.transform.position.x - i.gameObject.transform.position.x,
                                              j.gameObject.transform.position.y - i.gameObject.transform.position.y);
                    float jmass = j.mass;
                    i.AddForce(G * imass * jmass * dir.normalized / dir.sqrMagnitude);
                }
            }
        }
        if (starFixed)
        {
            bodies[0].velocity = Vector2.zero;
        }
  
        Time.timeScale = Mathf.Clamp(timeScale,0.01f,100);
        timer.text = Time.time.ToString();
	}
    void RemoveBody(Rigidbody2D body)
    {
        bodies.Remove(body);
        Destroy(body.gameObject);
    }
    void GenerateBody(Vector2 starPos)
    {
        Vector2 angle = Random.insideUnitCircle;
        Vector2 pos = starPos + angle * Random.Range(Rdist.x, Rdist.y);
        GameObject body = (GameObject)Instantiate(BodyPF, pos, Quaternion.identity);
        bodies.Add(body.GetComponent<Rigidbody2D>());
        body.GetComponent<Rigidbody2D>().mass = Random.Range(Rmass.x, Rmass.y);
        body.GetComponent<BodyScript>().SendMessage("MassToScale");
        float binaryDir = Random.value;
        if (binaryDir < Prev) {
            binaryDir = -1;
        }
        else {
            binaryDir = 1;
        }
        Vector2 dir = new Vector2(-pos.y, pos.x) / Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y) * Random.Range(Rvel.x, Rvel.y) * binaryDir;
        dir = Quaternion.Euler(0, 0, Random.Range(-Rdev, Rdev)) * dir;
        body.gameObject.GetComponent<BodyScript>().initialDir = dir;
        body.transform.parent = GameObject.Find("Spawns").transform;
    }
    void GenerateStar()
    {
        GameObject star = (GameObject)Instantiate(BodyPF, Vector3.zero, Quaternion.identity);
        bodies.Add(star.GetComponent<Rigidbody2D>());
        star.name = "Star";
        star.GetComponent<Rigidbody2D>().mass = starM;
        star.GetComponent<BodyScript>().SendMessage("MassToScale");
        //star.GetComponent<Renderer>().material.color = starC;
        star.GetComponent<TrailRenderer>().enabled = false;
        Camera.main.GetComponent<CameraScript>().star = star;
        star.transform.parent = GameObject.Find("Spawns").transform;
    }
    public void SetTrailLength()
    {
        foreach (Rigidbody2D b in bodies)
        {
            if (b.gameObject.GetComponent<TrailRenderer>() != null)
            {
                b.gameObject.GetComponent<TrailRenderer>().time = trailLen;
            }
        }
    }
    public void Terminate()
    {
        int tot = bodies.Count;
        for (int i = 0; i < tot; i++ )
        {
            RemoveBody(bodies[0]);
        }
        timer.text = "0";
        Time.timeScale = 0f;
    }
}

