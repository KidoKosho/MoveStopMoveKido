using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SightCharacter : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] float Wight;
    [SerializeField] private LineRenderer lineRenderer;
    public int segments = 50; 
    private float radius = 5f;
    public Color color = Color.red;
    public float Radius => radius * character.Increaseswithlevel;
    void Start()
    {
        lineRenderer.positionCount = segments+1;
        lineRenderer.startWidth = Wight;
        lineRenderer.endWidth =  Wight ;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        CreateCircle();
        gameObject.SetActive(true);
    }
    void CreateCircle()
    {
        float angleStep = 2 * Mathf.PI / segments;

        for (int i = 0; i <=segments; i++)
        {
            float x = Mathf.Cos(i * angleStep) * Radius;
            float z = Mathf.Sin(i * angleStep) * Radius;

            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(Cache.Character(other)!= null )character.AddCharacter(Cache.Character(other));
    }
    private void OnTriggerExit(Collider other)
    {
        character.RemoveCharacter(Cache.Character(other));
    }
}