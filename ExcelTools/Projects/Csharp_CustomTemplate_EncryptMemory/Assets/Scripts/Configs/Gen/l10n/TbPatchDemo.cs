
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using Scripts;

namespace cfg.l10n
{
    [Config]
    public partial class TbPatchDemo : ACategory<PatchDemo>
    {
        public override void TranslateText(Func<string, string, string> translator)
        {
            foreach(var v in dict.Values)
            {
                v.TranslateText(translator);
            }
        }
    }
}
