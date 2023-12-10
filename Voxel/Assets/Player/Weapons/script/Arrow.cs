using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bc;
    public GameObject trailRenderer;
    public float speed;

    public Transform aimPos;
    [SerializeField] LayerMask aimMask;
    [SerializeField] float aimSmoothSpeed = 20;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        bc.enabled = false;
    }
    public void FireArrow()
    {
        transform.parent = null;
        rb.isKinematic = false;
        trailRenderer.SetActive(true);
        rb.AddForce(transform.right * speed, ForceMode.Impulse);
        bc.enabled = true;
    }
    private void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(transform.position, transform.right, out RaycastHit hit, Mathf.Infinity, aimMask))
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.transform.parent = other.transform;
        rb.isKinematic = true;
        transform.position = transform.position - transform.right * 1;
        bc.enabled = false;
        if(other.transform.GetComponent<Rigidbody>() != null)
            other.transform.GetComponent<Rigidbody>().AddForce(this.transform.right * 200);

        Invoke("setTrailRenderer", 2);
    }
    private void setTrailRenderer()
    {
        trailRenderer.SetActive(false);
    }
}
