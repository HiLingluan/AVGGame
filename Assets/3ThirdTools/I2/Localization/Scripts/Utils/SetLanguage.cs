using UnityEngine;


namespace I2.Loc
{
	[AddComponentMenu("I2/Localization/SetLanguage Button")]
	public class SetLanguage : MonoBehaviour 
	{
		public string _Language;

#if UNITY_EDITOR
		public LanguageSource mSource;
#endif
		

		public void ApplyLanguage()
		{
			if( LocalizationManager.HasLanguage(_Language))
			{
				LocalizationManager.CurrentLanguage = _Language;
			}
            else
            {
				//LoggerHelper.CommLogError("不存在此语言=================="+ _Language);
            }
		}
    }
}