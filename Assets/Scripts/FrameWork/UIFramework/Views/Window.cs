
using System;
using UnityEngine;

using FrameLog = UnityGameFramework.Runtime.Log;

namespace Game.UI
{
    public interface IWindowData { }

    [DisallowMultipleComponent]
    public abstract class Window : MonoBehaviour , IWindow
    {
        public WindowType windowType;
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Key { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Created => throw new NotImplementedException();

        public bool Dismissed => throw new NotImplementedException();

        public bool Visibility => throw new NotImplementedException();

        public bool Activated => throw new NotImplementedException();

        //public IWindowManager WindowManager { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public WindowType WindowType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int WindowPriority { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IWindowData windowData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object Handle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler VisibilityChanged;
        public event EventHandler ActivatedChanged;
        public event EventHandler OnDismissed;
        public event EventHandler<WindowStateEventArgs> StateChanged;

        public void Create()
        {
            throw new NotImplementedException();
        }

        public ITransition Dismiss(bool ignoreAnimation = false)
        {
            throw new NotImplementedException();
        }

        public ITransition Hide(bool ignoreAnimation = false)
        {
            throw new NotImplementedException();
        }

        public ITransition Show(bool ignoreAnimation = false)
        {
            throw new NotImplementedException();
        }
    }
}
