using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeaconController : MonoBehaviour
{
    [SerializeField] private HealthManager        healthManager;
    [SerializeField] private float                beaconPowerCooldown = 10.0f;
    [SerializeField] private BeaconPowerAnimation beaconPowerAnimation;

    [SerializeField] private Image beaconPowerImage;

    private float _beaconPowerTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        healthManager.InitializeHealthBar(healthManager.initialHealth, "Beacon", OnDeath);
    }

    // Update is called once per frame
    private void Update()
    {
        _beaconPowerTimer += Time.deltaTime;
        _beaconPowerTimer = Mathf.Clamp(_beaconPowerTimer, 0.0f, beaconPowerCooldown);
        UpdateBeaconPowerImage();
        
        if (_beaconPowerTimer >= beaconPowerCooldown)
        {
            beaconPowerImage.color = Color.green;
        }
    }

    private static void OnDeath()
    {
        GameManager.SetCursor(true);
        SceneManager.LoadScene("GameOver");
    }

    private void BeaconPower()
    {
        healthManager.TakeDamage(10);
        beaconPowerImage.color = Color.white;

        beaconPowerAnimation.StartAnimation();
    }
    
    private void UpdateBeaconPowerImage()
    {
        // beaconPowerImage.transform.localScale = new Vector3(_beaconPowerTimer / beaconPowerCooldown, 1, 1);
        beaconPowerImage.fillAmount = _beaconPowerTimer / beaconPowerCooldown;
    }
    
    public void OnBeaconPower(InputAction.CallbackContext _context)
    {
        if (!_context.performed || !(_beaconPowerTimer >= beaconPowerCooldown)) return;
        
        BeaconPower();
        _beaconPowerTimer = 0.0f;
    }
}