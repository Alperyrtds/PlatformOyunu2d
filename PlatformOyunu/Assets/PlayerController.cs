using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mySpeedx;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    private Rigidbody2D myBody;
    private Vector3 defaultLocalScale;
    public bool onGround;
    private bool canDoubleJump;
    [SerializeField] GameObject arrow;
    [SerializeField] bool attacked;
    [SerializeField] float currentAttackTimer;
    [SerializeField] float defaultAttackTimer;
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        attacked = false;
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("Horizontal"));
        mySpeedx = Input.GetAxis("Horizontal");
        myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedx));
        myBody.velocity = new Vector2(mySpeedx * speed, myBody.velocity.y);

        Debug.Log("Frame Mantığı");
        #region yüzü sağa sola döndürme

        
        if (mySpeedx>0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x,defaultLocalScale.y,defaultLocalScale.z);
        }
        else if (mySpeedx<0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion

        #region Zıplama
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround == true)
            {
                myBody.velocity = new Vector2(defaultLocalScale.x, 8f);
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump == true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, 5f);
                    canDoubleJump = false;
                }
            }
            
        }
        #endregion

        #region Player'In ok atmasının controlü
        if (Input.GetMouseButtonDown(0))
        {
            if (attacked == false)
            {
                attacked = true;
                Fire();
            }
            
        }

        #endregion
        if (attacked == true)
        {
            currentAttackTimer -= Time.deltaTime;
        }
        else
        {
            currentAttackTimer = defaultAttackTimer;
        }
        if (currentAttackTimer<0)
        {
            attacked = false;
        }
    }
    void Fire()
    {
        GameObject okumuz = Instantiate(arrow, transform.position, Quaternion.identity);
        okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);

        if (transform.localScale.x > 0)
        {
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(28f, 0f);
        }
        else
        {
            Vector3 okumuzScale = okumuz.transform.localScale;
            okumuz.transform.localScale = new Vector3(-okumuzScale.x, okumuzScale.y, okumuzScale.z);
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-28f, 0f);
        }
    }
}
