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

    private bool grabbing;
    private bool onGround;
    private bool tryingToTunnel;
    private bool b;

    private GameObject grabbedObject;

    private Rigidbody rb;
    private QuantumManager qm;
    public void SetTryingToTunnel(bool value) { tryingToTunnel = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        qm = quantumManager.GetComponent<QuantumManager>();

        grabbing = false;
        onGround = true;
        tryingToTunnel = false;
        b = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!tryingToTunnel)
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
                        if (/*(hit.collider.gameObject.GetComponent<WallTunnelling>()*/b && qm.GetQuantum())
                        {
                            tryingToTunnel = true;
                        }
                        else if (hit.collider.gameObject.GetComponent<QuantumManager>())
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
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(0.0f, Input.GetAxis("Mouse X") * mouseSensX, 0.0f);
        cam.transform.Rotate(Input.GetAxis("Mouse Y") * mouseSensY, 0.0f, 0.0f);
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
