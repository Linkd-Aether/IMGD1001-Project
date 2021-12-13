using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBullet : MonoBehaviour
{
    string color;

    public GameObject orangePortal;
    public GameObject bluePortal;
    public GameObject node;

    private GameObject toPortal;

    public void Inst(string color, GameObject portal){
        this.color = color;
        toPortal = portal;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacles")) {
            if(color == "Orange") {
                FindObjectOfType<Pacman>().orangePortal = Instantiate(orangePortal, this.gameObject.transform.position, this.gameObject.transform.rotation);
            }
            if(color == "Blue"){
                GameObject thisPortal = Instantiate(bluePortal, this.gameObject.transform.position, this.gameObject.transform.rotation);
                toPortal.GetComponent<Passage>().connection = thisPortal.transform;
                thisPortal.GetComponent<Passage>().connection = toPortal.transform;
            }
            Instantiate(node, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
