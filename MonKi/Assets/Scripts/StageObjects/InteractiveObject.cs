using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InteractiveObject : MonoBehaviour
{
    public delegate void OnActivation();
    public event OnActivation activate;
    public WeightClass weight = WeightClass.Light;
    public bool isHoldable = false;
    public Rigidbody rb;
    public PhotonView photonView;

    public bool hasMessage = false;
    public string message, messageForMental;

    Vector3 respawningPos, respawningRot ;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        photonView = gameObject.GetComponent<PhotonView>();
        if(rb != null) isHoldable = true;
        if(isHoldable) 
        {
            respawningPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            respawningRot = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Action(InteracterScript caller)
    {
        if(activate != null)
            photonView.RPC("CallAction",RpcTarget.All);
        else
            Debug.Log("Não existe função escrita no activate");
    }

    [PunRPC]
    public void CallAction()
    {
        activate();
    }

    [PunRPC]
    public void GettingGrabbed(int id, bool isHand)
    {
        GameObject holder = PhotonView.Find(id).gameObject;
        if(isHand)
        {
            rb.isKinematic = true;
            transform.position = holder.GetComponent<InteracterScript>().hand.position;
            transform.parent = holder.GetComponent<InteracterScript>().hand.parent;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), holder.GetComponent<Collider>(), true);
            holder.GetComponent<InteracterScript>().grabbed = rb;
            holder.GetComponent<InteracterScript>().isHolding = true;
        }
        else
        {
            rb.isKinematic = true;
            transform.position = holder.GetComponent<InteracterScript>().shouder.position;
            transform.parent = holder.GetComponent<InteracterScript>().shouder.parent;
            transform.rotation = Quaternion.identity;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), holder.GetComponent<Collider>(), true);
            holder.GetComponent<InteracterScript>().grabbed = rb;
            holder.GetComponent<InteracterScript>().isHolding = true;
        }
    }

    [PunRPC]
    public void GettingDropped(int id, bool isDropping, float force)
    {
        GameObject holder = PhotonView.Find(id).gameObject;
        if(isDropping)
        {
            transform.parent = null;
            rb.isKinematic = false;
            Physics.IgnoreCollision(holder.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), false);
            holder.GetComponent<InteracterScript>().isHolding = false;
        }
        else
        {
            transform.parent = null;
            rb.isKinematic = false;
            rb.AddForce(holder.GetComponent<InteracterScript>().personalCamera.transform.forward * force);
            Physics.IgnoreCollision(holder.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), false);
            holder.GetComponent<InteracterScript>().isHolding = false;
        }
    }
    [PunRPC]
    public void Levitating(Vector3 position, bool get)
    {
        if(get)
        {
            transform.position = position;
            rb.useGravity = false;
            rb.freezeRotation = true;
        }
        else
        {
            rb.useGravity = true;
            rb.freezeRotation = false;
        }
    }

    [PunRPC]
    public void SetPosition(bool isDeactivating, float[] position, float[] rotation, int caller)
    {
        if(isDeactivating)
        {
            isHoldable = false;
            rb.isKinematic = true;
            //transform.parent = PhotonView.Find(caller).transform;
        }
        transform.position = new Vector3(position[0], position[1], position[2]);
        transform.eulerAngles = new Vector3(rotation[0], rotation[1], rotation[2]);
    }

    [PunRPC]
    public void Realocate()
    {
        transform.position = respawningPos;
        transform.eulerAngles = respawningRot;
    }
}

[Flags]
public enum WeightClass
{
    Light = 0b_0000_0001,
    Moderate = 0b_0000_0010,
    Heavy = 0b_0000_0100,
    Monkey = 0b_0000_1000,
    General = Light | Moderate
}
