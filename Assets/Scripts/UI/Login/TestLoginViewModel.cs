using System.Data;
using Game;
using Loxodon.Framework.Commands;
using Loxodon.Framework.Interactivity;
using Loxodon.Framework.ViewModels;

public class TestLoginViewModel : ViewModelBase
{
    private InteractionRequest _cancelRequest;

    private InteractionRequest<StateRoomViewModel> _affirmRequest;
    
    private Account _account;
    
    private SimpleCommand command;

    /// <summary>
    /// 大厅（包含玩家所有信息）
    /// </summary>
    private StateRoomViewModel stateRoomViewModel;
    
    public TestLoginViewModel()
    {
        _account = new Account();

        _cancelRequest = new InteractionRequest();

        _affirmRequest = new InteractionRequest<StateRoomViewModel>();
        
        this.command = new SimpleCommand(() =>
        {
            //this.command.Enabled = true;
            
            _cancelRequest.Raise(() =>
            {
                Debuger.Log("取消");
            });
        });
        
        stateRoomViewModel = new StateRoomViewModel();
    }

    public Account _Account => _account;

    public void OnAffirmClick()
    {
        _affirmRequest.Raise(stateRoomViewModel);
    }
    
    //第二种绑定
    public ICommand Click => command;

    public IInteractionRequest CancelRequest => _cancelRequest;
    
    public IInteractionRequest AffirmRequest => _affirmRequest;
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
