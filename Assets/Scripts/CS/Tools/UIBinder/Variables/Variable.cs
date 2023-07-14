using UnityEngine;

namespace Game.Editor
{
    [System.Serializable]
    public enum VariableType
    {
        Object,
        GameObject,
        Component,
    }

    [System.Serializable]
    public class Variable
    {
        [SerializeField]
        protected string name = "";

        [SerializeField]
        protected string note = "";

        [SerializeField]
        protected UnityEngine.Object objectValue;

        [SerializeField]
        protected string dataValue;

        [SerializeField]
        protected VariableType variableType;

        public virtual string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public virtual string Note
        {
            get { return this.note; }
            set { this.note = value; }
        }

        public virtual VariableType VariableType
        {
            get { return this.variableType; }
        }

        public virtual System.Type ValueType
        {
            get
            {
                switch (this.variableType)
                {
                    case VariableType.Object:
                        return this.objectValue == null ? typeof(UnityEngine.Object) : this.objectValue.GetType();
                    case VariableType.GameObject:
                        return this.objectValue == null ? typeof(GameObject) : this.objectValue.GetType();
                    case VariableType.Component:
                        return this.objectValue == null ? typeof(Component) : this.objectValue.GetType();
                    default:
                        throw new System.NotSupportedException();
                }
            }
        }

        public virtual void SetValue<T>(T value)
        {
            this.SetValue(value);
        }

        public virtual T GetValue<T>()
        {
            return (T)GetValue();
        }

        public virtual void SetValue(object value)
        {
            switch (this.variableType)
            {
                case VariableType.Object:
                    this.objectValue = (UnityEngine.Object)value;
                    break;
                case VariableType.GameObject:
                    this.objectValue = (GameObject)value;
                    break;
                case VariableType.Component:
                    this.objectValue = (Component)value;
                    break;
                default:
                    throw new System.NotSupportedException();
            }
        }
        public virtual object GetValue()
        {
            switch (this.variableType)
            {
                case VariableType.Object:
                    return this.objectValue;
                case VariableType.GameObject:
                    return this.objectValue;
                case VariableType.Component:
                    return this.objectValue;
                default:
                    throw new System.NotSupportedException();
            }
        }
    }
}
