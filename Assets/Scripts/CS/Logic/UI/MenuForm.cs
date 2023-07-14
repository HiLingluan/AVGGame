//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class MenuForm : UGuiForm
    {
        [SerializeField]
        //private MenuFormBinder binder = null;

        private ProcedureMenu m_ProcedureMenu = null;

        void OnStartButtonClick()
        {
            m_ProcedureMenu.StartGame();
        }

        void OnSettingButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.SettingForm);
        }

        void OnAboutButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.AboutForm);
        }

        void OnQuitButtonClick()
        {
            GameEntry.UI.OpenDialog(new DialogParams()
            {
                Mode = 2,
                Title = GameEntry.Localization.GetString("AskQuitGame.Title"),
                Message = GameEntry.Localization.GetString("AskQuitGame.Message"),
                OnClickConfirm = delegate (object userData) { UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit); },
            });
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            //binder.setBtn.gameObject.SetActive(false);
            //binder.aboutBtn.gameObject.SetActive(false);
            //binder.quitBtn.gameObject.SetActive(false);
            AddClickListener();
        }

        void AddClickListener()
        {
            //binder.startBtn.onClick.AddListener(OnStartButtonClick);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            m_ProcedureMenu = (ProcedureMenu)userData;
            if (m_ProcedureMenu == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }
            
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            m_ProcedureMenu = null;

            base.OnClose(isShutdown, userData);
        }
    }
}
