/////////////////////////////////////////////////////
// *** UTF-8 encoding ∞ ☼ *** Кодировка UTF-8 ∞ ☼ ***
/////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace WpfApplication1.Common
{
    public static class BinaryHelper
    {
        #region Преобразования байтов в биты и наоборот
        /// <summary>
        /// Массив байтов преобразуется в массив битов:  0x01, 0x01 -> 0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool[] BytesToBits(byte[] data)
        {
            List<bool> retVal = new List<bool>();
            foreach (byte b in data)
                retVal.AddRange(ByteToBitArray(b));
            return retVal.ToArray();
        }

        /// <summary>
        /// Массив байтов преобразуется в массив битов:  0x01, 0x01 -> 0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool[] BytesToBits(byte[] data, int startByteIndex, int count)
        {
            List<bool> retVal = new List<bool>();
            for (int i = startByteIndex; i < startByteIndex + count; i++)
                retVal.AddRange(ByteToBitArray(data[i]));
            return retVal.ToArray();
        }

        /// <summary>
        /// Массив битов преобразуется в массив байтов  0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1 -> 0x01, 0x01
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] BitsToBytes(bool[] data, LeftRightPadding leftRightPadding)
        {
            List<byte> retval = new List<byte>();
            List<bool> tmpData = new List<bool>();
            List<bool> sourceData = new List<bool>();

            #region Если количество бит не кратно 8, то дополняем массив старшими нулевыми битами до границы 8 бит чтобы было целое количество байтов
            int rest = data.Length % 8;
            if (rest > 0)
                rest = 8 - rest;
            if (leftRightPadding == LeftRightPadding.EmptyBitsAtLeft) // Добавляем нули слева - старшие биты
            {
                for (int i = 0; i < rest; i++)
                    sourceData.Add(false);
            }
            #endregion

            sourceData.AddRange(data);

            #region Если количество бит не кратно 8, то дополняем массив младшими нулевыми битами до границы 8 бит чтобы было целое количество байтов
            if (leftRightPadding == LeftRightPadding.EmptyBitsAtRight) // Добавляем нули справа - младшие биты
            {
                for (int i = 0; i < rest; i++)
                    sourceData.Add(false);
            }
            #endregion

            foreach (bool b in sourceData)
            {
                tmpData.Add(b);
                if (tmpData.Count == 8)
                {
                    byte byte1 = (byte)ReadInteger(tmpData.ToArray(), 0, 8);
                    retval.Add(byte1);
                    tmpData.Clear();
                }
            }
            return retval.ToArray();
        }

        public static byte[] BitsToBytes(bool[] data)
        {
            return BitsToBytes(data, LeftRightPadding.EmptyBitsAtLeft);
        }

        /// <summary>
        /// Массив битов преобразуется в байт. Используется последние 8 элементов массива. Остальные игнорируются
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte BitsToByte(bool[] data)
        {
            byte[] res = BitsToBytes(data);
            if (res != null && res.Length > 0)
                return res[0];
            else
                return (byte)0;
        }

        /// <summary>
        /// Byte->массив bool. Страшие биты в начале массива, в конце - младшие.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool[] ByteToBitArray(byte b)
        {
            byte[] tmpArray = new byte[1] { b };
            BitArray ba = new BitArray(tmpArray);
            List<bool> straightforwardArray = new List<bool>();
            for (int i = 7; i >= 0; i--)
                straightforwardArray.Add(ba[i]);
            return straightforwardArray.ToArray();
        }

        #endregion

        #region преобразование массива бит в целое число и наоборот
        /// <summary>
        /// Из массива бит считывается целое число: 0000010 -> число 2
        /// </summary>
        /// <param name="array">Массив бит</param>
        /// <param name="startIndex">Позиция первого считываемого бита в массиве</param>
        /// <param name="len">Количество бит для считывания</param>
        /// <returns></returns>
        public static ulong ReadInteger(bool[] array, int startIndex, int len)
        {
            ulong result = 0;
            if (array == null || array.Length < startIndex)
                return result;
            if (len > 64)
                len = 64;
            int startPos = startIndex + len - 1;
            if (startPos >= array.Length)
                startPos = array.Length - 1;
            int count = -1;
            for (int i = startPos; i >= startIndex; i--)
            {
                count++;
                if (array[i])
                {
                    ulong mask = (ulong)1 << count;
                    result |= mask;
                }
            }
            return result;
        }

        /// <summary>
        /// Возвращает массив битов - указанное количество (count) младших битов двоичного представления числа (value): число 3 -> 000011
        /// </summary>
        /// <param name="value"></param>
        /// <param name="lastBitCount"></param>
        /// <returns></returns>
        public static bool[] WriteInteger(ulong value, int count)
        {
            bool[] result = new bool[count];
            for (int i = 0; i < count; i++)
            {
                ulong mask = (ulong)1 << i;
                result[count - i - 1] = ((value & mask) != 0);
            }
            return result;
        }

        #endregion

        #region считать число, записанное в строковом 16-ричном представлении: строка "AAA" -> число 2730
        /// <summary>
        /// Read integer value from hex representation
        /// </summary>
        /// <param name="hexIntStr"></param>
        /// <returns></returns>
        public static int HexStrToInteger(string hexIntStr)
        {
            int result = -1;
            try { result = int.Parse(hexIntStr, NumberStyles.HexNumber); }
            catch { }
            return result;
        }
        #endregion

        #region Преобразование массива байт в строку в 16-ричном формате и наоборот: массиив [255, 255, 255] -> строка "FFFFFF"
        /// <summary>
        /// Array of bytes -> hex string
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static string ByteArrayToHexStr(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:X2}", b);
            return hex.ToString();
        }

        /// <summary>
        /// Hex string -> array of bytes
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static byte[] HexStrToByteArray(string hexStr)
        {
            if (hexStr.Length % 2 != 0)
                hexStr = '0' + hexStr;
            int NumberChars = hexStr.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hexStr.Substring(i, 2), 16);
            return bytes;
        }
        #endregion

        #region Aналог Substring, только для массива бит
        public static bool[] GetSubArray(bool[] data, int startBit, int length)
        {
            bool[] retVal = new bool[length];
            int k = 0;
            for (int i = startBit; i < (startBit + length); i++)
                retVal[k++] = data[i];
            return retVal;
        }
        #endregion

        #region  сжатие и декомпресия данных (gzip, алгоритм DEFLATE)
        public static byte[] Compress(byte[] source)
        {
            if (source == null)
                return null;
            if (source.Length < 2)
                return source;

            byte[] result;
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream zip = new GZipStream(stream, CompressionMode.Compress))
                {
                    zip.Write(source, 0, source.Length);
                }
                result = stream.ToArray();
            }
            return result;
        }

        public static byte[] Decompress(byte[] source)
        {
            if (source == null)
                return null;
            if (source.Length < 2)
                return source;

            List<byte> result = new List<byte>();
            using (MemoryStream stream = new MemoryStream(source))
            {
                using (GZipStream zip = new GZipStream(stream, CompressionMode.Decompress))
                {
                    int bValue = 0;
                    while (true)
                    {
                        bValue = zip.ReadByte();
                        if (bValue == -1)
                            break;
                        else
                            result.Add((byte)bValue);
                    }
                }
            }
            return result.ToArray();
        }

        #endregion

        #region padding - добавление и удаление набивки нулевыми битами массива бит или байт. Т.е. можно вставить, например, 3 нулевых бита, потом 5 битов из входного массива, потом снова 3 набивочных бита, потом опять 5 очередных бита из входного массива так далее

        /// <summary>
        /// Добавление пустой набивки по следующему принципу:
        /// первые saveBits копируются один в один. Затем добавляется addBits набивочных нулевых битов. Затем, если еще есть даенные, все повторяется.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool[] AddPadding(bool[] data, int saveBits, int addBits)
        {
            List<bool> retVal = new List<bool>();
            int counter = 0;
            List<bool> appendix = new List<bool>();
            for (int i = 0; i < addBits; i++)
                appendix.Add(false);

            for (int i = 0; i < data.Length; i++)
            {
                retVal.Add(data[i]);
                counter++;
                if (counter == saveBits)
                {
                    retVal.AddRange(appendix);
                    counter = 0;
                }
            }
            return retVal.ToArray();
        }

        public static byte[] AddPadding(byte[] data, int saveBits, int addBits, LeftRightPadding leftOrRightPadding)
        {
            bool[] sourceData = BinaryHelper.BytesToBits(data);
            bool[] removedResult = AddPadding(sourceData, saveBits, addBits);
            byte[] result = BinaryHelper.BitsToBytes(removedResult, leftOrRightPadding);
            return result;
        }

        /// <summary>
        /// Удаление набивки. Первые биты количеством saveBits сохраняются. Затем deleteBits прокидываются. Затем все повторяется.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="saveBits"></param>
        /// <param name="deleteBits"></param>
        /// <returns></returns>
        public static bool[] RemovePadding(bool[] data, int saveBits, int deleteBits)
        {
            List<bool> retVal = new List<bool>();
            int counter1 = 0;
            int counter2 = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (counter1 < saveBits)
                {
                    retVal.Add(data[i]);
                    counter1++;
                }
                else
                {
                    counter2++;
                }
                if (counter2 == deleteBits)
                {
                    counter1 = 0;
                    counter2 = 0;
                }
            }
            return retVal.ToArray();
        }

        public static byte[] RemovePadding(byte[] data, int saveBits, int deleteBits, LeftRightPadding leftRightPadding)
        {
            bool[] sourceData = BinaryHelper.BytesToBits(data);
            bool[] removedResult = RemovePadding(sourceData, saveBits, deleteBits);
            byte[] result = BinaryHelper.BitsToBytes(removedResult, leftRightPadding);
            return result;
        }

        #endregion
    }

    public enum LeftRightPadding
    {
        EmptyBitsAtLeft = 0,
        EmptyBitsAtRight = 1,
    }

}
