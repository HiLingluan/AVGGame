
using UGFExtensions.SpriteCollection;
using GameFramework;
using System.Text;

namespace Game
{
    public static class CommonTools
    {
        public static string ToSplit(this string fileName)
        {
            string[] fileNames = fileName.Split('.');
            return fileNames[fileNames.Length - 1];
        }
    }
}