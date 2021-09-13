using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BaseComponent : ILifeCycle
{
    public Task OnEnter(object obj = null)
    {
        throw new System.NotImplementedException();
    }

    public void OnInit(object obj = null)
    {
        throw new System.NotImplementedException();
    }

    public void OnStart(object obj = null)
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public void OnFixUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public void OnLateUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public void OnDispose()
    {
        throw new System.NotImplementedException();
    }
}
