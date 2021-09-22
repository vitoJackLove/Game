using System.Data;
using Game;
using Loxodon.Framework.Commands;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.ViewModels;

public class TestLoginViewModel : ViewModelBase
{
    private InteractionRequest _cancelRequest;
    
    private Account _account;
    
    private SimpleCommand command;
    
    public TestLoginViewModel()
    {
        _account = new Account();

        _cancelRequest = new InteractionRequest();
        
        this.command = new SimpleCommand(() =>
        {
            //this.command.Enabled = true;
            
            _cancelRequest.Raise(() =>
            {
                Debuger.Log("取消");
            });
        });
    }

    public Account _Account => _account;

    public void OnAffirmClick()
    {
        /*DRBuffData data =  GameEnter.DataTable.GetDataRow<DRBuffData>(310101);
        Debuger.Log(data.Text);*/
        _cancelRequest.Raise();
    }
    
    //第二种绑定
    public ICommand Click => command;

    public IInteractionRequest CancelRequest => _cancelRequest;
}

public class Account : ModelBase
{
    private string _name;

    private string _possWord;
    
    public string Name {
        get => this._name;
        set => this.Set<string> (ref this._name, value, "Name");
    }
    
    public string PossWord {
        get => this._possWord;
        set => this.Set<string> (ref this._possWord, value, "PossWord");
    }
}
