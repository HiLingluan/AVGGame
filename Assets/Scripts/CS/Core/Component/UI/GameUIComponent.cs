//using GameFramework;
//using GameFramework.ObjectPool;
//using GameFramework.Resource;
//using Loxodon.Framework.Asynchronous;
//using Loxodon.Framework.Views;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.U2D;
//using UnityGameFramework.Runtime;

//namespace JMatrix
//{
//    public class GameUIComponent : GameFrameworkComponent
//    {
//        private GlobalWindowManager globalWindowManager;

//        private IObjectPool<WindowInstanceObject> m_InstancePool;
//        private  List<int> m_UIFormsBeingLoaded;
//        private  List<string> m_UIFormAssetNamesBeingLoaded;
//        private  HashSet<int> m_UIFormsToReleaseOnLoad;
//        //private readonly Dictionary<string, IProgressPromise<float,IView>> asyncResultDic;
//        private  Queue<IWindow> m_RecycleQueue;

//        private int m_Serial;

//        public UIPanelStack uIPanelStack
//        {
//            get;
//            private set;
//        }

//        void Start()
//        {
//            m_InstancePool  = GameFrameworkEntry.GetModule<IObjectPoolManager>().CreateSingleSpawnObjectPool<WindowInstanceObject>();
            
//            m_InstancePool.Capacity = 15;
//            m_InstancePool.ExpireTime = 120;
//            m_InstancePool.AutoReleaseInterval = 60;

//            m_UIFormsBeingLoaded = new List<int>();
//            m_UIFormAssetNamesBeingLoaded = new List<string>();
//            m_UIFormsToReleaseOnLoad = new HashSet<int>();
//            m_RecycleQueue = new Queue<IWindow>();
//            m_Serial = 0;
//            uIPanelStack = new UIPanelStack();
//        }

//        /// <summary>
//        /// 界面管理器轮询。
//        /// </summary>
//        void Update()
//        {
//            while (m_RecycleQueue.Count > 0)
//            {
//                IWindow window = m_RecycleQueue.Dequeue();
//                //window.OnRecycle();
//                m_InstancePool.Unspawn(window.Handle);
//            }
//        }
        
//        protected virtual IWindowManager GetDefaultWindowManager()
//        {
//            if (globalWindowManager != null)
//                return globalWindowManager;

//            globalWindowManager = GameEntry.Global.globalWindowManager;
//            if (globalWindowManager == null)
//                throw new NotFoundException("GlobalWindowManager");

//            return globalWindowManager;
//        }


//        public IProgressResult<float, T> LoadViewAsync<T>(string name) 
//        {
//            throw new GameFrameworkException("不支持的类型============");
//        }

//        protected virtual void DoLoad(string uiAssetName,Action<IWindow> onFinish)
//        {
//            int serialId = m_Serial++;
//            WindowInstanceObject uiFormInstanceObject = m_InstancePool.Spawn(uiAssetName);
//            if (uiFormInstanceObject == null)
//            {
//                GameEntry.Resource.LoadAsset(AssetUtility.GetUIFormAsset(uiAssetName), 100, new LoadAssetCallbacks(
//                    (string uiFormAssetName, object uiFormAsset, float duration, object userData) => 
//                    {
//                        OpenWindowInfo openUIFormInfo = (OpenWindowInfo)userData;
//                        if (openUIFormInfo == null)
//                        {
//                            throw new GameFrameworkException("Open UI form info is invalid.");
//                        }
//                        GameObject instanceObj = UnityEngine.Object.Instantiate((GameObject)uiFormAsset,GameEntry.Global.uiRoot.transform);
//                        instanceObj.name = uiAssetName;
//                        instanceObj.FixShaders();
//                        instanceObj.FixFontShaders();

//                        IWindow window = instanceObj.GetComponent<IWindow>();
//                        WindowInstanceObject newUiFormInstanceObject = new WindowInstanceObject(uiAssetName, uiFormAsset, instanceObj, window);
//                        m_InstancePool.Register(newUiFormInstanceObject, true);

//                        window.WindowManager = GetDefaultWindowManager();
//                        window.Handle = newUiFormInstanceObject;
//                        onFinish?.Invoke(window);
//                    })
//                    , new OpenWindowInfo(serialId,false, null)
//                    );
//            }
//            else
//            {
//                IWindow window = uiFormInstanceObject.window;
//                onFinish?.Invoke(window);
//            }
//        }

//        public  void LoadWindowAsync(string name, Action<IWindow> onFinish)
//        {
//            DoLoad(name, onFinish);
//        }

//        /// 关闭界面。
//        /// </summary>
//        /// <param name="uiForm">要关闭的界面。</param>
//        /// <param name="userData">用户自定义数据。</param>
//        public void CloseWindow(IWindow window, object userData)
//        {
//            if (window == null)
//            {
//                throw new GameFrameworkException("UI window is invalid.");
//            }
//            m_RecycleQueue.Enqueue(window);
//        }
//        /// <summary>
//        /// 锁定Window
//        /// </summary>
//        /// <param name="window"></param>
//        public void LockWindow(IWindow window)
//        {
//            m_InstancePool.SetLocked(window.Handle as WindowInstanceObject,true);
//        }

//        /// <summary>
//        /// 解锁Window
//        /// </summary>
//        /// <param name="window"></param>
//        public void UnLockWindow(IWindow window)
//        {
//            m_InstancePool.SetLocked(window.Handle as WindowInstanceObject, false);

//        }
//    }
//}
