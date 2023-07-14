//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using System.Linq;



namespace cfg.condition
{

public abstract partial class BoolRoleCondition :  condition.RoleCondition 
{
    public BoolRoleCondition(ByteBuf _buf)  : base(_buf) 
    {
        PostInit();
    }

    public static BoolRoleCondition DeserializeBoolRoleCondition(ByteBuf _buf)
    {
        switch (_buf.ReadInt())
        {
            case condition.GenderLimit.__ID__: return new condition.GenderLimit(_buf);
            case condition.MinLevel.__ID__: return new condition.MinLevel(_buf);
            case condition.MaxLevel.__ID__: return new condition.MaxLevel(_buf);
            case condition.MinMaxLevel.__ID__: return new condition.MinMaxLevel(_buf);
            case condition.ClothesPropertyScoreGreaterThan.__ID__: return new condition.ClothesPropertyScoreGreaterThan(_buf);
            default: throw new SerializationException();
        }
    }



    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
    }

    public void Reload(BoolRoleCondition reloadData)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
