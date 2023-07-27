using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI 
{
    [DisallowMultipleComponent]
    public class WindowManager : MonoBehaviour
    {
        private bool lastActivated = true;
        private bool activated = true;
        private List<IWindow> windows = new List<IWindow>();

        public virtual bool Activated
        {
            get { return this.activated; }
            set
            {
                if (this.activated == value)
                    return;

                this.activated = value;
            }
        }

        public int Count { get { return this.windows.Count; } }

        public int VisibleCount { get { return this.windows.FindAll(w => w.Visibility).Count; } }

        public virtual IWindow Current
        {
            get
            {
                if (this.windows == null || this.windows.Count <= 0)
                    return null;

                IWindow window = this.windows[0];
                return window != null && window.Visibility ? window : null;
            }
        }


        protected virtual void OnEnable()
        {
            this.Activated = this.lastActivated;
        }

        protected virtual void OnDisable()
        {
            this.lastActivated = this.Activated;
            this.Activated = false;
        }

        protected virtual void OnDestroy()
        {
            if (this.windows.Count > 0)
            {
                this.Clear();
            }
        }
        
        public virtual void Clear()
        {
            for (int i = 0; i < this.windows.Count; i++)
            {
                try
                {
                    this.windows[i].Dismiss();
                }
                catch (Exception) { }
            }
            this.windows.Clear();
        }

        public virtual bool Contains(IWindow window)
        {
            return this.windows.Contains(window);
        }

        public virtual int IndexOf(IWindow window)
        {
            return this.windows.IndexOf(window);
        }

        public virtual IWindow Get(int index)
        {
            if (index < 0 || index > this.windows.Count - 1)
                throw new IndexOutOfRangeException();

            return this.windows[index];
        }

        public virtual void Add(IWindow window)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            if (this.windows.Contains(window))
                return;

            this.windows.Add(window);
            this.AddChild(GetTransform(window));
        }

        public virtual bool Remove(IWindow window)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            this.RemoveChild(GetTransform(window));
            return this.windows.Remove(window);
        }

        public virtual IWindow RemoveAt(int index)
        {
            if (index < 0 || index > this.windows.Count - 1)
                throw new IndexOutOfRangeException();

            var window = this.windows[index];
            this.RemoveChild(GetTransform(window));
            this.windows.RemoveAt(index);
            return window;
        }

        protected virtual void MoveToLast(IWindow window)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            try
            {
                int index = this.IndexOf(window);
                if (index < 0 || index == this.Count - 1)
                    return;

                this.windows.RemoveAt(index);
                this.windows.Add(window);
            }
            finally
            {
                Transform transform = GetTransform(window);
                if (transform != null)
                    transform.SetAsFirstSibling();
            }
        }

        protected virtual void MoveToFirst(IWindow window)
        {
            this.MoveToIndex(window, 0);
        }

        protected virtual void MoveToIndex(IWindow window, int index)
        {
            if (window == null)
                throw new ArgumentNullException("window");

            int oldIndex = this.IndexOf(window);
            try
            {
                if (oldIndex < 0 || oldIndex == index)
                    return;

                this.windows.RemoveAt(oldIndex);
                this.windows.Insert(index, window);
            }
            finally
            {
                Transform transform = GetTransform(window);
                if (transform != null)
                {
                    if (index == 0)
                    {
                        transform.SetAsLastSibling();
                    }
                    else
                    {
                        IWindow preWindow = this.windows[index - 1];
                        int preWindowPosition = GetChildIndex(GetTransform(preWindow));
                        int currWindowPosition = oldIndex >= index ? preWindowPosition - 1 : preWindowPosition;
                        transform.SetSiblingIndex(currWindowPosition);
                    }
                }
            }
        }


        protected virtual Transform GetTransform(IWindow window)
        {
            try
            {
                if (window == null)
                    return null;
        
                var propertyInfo = window.GetType().GetProperty("Transform");
                if (propertyInfo != null)
                    return (Transform)propertyInfo.GetGetMethod().Invoke(window, null);

                if (window is Component)
                    return (window as Component).transform;
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual List<IWindow> Find(bool visible)
        {
            return this.windows.FindAll(w => w.Visibility == visible);
        }

        public virtual IWindow Find(Type windowType)
        {
            if (windowType == null)
                return null;

            return this.windows.Find(w => windowType.IsAssignableFrom(w.GetType()));
        }

        public virtual T Find<T>() where T : IWindow
        {
            return (T)this.windows.Find(w => w is T);
        }

        public virtual IWindow Find(string name, Type windowType)
        {
            if (name == null || windowType == null)
                return null;

            var window =  this.windows.Find(w => windowType.IsAssignableFrom(w.GetType()) && w.Name.Equals(name));
            return window;
        }

        public virtual T Find<T>(string name) where T : IWindow
        {
            return (T)this.windows.Find(w => w is T && w.Name.Equals(name));
        }

        public virtual List<IWindow> FindAll(Type windowType)
        {
            List<IWindow> list = new List<IWindow>();
            foreach (IWindow window in this.windows)
            {
                if (windowType.IsAssignableFrom(window.GetType()))
                    list.Add(window);
            }
            return list;
        }

        public virtual List<T> FindAll<T>() where T : IWindow
        {
            List<T> list = new List<T>();
            foreach (IWindow window in this.windows)
            {
                if (window is T)
                    list.Add((T)window);
            }
            return list;
        }

        public virtual List<IWindow> FindAllByWindowTypes(WindowType[] type)
        {
            List<IWindow> list = new List<IWindow>();
            foreach (IWindow window in this.windows)
            {
                foreach (var t in type)
                {
                    if (window.WindowType == t)
                    {
                        list.Add(window);
                        continue;
                    }
                }
            }
            return list;
        }

        protected virtual int GetChildIndex(Transform child)
        {
            Transform transform = this.transform;
            int count = transform.childCount;
            for (int i = count - 1; i >= 0; i--)
            {
                if (transform.GetChild(i).Equals(child))
                    return i;
            }
            return -1;
        }

        protected virtual void AddChild(Transform child, bool worldPositionStays = false)
        {
            if (child == null || this.transform.Equals(child.parent))
                return;

            child.gameObject.layer = this.gameObject.layer;
            child.SetParent(this.transform, worldPositionStays);
            child.SetAsFirstSibling();
        }

        protected virtual void RemoveChild(Transform child, bool worldPositionStays = false)
        {
            if (child == null || !this.transform.Equals(child.parent))
                return;

            child.SetParent(null, worldPositionStays);
        }

        public void Show(IWindow window)
        {
            window.Show();
            this.MoveToFirst(window);
        }

        public void Hide(IWindow window)
        {
            window.Hide();
            this.MoveToLast(window);
        }

        public void Dismiss(IWindow window)
        {
            window.Dismiss();
            this.MoveToLast(window);
        }
        
    }
}
