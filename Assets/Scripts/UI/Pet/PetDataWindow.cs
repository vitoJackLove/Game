using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.ViewModels;
using Loxodon.Framework.Views;
using SuperScrollView;
using UnityEngine;

/// <summary>
/// 宠物详细信息窗口
/// </summary>
public class PetDataWindow : Window
{
    /// <summary>
    /// 左侧宠物列表
    /// </summary>
    public LoopGridView loopGridView;

    /// <summary>
    /// 宠物信息
    /// </summary>
    private PetDataViewModel viewModel;
    
    protected override void OnCreate(IBundle bundle)
    {
        viewModel = bundle.Get<PetDataViewModel>(Constant.UiConstant.WINDOW_DATA);
        
        BindingSet<PetDataWindow, PetDataViewModel> bindingSet = this.CreateBindingSet(viewModel);

        
        bindingSet.Build();
        
        loopGridView.InitGridView(viewModel.Data.PetDataList.Count, OnGetItemByRowColumn);
    }
    
    LoopGridViewItem OnGetItemByRowColumn(LoopGridView gridView, int itemIndex,int row,int column)
    {
        if (itemIndex < 0 || itemIndex >= viewModel.Data.PetDataList.Count)
        {
            return null;
        }
        
        PetData petData = viewModel.Data.PetDataList[itemIndex];
        if (petData == null)
        {
            return null;
        }
      
        LoopGridViewItem item = gridView.NewListViewItem("PetItem");

        if (item != null)
        {
            PetItemWindow petItemWindow = item.GetComponent<PetItemWindow>();
        
            PetItemViewModel itemViewModel = new PetItemViewModel(petData);
        
            if (item.IsInitHandlerCalled == false)
            {
                item.IsInitHandlerCalled = true;
            
                petItemWindow.Init(itemViewModel);
            }

            petItemWindow.SetItemData(petData, itemIndex, row, column);
        }

        return item;
    }
}
