
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
    public partial class TestSet : AConfig
    {

        public System.Collections.Generic.List<int>  x1 { get; set; }

        public System.Collections.Generic.List<long>  x2 { get; set; }

        public System.Collections.Generic.List<string>  x3 { get; set; }

        public System.Collections.Generic.List<test.DemoEnum>  x4 { get; set; }


        public override void EndInit() 
        {
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
