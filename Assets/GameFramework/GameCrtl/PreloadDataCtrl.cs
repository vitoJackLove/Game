using System;
using System.Collections.Generic;
using GameFramework;
using Loxodon.Framework.Asynchronous;
using UnityEngine;

public class PreloadDataCtrl 
{
    private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();
        
        private AsyncResult<bool> loadDataAsyncResult;

        private int finishCount;
        
        public IAsyncResult<bool> LoadData()
        {
            this.loadDataAsyncResult = new AsyncResult<bool>();

            m_LoadedFlag.Clear();
            
            this.finishCount = 0;

            PreloadResources();
            
            return this.loadDataAsyncResult;
        }

        public void Close()
        {
            Resources.UnloadUnusedAssets();
            
            GC.Collect();
        }

        private void CheckLoadFinish()
        {
            this.finishCount++;
            
            IEnumerator<bool> iter = m_LoadedFlag.Values.GetEnumerator();
            
            float progress = finishCount * 1.0f / m_LoadedFlag.Values.Count;
            
            while (iter.MoveNext())
            {
                if (!iter.Current)
                {
                    return;
                }
            }

            iter.Dispose();
            
            this.loadDataAsyncResult.SetResult(true);
            
            this.Close();
        }


        private void PreloadResources()
        {
            // Preload data tables
            foreach (string dataTableName in ExcelGlobal.DataTableNames)
            {
                LoadDataTable(dataTableName);
            }
            
            //LoadConfig("GameConfig");
        }

        private async void LoadConfig(string configName)
        {
            m_LoadedFlag.Add(Utility.Text.Format("Config.{0}", configName), false);

            var asset = await GameEnter.Resources.LoadAssetAsync<TextAsset>(AssetUtility.GetConfigAsset(configName, LoadType.Text));

            Debuger.Log($"Load Config '{configName}' OK.");
            
            m_LoadedFlag[Utility.Text.Format("Config.{0}", configName)] = true;

            /*GameEnter.Base.GameConfig = JsonConvert.DeserializeObject<GameConfig>(asset.text);*/
                        
            this.CheckLoadFinish();
        }
        
        private void LoadDataTable(string dataTableName)
        {
            m_LoadedFlag.Add(Utility.Text.Format("DataTable.{0}", dataTableName), false);
          
            AsyncResult asyncResult = new AsyncResult();
            
            GameEnter.DataTable.LoadDataTable(dataTableName, LoadType.Bytes , asyncResult);
            
            asyncResult.Callbackable().OnCallback(ar =>
            {
                m_LoadedFlag[Utility.Text.Format("DataTable.{0}", dataTableName)] = true;
                
                Debuger.Log($"Load data table '{dataTableName}' OK.");

                this.CheckLoadFinish();
            });
        }
}
