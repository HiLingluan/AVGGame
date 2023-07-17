using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using XLua;

namespace Game.UI
{
    public class WindowInfo
    {
        /// <summary>
        /// 界面类型
        /// </summary>
        public WindowType type;
        /// <summary>
        /// 名称
        /// </summary>
        public string name;
        /// <summary>
        /// 界面参数快照
        /// </summary>
        public IWindowData data;
        /// <summary>
        /// 
        /// </summary>
        public bool isPushToStack;

        public WindowInfo(Window window, bool isPushToStack)
        {
            this.type = window.WindowType;
            this.name = window.Name;
            this.data = window.windowData;
            this.isPushToStack = isPushToStack;
        }

        public WindowInfo()
        {

        }
    }
    public class UIPanelStack
    {
        Stack<WindowInfo> windowStack;

        /// <summary>
        /// 二级界面字典
        /// </summary>
        //Dictionary<string, List<WindowInfo>> popupWindow;

        public bool isAlowClick = true;
        public UIPanelStack()
        {
            windowStack = new Stack<WindowInfo>();
        }

        /// <summary>
        /// 一级界面入栈
        /// </summary>
        /// <param name="nextWindow"></param>
        /// <param name="isPushToStack">是否入栈</param>
        public void PushFullWindow(Window nextWindow, bool isPushToStack)
        {
            try
            {
                if (nextWindow == null || !isPushToStack) return;

                if (nextWindow.WindowType != WindowType.FULL)
                {
                    Log.Warning("界面类型错误,无法入栈==============" + nextWindow.Name);
                    return;
                }
                //记录上个界面的数据快照 并关闭上个一级界面
                if (windowStack.Count > 0)
                {
                    WindowInfo curContext = windowStack.Peek();
                    var window = GetWindow(curContext.name);
                    if (window != null)  //上个界面可能没有创建 只有数据
                    {
                        curContext.data = window.windowData;
                        window.Hide();
                    }
                }

                //新界面入栈
                WindowInfo info = new WindowInfo(nextWindow, isPushToStack);
                windowStack.Push(info);
            }
            catch (Exception ex)
            {
                Log.Warning("Push Exception :" + ex.ToString());
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        public void Back()
        {
            try
            {
                if (windowStack.Count <= 0) return;
                if (!isAlowClick) return;
                isAlowClick = false;
                WindowInfo currentWindow = windowStack.Pop();

                if (windowStack.Count <= 0)
                {
                    GetWindow(currentWindow.name)?.Hide();
                    isAlowClick = true;
                    return;
                }
                var previousWindowInfo = windowStack.Peek();
                CreateOrGetWindow(previousWindowInfo.name, (previousWindow) =>
                {
                    previousWindow.windowData = previousWindowInfo.data;
                    ITransition asyncResult = previousWindow.Show();
                    asyncResult.OnFinish(() =>
                    {
                        GetWindow(currentWindow.name)?.Hide(true);
                        isAlowClick = true;
                        //ExitAnimation有Bug
                    });
                });
            }
            catch (Exception ex)
            {
                isAlowClick = true;
                Log.Warning("Back Exception :" + ex.ToString());
            }
        }

        /// <summary>
        /// 获取Window
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Window GetWindow(string name)
        {
            //return GlobalWindowManager.Root.Find<Window>(name);
            return null;
        }

        void CreateOrGetWindow(string name, Action<IWindow> onFinish)
        {
            //Window window = GlobalWindowManager.Root.Find<Window>(name);
            //if (window != null)
            //{
            //    onFinish?.Invoke(window);
            //    return;
            //}

            //GameEntry.UI.LoadWindowAsync(name, onFinish);
        }
        /// <summary>
        /// 直接模拟堆栈数据
        /// </summary>
        /// <param name="windowInfo"></param>
        public void SetStackInfo(WindowInfo[] windowInfo)
        {
            windowStack.Clear();
            foreach (var info in windowInfo)
            {
                windowStack.Push(info);
            }
        }

        /// <summary>
        /// 增加界面数据
        /// </summary>
        /// <param name="windowInfo"></param>
        public void AddStackInfo(WindowInfo windowInfo)
        {
            windowStack.Push(windowInfo);
        }


        /// <summary>
        /// --获取堆栈顶部信息
        /// </summary>
        /// <returns></returns>
        public WindowInfo GetStackPeekInfo()
        {
            if (windowStack == null || windowStack.Count == 0)
            {
                return null;
            }
            WindowInfo windowInfo = windowStack.Peek();
            return windowInfo;
        }


        /// <summary>
        /// 清除队列顶部UI数据
        /// </summary>
        public void RemoveTop()
        {
            if (windowStack.Count > 0)
            {
                windowStack.Pop();
            }
        }

        /// <summary>
        /// 清除UI堆栈
        /// </summary>
        public void ClearStackInfo()
        {
            windowStack.Clear();
        }

    }

}
