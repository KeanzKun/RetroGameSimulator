using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBladeObjectPooling : MonoBehaviour
{
    public const float BULLET_DELAY_MAX = 3.0f;
    static private List<LeafBladeObjectPooling> leafBlades;
    [SerializeField] private static float leafBladeSpeed = 100.0f;

    // Start is called before the first frame update
    void Awake()
    {
        if (leafBlades == null)
        {
            leafBlades = new List<LeafBladeObjectPooling>();
        }

        leafBlades.Add(this);

        //set all to inactive
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

static public LeafBladeObjectPooling SpawnLeafBlade(Vector3 location, Quaternion landing, Vector3 directionToPlayer)
{
    foreach (LeafBladeObjectPooling leafBlade in leafBlades)
    {
        if (leafBlade.gameObject.activeSelf == false)
        {
            leafBlade.transform.position = location;

            // Apply the rotation offset to the original rotation
            leafBlade.transform.rotation = landing * Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            leafBlade.GetComponent<Rigidbody>().velocity = directionToPlayer * leafBladeSpeed;

            leafBlade.gameObject.SetActive(true);

            leafBlade.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            // Hide the leaf blade after 3 seconds
            leafBlade.StartCoroutine(HideLeafBladeAfterDelay(leafBlade.gameObject));

            return leafBlade;
        }
    }

    return null;
}

private static IEnumerator HideLeafBladeAfterDelay(GameObject leafBladeObject)
{
    yield return new WaitForSeconds(3f);

    // Set the leaf blade object inactive after 3 seconds
    leafBladeObject.SetActive(false);
}

}
