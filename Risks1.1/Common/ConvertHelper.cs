/////////////////////////////////////////////////////
// *** UTF-8 encoding ∞ ☼ *** Кодировка UTF-8 ∞ ☼ ***
/////////////////////////////////////////////////////

using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Text;

namespace WpfApplication1.Common
{
    public static class ConvertHelper
    {
        public const string C_NULL_STR = "Null";

        #region To Enum
        public static Enum ConvertToEnum(int value, Enum defaultValue)
        {
            Enum retVal = defaultValue;
            foreach (Enum en in Enum.GetValues(defaultValue.GetType()))
            {
                if (Convert.ToInt32(en) == value)
                {
                    retVal = en;
                    break;
                }
            }
            return retVal;
        }
        #endregion

        #region To Bool
        public static bool ConvertToBool(object value)
        {
            if (IsNull(value))
                return false;

            if (value is bool)
                return (bool)value;

            if (value is System.Data.SqlTypes.SqlBoolean)
                return ((System.Data.SqlTypes.SqlBoolean)value).Value;

            bool boolResult = false;
            int intResult = 0;
            bool success = false;

            if (value is string)
            {
                string strValue = (string)value;
                if (strValue == "0" || strValue == "00")
                    return false;
                else if (strValue == "1" || strValue == "01")
                    return true;
            }

            if (value is char)
            {
                char ch = (char)value;
                if (ch == '1' || ch == '2' || ch == '3' || ch == '4' || ch == '5' || ch == '6' || ch == '7' || ch == '8' || ch == '9' || ch == 'T' || ch == 't' || ch == 'Y' || ch == 'y')  // True: цифры 1-9 и первая буква от слов True, Yes
                    return true;
                return false;
            }

            try
            {
                boolResult = Convert.ToBoolean(value);
                success = true;
            }
            catch { success = false; }

            if (success)
                return boolResult;

            try
            {
                intResult = Convert.ToInt32(value);
                success = true;
            }
            catch { success = false; }

            if (success)
                return intResult > 0;

            string str = value.ToString();
            if (string.IsNullOrEmpty(str))
                return false;
            str = str.ToUpper();
            return (str[0] == 'T' || str[0] == 'Y');
        }
        #endregion

        #region To Integer

        public static int ConvertBoolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        //public static long ConvertToLong(object value, long defaultValue)
        //{
        //    long valueLong = defaultValue;
        //    try
        //    {
        //        if (IsNull(value))
        //            return defaultValue;

        //        if (value is int)
        //            return (int)value;

        //        if (value is long)
        //            return (long)value;

        //        if (value is bool)
        //        {
        //            bool boolValue = (bool)value;
        //            return boolValue ? 1 : 0;
        //        }

        //        if (value is System.Data.SqlTypes.SqlByte)
        //            return (long)((System.Data.SqlTypes.SqlByte)value).Value;

        //        if (value is System.Data.SqlTypes.SqlInt16)
        //            return (long)((System.Data.SqlTypes.SqlInt16)value).Value;

        //        if (value is System.Data.SqlTypes.SqlInt32)
        //            return (long)((System.Data.SqlTypes.SqlInt32)value).Value;

        //        if (value is System.Data.SqlTypes.SqlInt64)
        //            return (long)((System.Data.SqlTypes.SqlInt64)value).Value;

        //        if (value is System.Data.SqlTypes.SqlBoolean)
        //            return (long)ConvertBoolToInt(((System.Data.SqlTypes.SqlBoolean)value).Value);

        //        string valueStr = value.ToString().Trim();
        //        bool isNegative = valueStr.StartsWith("-");
        //        valueStr = StringHelper.FilterDigitSymbols(valueStr);
        //        bool success = long.TryParse(valueStr, out valueLong);
        //        if (success)
        //            return (isNegative) ? (0 - valueLong) : valueLong;
        //        else
        //            return defaultValue;
        //    }
        //    catch
        //    {
        //        valueLong = defaultValue;
        //    }
        //    return valueLong;
        //}

        public static ulong ConvertToULong(object value, ulong defaultValue)
        {
            ulong valueULong = defaultValue;
            try
            {
                if (IsNull(value))
                    return defaultValue;

                if (value is bool)
                {
                    bool boolValue = (bool)value;
                    return (ulong)(boolValue ? 1 : 0);
                }

                if (value is System.Data.SqlTypes.SqlByte)
                    return (ulong)((System.Data.SqlTypes.SqlByte)value).Value;

                if (value is System.Data.SqlTypes.SqlInt16)
                    return (ulong)((System.Data.SqlTypes.SqlInt16)value).Value;

                if (value is System.Data.SqlTypes.SqlInt32)
                    return (ulong)((System.Data.SqlTypes.SqlInt32)value).Value;

                if (value is System.Data.SqlTypes.SqlInt64)
                    return (ulong)((System.Data.SqlTypes.SqlInt64)value).Value;

                if (value is System.Data.SqlTypes.SqlBoolean)
                    return (ulong)ConvertBoolToInt(((System.Data.SqlTypes.SqlBoolean)value).Value);

                string valueStr = value.ToString().Trim();
                bool success = ulong.TryParse(valueStr, out valueULong);
                if (!success)
                    valueULong = defaultValue;
            }
            catch
            {
                valueULong = defaultValue;
            }
            return valueULong;
        }


        public static int ConvertToInt(object value, int defaultValue)
        {
            int retVal = defaultValue;
            try
            {
                if (value is int)
                    retVal = (int)value;
                else retVal = defaultValue;
                //retVal = (int)ConvertToLong(value, defaultValue);
            }
            catch { }
            return retVal;
        }

        public static uint ConvertToUInt(object value, uint defaultValue)
        {
            uint retVal = defaultValue;
            try
            {
                retVal = (uint)ConvertToULong(value, defaultValue);
            }
            catch { }
            return retVal;
        }

        #endregion

        #region To String

        /// <summary>
        /// DateTime - используется не для UI: для xml сериализации, как часть имени файла и т.п.
        /// Для UI - см. DateTimeUIFormatConverter.cs
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ConvertToString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        public static string ConvertToString(DateTime? dateTime)
        {
            if (dateTime == null)
                return C_NULL_STR;
            else
                return ConvertToString(dateTime.Value);
        }

        /// <summary>
        /// 8 знаков после запятой
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToString(double value)
        {
            return value.ToString("F8", CultureInfo.InvariantCulture);
        }

        public static string ConvertToString(double? value)
        {
            if (value == null)
                return C_NULL_STR;
            else
                return value.Value.ToString("F8", CultureInfo.InvariantCulture);
        }
        #endregion

        #region string <-> byte array (UTF-8)
        public static byte[] ConvertStringToBytes(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        public static string ConvertBytesToString(byte[] value)
        {
            return Encoding.UTF8.GetString(value).TrimEnd('\0');
        }
        #endregion

        #region To Date, To TimeSpan

        public static DateTime ConvertToDate(object value, DateTime defaultValue)
        {
            if (IsNull(value))
                return defaultValue;

            if (value is DateTime)
                return (DateTime)value;

            if (value is SqlDateTime)
                return ((SqlDateTime)value).Value;

            string stringDate = value.ToString();

            if (string.IsNullOrWhiteSpace(stringDate))
                return defaultValue;

            DateTime tmpDate = DateTime.MinValue;
            bool success = parseDateTime(stringDate, out tmpDate);
            if (success)
                return tmpDate;
            else
                return defaultValue;
        }

        private static bool parseDateTime(string value, out DateTime result)
        {
            result = DateTime.MinValue;
            DateTime tmpResult = DateTime.MinValue;
            bool success = DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out tmpResult);
            if (success)
            {
                result = tmpResult;
                return success;
            }
            success = DateTime.TryParse(value, out tmpResult);
            if (success)
                result = tmpResult;
            return success;
        }

        public static TimeSpan ConvertToTimeSpan(object value)
        {
            if (IsNull(value))
                return new TimeSpan(0);

            if (value is TimeSpan)
                return (TimeSpan)value;

            TimeSpan newValue = new TimeSpan(0);
            bool success = TimeSpan.TryParse(value.ToString(), out newValue);
            if (success)
                return newValue;
            else
                return new TimeSpan(0);
        }

        public static DateTime ConvertToDateWithSpecialFormat(object value, string format, DateTime defaultValue)
        {
            if (IsNull(value))
                return defaultValue;

            if (value is DateTime)
                return (DateTime)value;

            if (value is SqlDateTime)
                return ((SqlDateTime)value).Value;

            string dateTimeStr = value.ToString();
            if (string.IsNullOrWhiteSpace(dateTimeStr))
                return defaultValue;

            if (string.IsNullOrWhiteSpace(format)) // Пустой формат означает, что формат должен браться по умолчанию из операционной системы Windows
            {
                return convertToDateWithoutFormat(dateTimeStr, defaultValue);
            }
            else // Задан конкретный формат даты 
            {
                bool success = false;
                DateTime result = DateTime.MinValue;

                // 1. Пытаемся распознать в строгом соответствии с указанным форматом и выбранным языком
                success = DateTime.TryParseExact(dateTimeStr, format, Properties.Resources.Culture, DateTimeStyles.AllowWhiteSpaces, out result);
                if (success)
                {
                    return result;
                }
                else
                {
                    // 2. Пытаемся распознать в соответствии с указанным форматом и CultureInfo.InvariantCulture
                    success = DateTime.TryParseExact(dateTimeStr, format, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result);
                    if (success)
                        return result;
                    else
                        return convertToDateWithoutFormat(dateTimeStr, defaultValue); // 3. Уже на формат не смотрим
                }
            }
        }

        private static DateTime convertToDateWithoutFormat(string dateTimeStr, DateTime defaultValue)
        {
            DateTime result = DateTime.MinValue;
            bool success = DateTime.TryParse(dateTimeStr, Properties.Resources.Culture, DateTimeStyles.AllowWhiteSpaces, out result); // 1. пытаемся распознать в соответствии с назначенным языком
            if (success)
            {
                return result;
            }
            else
            {
                success = DateTime.TryParse(dateTimeStr, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result); // 2. пытаемся распознать в соответствии с CultureInfo.InvariantCulture
                if (success)
                {
                    return result;
                }
                else
                {
                    success = DateTime.TryParse(dateTimeStr, out result); // 3. все по умолчанию
                    if (success)
                    {
                        return result;
                    }
                    else
                    {
                        success = DateTime.TryParseExact(dateTimeStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result); // напоследок пытаемся распознать дату в соответствии с нашим фиксированным форматом - чтобы дата в нем гарантированно распознавалась
                        if (success)
                            return result;
                        else
                            return defaultValue;
                    }
                }
            }
        }

        #endregion

        #region To Double

        public static double ConvertToDouble(object value)
        {
            if (value == null)
                return 0.0;
            return ConvertHelper.ConvertToDouble(value.ToString(), 0.0);
        }

        public static double ConvertToDouble(string value, double defaultValue)
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            double retVal = defaultValue;
            bool success = double.TryParse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite, CultureInfo.InvariantCulture, out retVal);
            if (!success)
                success = double.TryParse(value, out retVal);

            string value2 = string.Empty;
            if (!success)
                value2 = value.Replace('.', ',');

            if (!success)
                success = double.TryParse(value2, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite, CultureInfo.InvariantCulture, out retVal);

            if (!success)
                success = double.TryParse(value2, out retVal);

            if (!success)
                value2 = value.Replace(',', '.');

            if (!success)
                success = double.TryParse(value2, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite, CultureInfo.InvariantCulture, out retVal);

            if (!success)
                success = double.TryParse(value2, out retVal);

            if (!success && value.Trim().StartsWith("N", true, CultureInfo.InvariantCulture))
                return double.NaN;

            if (!success && value.Trim().StartsWith("-Inf", true, CultureInfo.InvariantCulture))
                return double.NegativeInfinity;

            if (!success && value.Trim().StartsWith("-\u221E", true, CultureInfo.InvariantCulture))
                return double.NegativeInfinity;

            if (!success && value.Trim().StartsWith("Inf", true, CultureInfo.InvariantCulture))
                return double.PositiveInfinity;

            if (!success && value.Trim().StartsWith("+Inf", true, CultureInfo.InvariantCulture))
                return double.PositiveInfinity;

            if (!success && value.Trim().StartsWith("+\u221E", true, CultureInfo.InvariantCulture))
                return double.PositiveInfinity;

            if (success)
                return retVal;
            else
                return defaultValue;
        }
        #endregion

        #region Test for NULL
        public static bool IsNull(object value)
        {
            if (value == null)
                return true;

            if (value == DBNull.Value)
                return true;

            System.Data.SqlTypes.INullable iNullable = value as System.Data.SqlTypes.INullable;
            if (iNullable != null)
                return iNullable.IsNull;
            else
                return false;
        }
        #endregion
    }
}
