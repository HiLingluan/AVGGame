
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







namespace cfg.error
{
    [Serializable]
    public partial class ErrorStyleDlgOkCancel : AConfig
    {
        public string  btn1_name { get; set; }

        public string  btn2_name { get; set; }


        public override void EndInit() 
        {
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
