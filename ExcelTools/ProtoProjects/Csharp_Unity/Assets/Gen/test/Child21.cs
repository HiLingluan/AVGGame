//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Bright.Serialization;

namespace proto.test
{

    public  abstract class Child21 :  test.Dyn 
    {
        public Child21()
        {
        }

        public Child21(Bright.Common.NotNullInitialization _)  : base(_) 
        {
        }

        public static void SerializeChild21(ByteBuf _buf, Child21 x)
        {
            _buf.WriteInt(x.GetTypeId());
            x.Serialize(_buf);
        }

        public static Child21 DeserializeChild21(ByteBuf _buf)
        {
           test.Child21 x;
            switch (_buf.ReadInt())
            {
                case test.Child31.__ID__: x = new test.Child31(); break;
                case test.Child32.__ID__: x = new test.Child32(); break;
                default: throw new SerializationException();
            }
            x.Deserialize(_buf);
            return x;
        }

         public int A24;


    }

}