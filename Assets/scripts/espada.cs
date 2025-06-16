using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
//using static System.Net.Mime.MediaTypeNames;

public class espada : MonoBehaviour
{
    bool inHand;
    bool isAttacking;
    bool ischarging;
    bool charged;

    int combo;
    int totalHits = 0;

    float lastclick;

    [SerializeField]
    GameObject swing;

    [SerializeField]
    GameObject victoriaPantalla; // Plano con imagen de victoria

    [SerializeField]
    GameObject enemigo; // Enemigo a ocultar al final

    void Start()
    {
        inHand = true;
        combo = 0;
        isAttacking = false;
        lastclick = 0f;

        if (victoriaPantalla != null)
            victoriaPantalla.SetActive(false);

        if (enemigo != null)
            enemigo.SetActive(true);
    }

    void Update()
    {
        lastclick += Time.deltaTime;

        if (Input.GetMouseButton(0) && inHand && !isAttacking)
        {
            isAttacking = true;
            combo++;
            totalHits++;
            lastclick = 0f;

            GetComponent<Animator>().SetInteger("combo", combo);

            if (swing != null)
            {
                GameObject effect = Instantiate(swing, transform.TransformPoint(new Vector3(0, 3f, 0)), transform.rotation, transform);
                Destroy(effect, 0.5f);
            }

            Debug.Log("Golpes realizados: " + totalHits);

            if (totalHits >= 9)
            {
                if (victoriaPantalla != null)
                    victoriaPantalla.SetActive(true);

                if (enemigo != null)
                    enemigo.SetActive(false); // Oculta el enemigo

                StartCoroutine(SalirJuegoConRetraso());
            }
        }

        if (combo > 0 && lastclick > 0.7f)
        {
            combo = 0;
            lastclick = 0f;
            GetComponent<Animator>().SetInteger("combo", combo);
        }

        if (isAttacking && lastclick > 0.7f)
        {
            isAttacking = false;
        }

        if (combo > 2)
        {
            combo = 0;
        }
    }

    IEnumerator SalirJuegoConRetraso()
    {
        // Opcional: ocultar visualmente la espada
        GetComponent<Renderer>().enabled = false;
        if (swing != null)
            swing.SetActive(false);

        yield return new WaitForSeconds(2f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void noAttacking()
    {
        isAttacking = false;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
