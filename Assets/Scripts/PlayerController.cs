using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float speed;

    public float mouseSensX;
    public float mouseSensY;

    public float grabRange;

    public float tunnelTimeMax;
    public float tunnelTimeMin;

    public GameObject cam;
    public GameObject quantumManager;
    public GameObject playerPrefab;

    private bool grabbing;
    private bool onGround;
    private bool isItSplit;
    private bool isTheCopy;

    private GameObject grabbedObject;

    private Rigidbody rb;
    private QuantumManager qm;

    public bool GetIsTheCopy() { return isTheCopy; }

    public void SetIsTheCopy(bool value) { isTheCopy = value; }

    public void Split() 
    {
        GetComponent<MeshRenderer>().enabled = true;
        if (!isItSplit)
        {
            isItSplit = true;
            if (!isTheCopy) cam.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
            else cam.GetComponent<Camera>().rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
        }
        else
        {
            if (isTheCopy) Destroy(gameObject);
            isItSplit = false;
            cam.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        qm = quantumManager.GetComponent<QuantumManager>();

        grabbing = false;
        onGround = true;
        isItSplit = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbing)
            {
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.transform.SetParent(null);
                grabbing = false;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, grabRange))
                {
                    if (hit.collider.gameObject.GetComponent<QuantumManager>())
                    {
                        hit.collider.gameObject.GetComponent<QuantumManager>().ToggleQuantum();
                    }
                    else if (hit.collider.gameObject.GetComponent<Rigidbody>())
                    {
                        grabbedObject = hit.collider.gameObject;
                        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                        grabbedObject.transform.SetParent(cam.transform);
                        grabbing = true;
                    }
                    else if (hit.collider.gameObject.GetComponent<PlayerSplitter>() && !isItSplit) 
                    {
                        GameObject copy = Instantiate(playerPrefab, hit.collider.gameObject.GetComponent<PlayerSplitter>().copySpawnPosition, transform.rotation);
                        copy.transform.GetChild(0).transform.rotation = cam.transform.rotation;
                        copy.transform.GetChild(0).GetComponent<AudioListener>().enabled = false;
                        copy.GetComponent<WallTunneling>().textCanvas = GetComponent<WallTunneling>().textCanvas;
                        copy.GetComponent<PlayerController>().quantumManager = quantumManager;
                        copy.GetComponent<PlayerController>().SetIsTheCopy(true);
                        copy.GetComponent<PlayerController>().Split();
                        Split();
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (cam.transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") > 89) cam.transform.localRotation = Quaternion.Euler(89.0f, 0.0f, 0.0f);
        if (cam.transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") < -89) cam.transform.localRotation = Quaternion.Euler(-89.0f, 0.0f, 0.0f);
        cam.transform.Rotate(Input.GetAxis("Mouse Y") * mouseSensY, 0.0f, 0.0f);
        transform.Rotate(0.0f, Input.GetAxis("Mouse X") * mouseSensX, 0.0f);
        rb.velocity = Vector3.Project(rb.velocity, Vector3.up) + Input.GetAxis("Horizontal") * speed * transform.right + Input.GetAxis("Vertical") * speed * transform.forward;
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            onGround = false;
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.2f))
        {
            onGround = true;
        }
    }
}
