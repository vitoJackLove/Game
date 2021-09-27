using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.ViewModels;
using UnityEngine;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Interactivity;

public class TestStartupViewModel : ViewModelBase
{
    //VM 通知 V 这里可以向 V 传参
    private InteractionRequest<TestLoginViewModel> loginRequest;

    private Progress _progress;
    
    public TestStartupViewModel()
    {
        _progress = new Progress();
        loginRequest = new InteractionRequest<TestLoginViewModel>();
    }

    public void OnButtonClick()
    {
        loginRequest.Raise(null);
    }
    
    public async void Zip()
    {
        _progress.Enable = true;
        
        float tempValue = 0;

        _progress.Progressbar = tempValue;

        while (tempValue < 1)
        {
            tempValue += 0.01f;
            _progress.Progressbar = tempValue;
            await new WaitForSecondsRealtime(0.02f);
        }

        _progress.Enable = false;

        OnButtonClick();
    }

    public Progress Progress => _progress;

    public IInteractionRequest LoginRequest => loginRequest;
}

public class Progress : ModelBase
{
    private float progress;
    private string tip;
    private bool enable;
 public bool Enable {
        get => this.enable;
        set => this.Set<bool> (ref this.enable, value, "Enable");
    }
   

    public float Progressbar {
        get => this.progress;
        set => this.Set<float> (ref this.progress, value, "Progressbar");
    }

    public string Tip {
        get => this.tip;
        set => this.Set<string> (ref this.tip, value, "Tip");
    }
}
