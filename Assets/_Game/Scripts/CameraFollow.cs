using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Character player;
    [SerializeField] private float speed = 5f;
    public UnityEngine.Vector3 offset;
    public Vector3 offsetlevel => offset * player.Increaseswithlevel;
    public Transform Player => player.transform;
    private Quaternion targetRotation;

    void Start()
    {
        transform.position = Player.position + offset;
    }
    private void Oninit()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = UnityEngine.Vector3.Lerp(transform.position, Player.position + offsetlevel, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
    public void ChangeCameraMainMenu()
    {
        targetRotation = Quaternion.Euler(30, 0, 0);
        offset = new Vector3(0, 3f, -6.5f); 
    }
    public void ChangeCameraPlay()
    {
        targetRotation = Quaternion.Euler(60, 0, 0);
        offset = new Vector3(0, 14f, -12f);
    }
    public void ChangeCameraSkin()
    {
        targetRotation = Quaternion.Euler(19, 0, 0);
        offset = new Vector3(0, 1f, -6f);
    }
    public void ChangeColorPlayer()
    {
        targetRotation = Quaternion.Euler(8, -5 , 0);
        offset = new Vector3(0, 1f, -6f);
    }
}
