using UnityEngine;

public class espada : MonoBehaviour
{
    bool inHand;
    bool isAttacking;
    bool ischarging;
    bool charged;

    int combo;

    float lastclick;

    [SerializeField]
    GameObject swing;

    void Start()
    {
        inHand = true;
        combo = 0;
        isAttacking = false;
        lastclick = 0f;
    }

    void Update()
    {
        lastclick += Time.deltaTime;

        if (Input.GetMouseButton(0) && inHand && !isAttacking)
        {
            isAttacking = true;
            combo++;
            lastclick = 0f; 
            GetComponent<Animator>().SetInteger("combo", combo);

            if (swing != null)
            {
                GameObject effect = Instantiate(swing, transform.TransformPoint(new Vector3(0, 3f, 0)), transform.rotation, transform);

                Destroy(effect, 0.5f);
            }
        }

        if (combo > 0 && lastclick > 0.7f)
        {
            combo = 0;
            lastclick = 0f;
            GetComponent<Animator>().SetInteger("combo", combo);
            
        }
        //failsafe
        if (isAttacking && lastclick > 0.7f)
        {
            isAttacking = false;
        }

        if (combo > 2)
        {
            combo = 0;
        }

    }

    public void noAttacking()
    {
        isAttacking = false;
    }
}
