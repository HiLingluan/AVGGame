
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using Newtonsoft.Json;
using Scripts;







namespace cfg.test
{
    [Serializable]
    public partial class CompactString : AConfig
    {

        public string  s2 { get; set; }

        public string  s3 { get; set; }


        public override void EndInit() 
        {
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
