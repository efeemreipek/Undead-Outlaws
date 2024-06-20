using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private const float DISAPPER_TIMER_MAX = 1f;

    [SerializeField] private int regularFontSize = 18;
    [SerializeField] private int criticalHitFontSize = 24;
    [SerializeField] private Color regularColor;
    [SerializeField] private Color criticalHitColor;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float increaseSpeedAmount = 1f;
    [SerializeField] private float decreaseSpeedAmount = 1f;
    [SerializeField] private float disappearSpeed = 3f;

    private static int sortingOrder = 0;

    private TextMeshPro textMesh;
    private float disapperTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public static DamagePopUp Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {
        Transform damagePopUpTransform = Instantiate(GameAssets.Instance.damagePopupPrefab, position, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageAmount, isCriticalHit);

        return damagePopUp;
    }


    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            textMesh.fontSize = regularFontSize;
            textColor = Color.yellow;
        }
        else
        {
            textMesh.fontSize = criticalHitFontSize;
            textColor = Color.red;
        }
        textMesh.color = textColor;
        disapperTimer = DISAPPER_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(0.7f, 1f) * moveSpeed;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if(disapperTimer > DISAPPER_TIMER_MAX * 0.5f)
        {
            //First half of popup lifetime
            transform.localScale += Vector3.one * increaseSpeedAmount * Time.deltaTime;
        }
        else
        {
            //Second half of popup lifetime
            transform.localScale -= Vector3.one * decreaseSpeedAmount * Time.deltaTime;
            if(transform.localScale.x < 0)
            {
                transform.localScale = Vector3.zero;
            }
        }
        disapperTimer -= Time.deltaTime;
        if (disapperTimer < 0f)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
