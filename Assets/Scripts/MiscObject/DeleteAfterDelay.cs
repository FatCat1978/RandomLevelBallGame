using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterDelay : MonoBehaviour
{
    public bool InitiallyDelete; //do we delete as soon as possible??
    public long SecondsToDelete; //how long??
    void Start()
    {
        if (InitiallyDelete)
        {
            delete_this();
        }
    }

    public void delete_this()
    {
        Invoke("actual_delete", SecondsToDelete);
    }

    private void actual_delete()
    {
        Destroy(this.gameObject);
    }
}
