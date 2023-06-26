using System.IO;
using System;
using System.Text.RegularExpressions;


namespace Util
{
    public static class Utils
    {
        public static string RemoveDataTag(string imageData)
        {
            return Regex.Replace(imageData, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
        }

    };
};