using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraScript : MonoBehaviour
{
    private Vector3 Origin;
    private Vector3 Difference;
    private bool isDrag = false;
    public GameObject star;
    private bool isFixed = true;

    public float minSize = 0.1f;
    public float maxSize = 500f;
    public float sensitivity = 1f;

    void Start()
    {

    }
    void LateUpdate()
    {
        if (isFixed){
            Camera.main.transform.position = new Vector3(star.transform.position.x, star.transform.position.y, -10);
        }
        if ((Input.GetMouseButton(0)) && (!EventSystem.current.IsPointerOverGameObject()))
        {
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (isDrag == false)
            {
                isDrag = true;
                isFixed = false;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            isDrag = false;
        }
        if (isDrag == true)
        {
            Camera.main.transform.position = Origin - Difference;
        }
        
        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = new Vector3(star.transform.position.x, star.transform.position.y, -10);
            //Camera.main.orthographicSize = 15f;
            isFixed = true;
        }

        float size = Camera.main.orthographicSize;
        size += Input.GetAxis("Mouse ScrollWheel") * sensitivity * size;
        size = Mathf.Clamp(size, minSize, maxSize);
        Camera.main.orthographicSize = size;
    }
}