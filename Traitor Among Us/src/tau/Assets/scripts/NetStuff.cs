using UnityEngine;
using System.Collections;

public class NetStuff : Photon.MonoBehaviour {
    
    //private Vector3 correctPlayerPos = Vector3.zero; // We lerp towards this
    //private Quaternion correctPlayerRot = Quaternion.identity; // We lerp towards this

    protected PhotonView myPhotonView;

	public Animator myAnimator;

    void Awake()
    {
        myPhotonView = gameObject.GetComponent<PhotonView>();
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            /*
                        CharacterMotor myC = GetComponent<CharacterMotor>();
                        stream.SendNext((int)myC._characterState);*/
        }
        else
        {
            // Network player, receive data
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
            /*
                        CharacterMotor myC = GetComponent<CharacterMotor>();
                        myC._characterState = (CharacterState)stream.ReceiveNext();*/
        }
    }

    public void desativeNet()
    {
        desative();
        if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
        {
            myPhotonView.RPC("desative", PhotonTargets.Others);
        }
    }

    public void ativeNet()
    {
        ative();
        if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
        {
            myPhotonView.RPC("ative", PhotonTargets.Others);
        }

    }

    [RPC]
    public void desative()
    {
		enabled = false;
		if (collider != null){
        	collider.enabled = false;
		}
		if (rigidbody != null){
        	rigidbody.useGravity = false;
		}
		if (renderer != null){
        	renderer.enabled = false;
		}

		if (myAnimator != null)
		{
			myAnimator.SetBool("morra",true);
		}
    }

    [RPC]
    public void ative()
    {
		enabled = true;
		if (collider != null){
			collider.enabled = true;
		}
		if (rigidbody != null){
			rigidbody.useGravity = true;
		}
		if (renderer != null){
			renderer.enabled = true;
		}

		if (myAnimator != null)
		{
			myAnimator.SetBool("morra",false);
		}
    }

}
