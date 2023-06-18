using UnityEngine;
using UnityEngine.Events;

public class QuestionAnswerer : MonoBehaviour
{
    [SerializeField] private UnityEvent onShootTrigger;
    [SerializeField] private bool triggered = false;
    
    public void SetTriggered(bool trigger)
    {
        triggered = trigger;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Bullet") || triggered) return;
        triggered = true;
        
        onShootTrigger.Invoke();
        DeleteAllBullets();
    }

    private void DeleteAllBullets()
    {
        // Find all objects of type Bullet in the scene
        ShipBullet[] bullets = FindObjectsOfType<ShipBullet>();

        // Loop through the list of bullets and do something with them
        foreach (ShipBullet bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
}
