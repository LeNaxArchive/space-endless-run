using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject[] roadSections; // Array to hold different road section prefabs

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            // Check if the array is not empty
            if (roadSections.Length > 0)
            {
                // Select a random index from the array
                int randomIndex = Random.Range(0, roadSections.Length);
                // Instantiate the selected road section prefab at the specified position and rotation
                Instantiate(roadSections[randomIndex], new Vector3(0, 0, -57), Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No road sections assigned in the array.");
            }
        }
    }
}
