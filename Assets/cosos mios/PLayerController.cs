using UnityEngine;
using TMPro; 

public class PlayerController : MonoBehaviour
{
    [Header("Parametros Fisicos")]
    public float velocidad = 12f;
    public float fuerzaSalto = 6f;

    [Header("Referencias de UI")]
    public TextMeshProUGUI textoContador;
    public GameObject panelVictoria;

    
    private int totalItems = 0;
    private int itemsRecolectados = 0;
    private Rigidbody rb;
    [SerializeField] private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }

        // 
        totalItems = GameObject.FindGameObjectsWithTag("Collectible").Length;

        // 
        ActualizarTextoUI();
    }

    void Update()
    {
        // Salto con impulso
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Movimiento con AddForce
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direccion = new Vector3(h, 0f, v);
        rb.AddForce(direccion * velocidad);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            // Suma 1 al recolectar
            itemsRecolectados++;

            // Actualiza el texto en pantalla inmediatamente
            ActualizarTextoUI();

            // Hace desaparecer el objeto
            Destroy(other.gameObject);

            // Condición de victoria
            if (itemsRecolectados >= totalItems && totalItems > 0)
            {
                if (panelVictoria != null)
                {
                    panelVictoria.SetActive(true);
                }
            }
        }
    }

    // Funcion que refresca el texto de la pantalla
    void ActualizarTextoUI()
    {
        if (textoContador != null)
        {
            textoContador.text = "Items: " + itemsRecolectados + " / " + totalItems;
        }
    }
  
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}