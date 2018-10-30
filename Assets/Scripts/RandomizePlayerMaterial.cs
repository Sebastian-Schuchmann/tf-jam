using System.Collections.Generic;
using UnityEngine;

public class RandomizePlayerMaterial : MonoBehaviour
{

    public List<GameObject> players;

    // Use this for initialization
    void Awake()
    {
        //For better readability
        foreach (GameObject player in players)
        {
            setRandomMaterial(player);
        }
    }

    private void setRandomMaterial(GameObject player)
    {
        Material originalMaterial = player.GetComponent<PlayerController>().Body.GetComponent<Renderer>().material;
        Material material = new Material(originalMaterial)
        {
            color = Random.ColorHSV()
        };

        player.GetComponent<PlayerController>().Body.GetComponent<Renderer>().material = material;
    }

}
