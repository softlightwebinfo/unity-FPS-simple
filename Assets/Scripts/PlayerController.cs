using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 5.0f;
    public float currentMoveSpeed;
    public float rotateSpeed = 60f;
    private float hInput, vInput;
    private Rigidbody _rb;
    public float distanceToGround = 0.10f;
    public LayerMask groundLayer;
    private CapsuleCollider _col;
    public GameObject bullet;
    public Transform shootPoint;
    public float bulletSpeed = 100.0f;
    private GameManager gameManager;

    void Start()
    {
        this._rb = GetComponent<Rigidbody>();
        this.currentMoveSpeed = moveSpeed;
        this._col = GetComponent<CapsuleCollider>();
        this.gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        if (Input.GetAxis("Fire1") > 0.5f)
        {
            this.currentMoveSpeed = moveSpeed * 2;
        }
        else
        {
            this.currentMoveSpeed = moveSpeed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(this.bullet, this.shootPoint.position, shootPoint.rotation) as GameObject;
            Rigidbody bulletRD = newBullet.GetComponent<Rigidbody>();
            bulletRD.velocity = this.shootPoint.forward * this.bulletSpeed;
        }

        if (this.IsOnTheGround() && Input.GetKeyDown(KeyCode.Space))
        {
            this._rb.AddForce(Vector3.up * this.jumpSpeed, ForceMode.Impulse);
        }

        this.hInput = Input.GetAxis("Horizontal") * this.rotateSpeed;
        this.vInput = Input.GetAxis("Vertical") * this.currentMoveSpeed;
        //this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        //this.transform.Rotate(hInput * Time.deltaTime * Vector3.up);        
    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.deltaTime);

        this._rb.MovePosition(this.transform.position + this.transform.forward * vInput * Time.fixedDeltaTime);
        this._rb.MoveRotation(_rb.rotation * angleRot);
    }
    /// <summary>
    /// Comprueba si el personaje esta tocando el suelo
    /// </summary>
    /// <returns><c>true</c>, si esta tocando el suelo, <c>false</c> si no.</returns>
    bool IsOnTheGround()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        return Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.playerHP -= Random.Range(0.0f, 10.0f);
        }
    }
}
