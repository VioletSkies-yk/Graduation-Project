using Assets.Scripts.GamePlay.GameLogic;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using EzySlice;

public class Cuttable : Interactable
{
    public Material slicedMaterial; // 切割后的材质

    public override void OnFocus()
    {
        //throw new NotImplementedException();
    }

    public override void OnInteract()
    {
        Vector3 direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        EzySlice.Plane cuttingPlane = new EzySlice.Plane(direction, this.transform.position);

        // 对物体进行切割并生成新的物体
        GameObject[] pieces = this.gameObject.SliceInstantiate(cuttingPlane);

        // 遍历新生成的物体，并添加一些效果
        foreach (GameObject piece in pieces)
        {
            piece.AddComponent<MeshCollider>().convex=true;
            Rigidbody rb = piece.AddComponent<Rigidbody>();
            rb.AddExplosionForce(10f, this.transform.position, 5f, 1f, ForceMode.Impulse);
            piece.GetComponent<MeshRenderer>().material = slicedMaterial;
        }

        // 销毁原来的物体
        Destroy(this.gameObject);
    }

    public override void OnLoseFocus()
    {
        //throw new NotImplementedException();
    }

}