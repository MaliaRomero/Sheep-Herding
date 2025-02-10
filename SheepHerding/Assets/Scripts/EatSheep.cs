using UnityEngine;
using TMPro;

public class EatSheep : MonoBehaviour
{
    int counter = 0;
    public TextMeshProUGUI loseText;
    void OnCollisionEnter(Collision collision)
    {
        // Check if the object the wolf collided with is a sheep
        if (collision.gameObject.CompareTag("bird"))
        {   counter++;
            if (counter > 9){
                Debug.Log("No Sheep");
                loseText.gameObject.SetActive(true);

            }
            Debug.Log("Eaten");
            collision.gameObject.SetActive(false);
            
        }
    }

}