﻿using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.ViewModels;
using Loxodon.Framework.Views;
using SuperScrollView;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 宠物详细信息窗口
/// </summary>
public class PetDataWindow : Window
{
    /// <summary>
    /// 宠物信息
    /// </summary>
    private PetDataViewModel viewModel;

    /// <summary>
    /// 左侧宠物列表
    /// </summary>
    public LoopGridView loopGridView;

    /// <summary>
    /// 展示属性面板
    /// </summary>
    public Button propertyBtn;

    /// <summary>
    /// 展示资质面板
    /// </summary>
    public Button aptitudeBtn;

    /// <summary>
    /// 属性面板
    /// </summary>
    public GameObject propertyPanel;

    /// <summary>
    /// 资质面板
    /// </summary>
    public GameObject aptitudePanel;
    
    protected override void OnCreate(IBundle bundle)
    {
        viewModel = bundle.Get<PetDataViewModel>(Constant.UiConstant.WINDOW_DATA);

        BindingSet<PetDataWindow, PetDataViewModel> bindingSet = this.CreateBindingSet(viewModel);

        bindingSet.Bind(this.propertyBtn).For(v => v.onClick).To(vm => vm.ShowPropertyPanel);
        
        bindingSet.Bind(this.aptitudeBtn).For(v => v.onClick).To(vm => vm.ShowAptitudePanel);
        
        bindingSet.Bind(this.propertyPanel).For(v => v.activeSelf).To(vm => vm.IsShowPetPropertyPanel);
        
        bindingSet.Bind(this.aptitudePanel).For(v => v.activeSelf).ToExpression(vm => vm.IsShowPetPropertyPanel == false);
        
        bindingSet.Build();

        loopGridView.InitGridView(viewModel.Data.PetDataList.Count, OnGetItemByRowColumn);
    }

    LoopGridViewItem OnGetItemByRowColumn(LoopGridView gridView, int itemIndex, int row, int column)
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

            PetItemViewModel itemViewModel = new PetItemViewModel(petData,viewModel);

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