using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SphereHandler : MonoBehaviour
{
    public Transform player;

    [SerializeField] private Color colorAt0;
    [SerializeField] private Color colorAt1;

    private Material material;
    private ParticleSystem pS;

    private void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;

        pS = GetComponentInChildren<ParticleSystem>();
    }

    [SerializeField] private float playIfProximityOver = 0.8f;
    [SerializeField] private float stopIfProximityUnder = 1;

    private float minDistance = 1.7f;
    private float maxDistance = 10;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        float distance01 = (distance - minDistance) / (maxDistance - minDistance);
        float proximity01 = 1 - distance01;

        // Debug.Log($"Sphere is {distance} units from player {distance01}");

        // Material color lerp
        Color newColor = Color.Lerp(colorAt0, colorAt1, proximity01);
        material.color = newColor;

        // VFX On-off
        Debug.Log($"Proximity is {proximity01}");

        if (!pS.isPlaying && proximity01 > playIfProximityOver)
        {
            //Debug.Log("PLAY!");
            pS.gameObject.SetActive(true);
            pS.Play();
        }
        else if (pS.isPlaying && proximity01 <= stopIfProximityUnder) // if (proximity01 > stopIfProximityOver)
        {
            //Debug.Log("STOP!");
            pS.Stop();
            pS.gameObject.SetActive(false);
        }

    }
}
