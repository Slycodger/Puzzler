using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private float mouseX;
    private float mousey;
    private float horizontal;
    public float VerticalArmSpeed;
    public GameObject body;
    public GameObject Laser;
    private GameObject SelectedObject;
    private GameObject SavedObject;
    public GameObject BlockPosition;
    public Rigidbody rb;
    private RaycastHit Ray;
    public List<GameObject> Sprites;
    public bool Off;
    // Start is called before the first frame update

    private void Start()
    {
    }    

    // Update is called once per frame
    void Update()
    {
        if (!Off)
        {
            Cursor.visible = false;

            mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * speed;
            mousey = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;
            horizontal -= mousey;

            horizontal = Mathf.Clamp(horizontal, -90, 90);

            body.transform.Rotate(Vector3.up * mouseX);
            transform.localRotation = Quaternion.Euler(horizontal, 0, 0);

            rb.AddForce(body.transform.forward * Time.deltaTime * speed * Input.GetAxis("Vertical"));
            rb.AddForce(body.transform.right * Time.deltaTime * speed * Input.GetAxis("Horizontal"));

            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(0, 1, 0);
            }

            Laser.transform.Rotate(Vector3.right * (0.5f * Input.GetAxis("Mouse ScrollWheel")));

            if (Input.GetKey(KeyCode.Q))
            {
                if (Physics.Raycast(Laser.transform.position, Laser.transform.forward, out Ray, 5000, ~10))
                {
                    if (Ray.collider.CompareTag("Prop"))
                    {
                        SelectedObject = Ray.collider.gameObject;
                    }
                }
            }

            if (Input.GetMouseButton(1))
            {
                if (SelectedObject != null)
                {
                    SelectedObject.transform.position = BlockPosition.transform.position;
                    SelectedObject.transform.rotation = Laser.transform.rotation;
                    SelectedObject.layer = 10;
                    Laser.layer = 10;
                }
            }
            GameObject[] Sprite = GameObject.FindGameObjectsWithTag("Sprite");
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (SelectedObject != null)
                {
                    if (Sprite.Length >= 2)
                    {
                        foreach (GameObject Sp in Sprite)
                        {
                            Destroy(Sp);
                        }
                        Instantiate(Sprites[SelectedObject.GetComponent<Proplets>().ID]);
                        Instantiate(Sprites[SavedObject.GetComponent<Proplets>().ID], new Vector3(0.1567f, 0, 0), Sprites[SavedObject.GetComponent<Proplets>().ID].transform.rotation);
                    }
                    if (Sprite.Length <= 0)
                    {
                        Instantiate(Sprites[SelectedObject.GetComponent<Proplets>().ID]);
                    }
                }
            }
            GameObject[] Copy = GameObject.FindGameObjectsWithTag("Sprite");
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (SavedObject != null)
                    {
                        if (Copy.Length >= 2)
                        {
                            foreach (GameObject Sp in Copy)
                            {
                                Destroy(Sp);
                            }
                            Instantiate(Sprites[SelectedObject.GetComponent<Proplets>().ID]);
                            Instantiate(Sprites[SavedObject.GetComponent<Proplets>().ID], new Vector3(0.1567f, 0, 0), Sprites[SavedObject.GetComponent<Proplets>().ID].transform.rotation);
                        }
                        if (Copy.Length <= 1)
                        {
                            Instantiate(Sprites[SavedObject.GetComponent<Proplets>().ID], new Vector3(0.1567f, 0, 0), Sprites[SavedObject.GetComponent<Proplets>().ID].transform.rotation);
                        }
                    }
                }
            }
        }


        if (SelectedObject != null)
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    SavedObject = SelectedObject;
                }
                if(SavedObject != null)
                {
                    if (Input.GetKeyDown(KeyCode.V))
                    {
                        Instantiate(SavedObject, BlockPosition.transform.position, BlockPosition.transform.rotation);
                    }
                }
            }
        }

    }

    private void LateUpdate()
    {
        Laser.layer = 1;
    }
}
