using UnityEngine;

public class lifeHandler : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] string trigger;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == trigger)
        {
            if (life == 1)
            {
                PlayerPrefs.SetInt("count", PlayerPrefs.GetInt("count") + 1);
                PlayerPrefs.Save();
                Destroy(this.gameObject);
            }

            life--;
        }
    }
}
