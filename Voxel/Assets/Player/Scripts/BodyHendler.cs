using UnityEngine;

public class BodyHendler : MonoBehaviour
{
    public Cut cut;

    private Animator animator;

    public GameObject SwordHand;
    public GameObject BowHand;
    public GameObject HammerHand;
    public GameObject Swordback;
    public GameObject Bowback;
    public GameObject Hammerback;

    public GameObject BowrLeftHand;
    private int NumberWeapon = 1;

    public GameObject Arrow;
    public Transform SpawnArrow;
    private GameObject SpawningArrow;

    public GameObject ArrowTarget;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if(NumberWeapon == 1)
        {
            SwordHand.SetActive(true);
            BowHand.SetActive(false);
            HammerHand.SetActive(false);

            Swordback.SetActive(false);
            Bowback.SetActive(true);
            Hammerback.SetActive(true);
        }
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "idle" && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "HoldBow") return;

        if (NumberWeapon == 1 || NumberWeapon == 3)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.D) == false)
            {
                cut.cut = true;
                animator.Play("attack");
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.A) == true)
            {
                cut.cut = true;
                animator.Play("left");
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.D) == true)
            {
                cut.cut = true;
                animator.Play("right");
            }
        }
        else if(NumberWeapon == 2)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.Play("BowAttack");
                animator.SetBool("BowFire", true);
            }
            else if (Input.GetKey(KeyCode.Mouse0) == false)
            {
                animator.SetBool("BowFire", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(NumberWeapon == 2 || NumberWeapon == 3)
            {
                if (BowrLeftHand.activeSelf == true)
                {
                    BowHand.SetActive(true);
                    BowrLeftHand.SetActive(false);
                }
                animator.Play("chanche");
                NumberWeapon = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (NumberWeapon == 1 || NumberWeapon == 3)
            {
                animator.Play("chanche");
                NumberWeapon = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (NumberWeapon == 1 || NumberWeapon == 2)
            {
                if (BowrLeftHand.activeSelf == true)
                {
                    BowHand.SetActive(true);
                    BowrLeftHand.SetActive(false);
                }
                animator.Play("chanche");
                NumberWeapon = 3;
            }
        }
    }
    public void SetCutfalse()
    {
        cut.cut = false;
    }
    public void SetCuttrue()
    {
        cut.cut = true;
    }
    public void SetChanche()
    {
        if (NumberWeapon == 1)
        {
            SwordHand.SetActive(true);
            BowHand.SetActive(false);
            HammerHand.SetActive(false);

            Swordback.SetActive(false);
            Bowback.SetActive(true);
            Hammerback.SetActive(true);
        }
        else if(NumberWeapon == 2)
        {
            SwordHand.SetActive(false);
            BowHand.SetActive(true);
            HammerHand.SetActive(false);

            Swordback.SetActive(true);
            Bowback.SetActive(false);
            Hammerback.SetActive(true);
        }
        else if(NumberWeapon == 3)
        {
            SwordHand.SetActive(false);
            BowHand.SetActive(false);
            HammerHand.SetActive(true);

            Swordback.SetActive(true);
            Bowback.SetActive(true);
            Hammerback.SetActive(false);
        }
    }

    public void SetchancheHendBow()
    {
        if(BowHand.activeSelf == true)
        {
            BowHand.SetActive(false);
            BowrLeftHand.SetActive(true);
        }
    }
    
    public void SpuwnArrow()
    {
        SpawningArrow = Instantiate(Arrow, SpawnArrow.position, SpawnArrow.rotation, BowrLeftHand.transform);
        ArrowTarget.SetActive(true);
    }
    public void FireArrow()
    {
        if(SpawningArrow != null)
        {
            SpawningArrow.GetComponent<Arrow>().FireArrow();
            SpawningArrow = null;
        }
        ArrowTarget.SetActive(false);
    }
}
