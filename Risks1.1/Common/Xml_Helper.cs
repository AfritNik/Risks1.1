/////////////////////////////////////////////////////
// *** UTF-8 encoding ∞ ☼ *** Кодировка UTF-8 ∞ ☼ ***
/////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
//using NS.Shared.Common;

namespace WpfApplication1.Common
{
    public static class Xml_Helper
    {
        public const string C_NULL_STR = "Null";

        #region Common methods
        public static XmlDocument LoadXmlDocum(string xmlText)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);
            return doc;
        }

        public static string LoadXmlFromFile(string fileName)
        {
            string xmlText = File.ReadAllText(fileName);
            XmlDocument xmlDoc = LoadXmlDocum(xmlText);
            return SerializeXml(xmlDoc);
        }

        public static XmlElement FindElement(XmlNode parentNode, string elementName)
        {
            if (parentNode == null)
                return null;

            foreach (XmlNode node in parentNode.ChildNodes)
            {
                XmlElement elem = node as XmlElement;
                if (elem != null && string.Compare(elem.Name, elementName, true) == 0)
                    return elem;
            }

            if (string.Compare(parentNode.Name, elementName, true) == 0)
            {
                XmlElement elem = parentNode as XmlElement;
                if (elem != null)
                    return elem;
            }
            return null;
        }

        /// <summary>
        /// Предназначен для взятия атрибута "Count" ноды-списка
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public static int GetCount(XmlNode parentNode, string elementName)
        {
            if (parentNode == null)
                return 0;
            XmlElement element = FindElement(parentNode, elementName);
            if (element == null)
                return 0;
            XmlAttribute attr = FindAttribute(element, "Count");
            if (attr == null)
                return 0;
            return ConvertHelper.ConvertToInt(attr.InnerText, 0);
        }

        /// <summary>
        /// Получить список xml сериализированных объектов из корневого узла xml документа.
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static List<string> ParseListXml(string xmlDoc)
        {
            XmlNode startNode = LoadXmlDocum(xmlDoc).FirstChild;
            if (GetCount(startNode.ParentNode, startNode.Name) > 0)
            {
                return GetAllObjectsAsXmlStrings(startNode);
            }
            else
            {
                startNode = LoadXmlDocum(xmlDoc);
                foreach (XmlNode node in startNode.ChildNodes)
                {
                    if (GetCount(node.ParentNode, node.Name) > 0)
                    {
                        startNode = node;
                        break;
                    }
                }
                if (GetCount(startNode.ParentNode, startNode.Name) > 0)
                    return GetAllObjectsAsXmlStrings(startNode);
                else
                    return new List<string>();
            }
        }

        public static XmlDocument AddXmlDeclaration(XmlDocument xmlDoc)
        {
            bool hasDec = xmlDoc.FirstChild.NodeType == XmlNodeType.XmlDeclaration;
            if (!hasDec)
            {
                XmlDeclaration declar = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                XmlElement root = xmlDoc.DocumentElement;
                xmlDoc.InsertBefore(declar, root);
            }
            return xmlDoc;
        }

        #endregion

        #region Serialize

        public static void SerializeXmlToFile(string fileName, ISerializeXml iSerializeXml)
        {
            string title = Path.GetFileNameWithoutExtension(fileName);
            if (string.IsNullOrEmpty(title))
                title = "title";
            SerializeXmlToFile(fileName, iSerializeXml, title);
        }

        public static void SerializeXmlToFile(string fileName, ISerializeXml iSerializeXml, string title)
        {
            if (iSerializeXml != null)
            {
                string xmlText = iSerializeXml.SerializeXml(title);
                SerializeXmlToFile(fileName, xmlText);
            }
        }

        public static void SerializeXmlToFile(string fileName, string xmlText)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);
            SerializeXmlToFile(fileName, doc);
        }

        public static void SerializeXmlToFile(string fileName, XmlDocument xmlDoc)
        {
            Encoding encoding = Encoding.UTF8;
            string xmlText = serializeXml(xmlDoc, true, out encoding);
            File.WriteAllText(fileName, xmlText, encoding);
        }

        public static string SerializeXml(XmlDocument xmlDoc)
        {
            Encoding encoding = Encoding.UTF8;
            return serializeXml(xmlDoc, false, out encoding);
        }

        private static string serializeXml(XmlDocument xmlDoc, bool withXmlDeclaration, out Encoding encoding)
        {
            encoding = Encoding.UTF8;
            if (withXmlDeclaration)
            {
                bool hasDec = xmlDoc.FirstChild.NodeType == XmlNodeType.XmlDeclaration;
                XmlDeclaration declar = xmlDoc.FirstChild as XmlDeclaration;
                if (hasDec && declar != null)
                {
                    try
                    {
                        encoding = Encoding.GetEncoding(declar.Encoding);
                    }
                    catch
                    {
                        encoding = Encoding.UTF8;
                        declar.Encoding = "UTF-8";
                    }
                }
                else
                {
                    declar = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                    XmlElement root = xmlDoc.DocumentElement;
                    xmlDoc.InsertBefore(declar, root);
                    encoding = Encoding.UTF8;
                }
            }

            using (StringWriterWithEncoding stringWriter = new StringWriterWithEncoding(encoding))
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.Encoding = encoding;
                settings.OmitXmlDeclaration = !withXmlDeclaration;
                using (XmlWriter xmlTextWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xmlDoc.Save(xmlTextWriter);
                    return stringWriter.ToString();
                }
            }
        }

        public static string SerializeXml(XmlElement element)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rootElement = doc.CreateElement(element.Name);
            rootElement.InnerXml = element.InnerXml;
            foreach (XmlAttribute attribute in element.Attributes)
            {
                XmlAttribute newAttribute = doc.CreateAttribute(attribute.Name);
                newAttribute.InnerText = attribute.InnerText;
                rootElement.Attributes.Append(newAttribute);
            }
            doc.AppendChild(rootElement);
            return SerializeXml(doc);
        }

        /// <summary>
        /// Список xml сериализованных объектов сериализуется в один объект под указанным именем (listName)
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="listOfXmlObjects"></param>
        /// <returns></returns>
        public static string SerializeListOfXmlObjects(string listName, IEnumerable<string> listOfXmlObjects)
        {
            return SerializeListOfXmlObjects(0, listName, listOfXmlObjects);
        }

        /// <summary>
        /// Список xml сериализованных объектов сериализуется в один объект под указанным именем (listName)
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="listOfXmlObjects"></param>
        /// <returns></returns>
        public static string SerializeListOfXmlObjects(int actionID, string listName, IEnumerable<string> listOfXmlObjects)
        {
            string retVal = string.Empty;
            if (listOfXmlObjects != null)
            {
                int count = 0;
                foreach (string xmlText in listOfXmlObjects)
                    count++;
                if (count > 0)
                {
                    XmlDocument doc = new XmlDocument();
                    XmlElement listNode = doc.CreateElement(listName);

                    XmlAttribute attrCount = doc.CreateAttribute("Count");
                    attrCount.InnerText = count.ToString(CultureInfo.InvariantCulture);

                    XmlAttribute commandIDAttr = doc.CreateAttribute("ActionId");
                    commandIDAttr.InnerText = actionID.ToString(CultureInfo.InvariantCulture);

                    listNode.Attributes.Append(attrCount);
                    listNode.Attributes.Append(commandIDAttr);

                    doc.AppendChild(listNode);

                    foreach (string xmlText in listOfXmlObjects)
                        AddXmlObject(listNode, xmlText);

                    retVal = SerializeXml(doc);
                }
            }
            return retVal;
        }

        /// <summary>
        /// Xml сериализация строкового значения под указанным именем
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, string value)
        {
            return serializeSimpleValue(valueName, value);
        }

        /// <summary>
        /// Xml сериализация int значения под указанным именем
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, int? value)
        {
            if (value.HasValue)
                return serializeSimpleValue(valueName, value.Value.ToString(CultureInfo.InvariantCulture));
            else
                return serializeSimpleValue(valueName, C_NULL_STR);
        }

        /// <summary>
        /// Xml сериализация uint значения под указанным именем
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, uint? value)
        {
            if (value.HasValue)
                return serializeSimpleValue(valueName, value.Value.ToString(CultureInfo.InvariantCulture));
            else
                return serializeSimpleValue(valueName, C_NULL_STR);
        }

        /// <summary>
        /// Xml сериализация long значения под указанным именем
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, long? value)
        {
            if (value.HasValue)
                return serializeSimpleValue(valueName, value.Value.ToString(CultureInfo.InvariantCulture));
            else
                return serializeSimpleValue(valueName, C_NULL_STR);
        }

        /// <summary>
        /// Xml сериализация ulong значения под указанным именем
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, ulong? value)
        {
            if (value.HasValue)
                return serializeSimpleValue(valueName, value.Value.ToString(CultureInfo.InvariantCulture));
            else
                return serializeSimpleValue(valueName, C_NULL_STR);
        }

        /// <summary>
        /// Xml сериализация bool значения под указанным именем
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, bool? value)
        {
            if (value.HasValue)
            {
                int valueInt = ConvertHelper.ConvertBoolToInt(value.Value);
                return SerializeValue(valueName, valueInt);
            }
            else
            {
                return serializeSimpleValue(valueName, C_NULL_STR);
            }
        }

        /// <summary>
        /// Xml сериализация double значения под указанным именем
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, double? value)
        {
            if (value.HasValue)
                return serializeSimpleValue(valueName, ConvertHelper.ConvertToString(value.Value));
            else
                return serializeSimpleValue(valueName, C_NULL_STR);
        }

        /// <summary>
        /// Xml сериализация Enum значения под указанным именем
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, Enum value)
        {
            int valueInt = ((IConvertible)value).ToInt32(CultureInfo.InvariantCulture);
            return SerializeValue(valueName, valueInt);
        }

        /// <summary>
        /// Xml сериализация DateTime значения под указанным именем
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, DateTime? value)
        {
            if (value.HasValue)
                return serializeSimpleValue(valueName, ConvertHelper.ConvertToString(value.Value));
            else
                return serializeSimpleValue(valueName, C_NULL_STR);
        }

        /// <summary>
        /// Xml сериализация TimeSpan значения под указанным именем (сериализуем как long значение - количество миллисекунд)
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, TimeSpan? value)
        {
            if (value.HasValue)
                return SerializeValue(valueName, (long)value.Value.TotalMilliseconds);
            else
                return serializeSimpleValue(valueName, C_NULL_STR);
        }

        /// <summary>
        /// Xml сериализация Guid значения под указанным именем
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, Guid? value)
        {
            if (value.HasValue)
                return serializeSimpleValue(valueName, value.Value.ToString("D"));
            else
                return serializeSimpleValue(valueName, C_NULL_STR);
        }

        /// <summary>
        /// Xml сериализация byte[] под указанным именем
        /// </summary>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeValue(string valueName, byte[] value)
        {
            if (value == null)
            {
                return serializeSimpleValue(valueName, C_NULL_STR);
            }
            else
            {
                byte[] compressedData = BinaryHelper.Compress(value);
                string valueStr = BinaryHelper.ByteArrayToHexStr(compressedData);
                return serializeSimpleValue(valueName, valueStr);
            }
        }

        #endregion

        #region Add Objects to Document

        /// <summary>
        /// В указанную ноду добавляется строковое значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, string value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        /// <summary>
        /// В указанную ноду добавляется bool значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, bool? value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        /// <summary>
        /// В указанную ноду добавляется int значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, int? value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        /// <summary>
        /// В указанную ноду добавляется uint значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, uint? value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        /// <summary>
        /// В указанную ноду добавляется long значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, long? value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        /// <summary>
        /// В указанную ноду добавляется ulong значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, ulong? value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        /// <summary>
        /// В указанную ноду добавляется double значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, double? value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        /// <summary>
        /// В указанную ноду добавляется Enum значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, Enum value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        /// <summary>
        /// В указанную ноду добавляется DateTime значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, DateTime? value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        /// <summary>
        /// В указанную ноду добавляется TimeSpan значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, TimeSpan? value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        /// <summary>
        /// В указанную ноду добавляется Guid значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, Guid? value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        /// <summary>
        /// В указанную ноду добавляется byte[] значение под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        public static void AddValue(XmlNode node, string valueName, byte[] value)
        {
            string xmlText = SerializeValue(valueName, value);
            AddXmlObject(node, xmlText);
        }

        public static void AddValue(XmlNode node, string valueName, CultureInfo value)
        {
            int cultureLCID = (value == null) ? 0 : value.LCID;
            AddValue(node, valueName, cultureLCID);
        }

        /// <summary>
        /// В указанную ноду добавляется объект способный к xml сериализации под указанным именем
        /// </summary>
        /// <param name="node"></param>
        /// <param name="valueName"></param>
        /// <param name="value"></param>
        //public static void AddValue(XmlNode node, string valueName, ISerializeXml value)
        //{
        //    string xmlText = (value == null) ? SerializeValue(valueName, C_NULL_STR) : value.SerializeXml(valueName);
        //    AddXmlObject(node, xmlText);
        //}

        /// <summary>
        /// В указанную ноду добавляется xml сериализованный объект
        /// </summary>
        /// <param name="node"></param>
        /// <param name="xmlObjectSource"></param>
        public static void AddXmlObject(XmlNode node, string xmlObjectSource)
        {
            if (string.IsNullOrEmpty(xmlObjectSource))
                return;
            XmlDocument doc = null;
            XmlNode currentNode = null;
            if (node.NodeType == XmlNodeType.Document)
            {
                doc = node as XmlDocument;
                currentNode = doc.FirstChild;
            }
            else
            {
                doc = node.OwnerDocument;
                currentNode = node;
            }

            XmlDocument tmpDocum = new XmlDocument();
            tmpDocum.LoadXml(xmlObjectSource);

            XmlElement newElement = doc.CreateElement(tmpDocum.FirstChild.Name);
            newElement.InnerXml = tmpDocum.FirstChild.InnerXml;
            foreach (XmlAttribute attr in tmpDocum.FirstChild.Attributes)
            {
                XmlAttribute newAttr = doc.CreateAttribute(attr.Name);
                newAttr.InnerText = attr.InnerText;
                newElement.Attributes.Append(newAttr);
            }
            currentNode.AppendChild(newElement);
        }

        /// <summary>
        /// В указанную ноду добавляется именованный список xml сериализованных объектов.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="listName"></param>
        /// <param name="listOfXmlObjects"></param>
        public static void AddListOfObjectsXmlSerialized(XmlNode node, string listNodeName, IEnumerable<string> listOfXmlObjects)
        {
            if (listOfXmlObjects != null)
                AddXmlObject(node, SerializeListOfXmlObjects(listNodeName, listOfXmlObjects));
        }

        /// <summary>
        /// В указанную ноду добавляется именованный список xml сериализованных объектов.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="listNodeName"></param>
        /// <param name="listOfXmlObjects"></param>
        public static void AddListOfObjects(XmlNode node, string listNodeName, IEnumerable<ISerializeXml> listOfXmlObjects)
        {
            if (listOfXmlObjects != null)
            {
                List<string> list = new List<string>();
                foreach (ISerializeXml serialXmlObj in listOfXmlObjects)
                    list.Add(serialXmlObj.SerializeXml("Record"));
                AddListOfObjectsXmlSerialized(node, listNodeName, list);
            }
        }

        public static void AddListOfObjects(XmlNode node, string listNodeName, IEnumerable<int> listOfIntObj)
        {
            if (listOfIntObj != null)
            {
                List<string> list = new List<string>();
                foreach (int intValue in listOfIntObj)
                    list.Add(SerializeValue("Record", intValue));
                AddListOfObjectsXmlSerialized(node, listNodeName, list);
            }
        }

        public static void AddListOfObjects(XmlNode node, string listNodeName, object iEnumerableListOfEnum, Enum enumExample)
        {
            IEnumerable listOfEnum = iEnumerableListOfEnum as IEnumerable;
            if (listOfEnum != null)
            {
                List<string> list = new List<string>();
                foreach (object enumValue in listOfEnum)
                {
                    IConvertible iconvert = enumValue as IConvertible;
                    if (iconvert != null)
                        list.Add(SerializeValue("Record", Convert.ToInt32(enumValue)));
                }
                AddListOfObjectsXmlSerialized(node, listNodeName, list);
            }
        }

        public static void AddListOfObjects(XmlNode node, string listNodeName, IEnumerable<string> listOfStringObj)
        {
            if (listOfStringObj != null)
            {
                List<string> list = new List<string>();
                foreach (string strValue in listOfStringObj)
                    list.Add(SerializeValue("Record", strValue));
                AddListOfObjectsXmlSerialized(node, listNodeName, list);
            }
        }

        #endregion

        #region Deserialize

        public static string GetValueStr(XmlNode parentNode, string elementName)
        {
            return GetValueStr(parentNode, elementName, string.Empty);
        }

        public static string GetValueStr(XmlNode parentNode, string elementName, string defaultValue)
        {
            if (parentNode == null)
                return defaultValue;
            XmlElement element = FindElement(parentNode, elementName);
            if (element == null)
                return defaultValue;
            XmlAttribute attr = FindAttribute(element, "Value");
            if (attr == null)
                return defaultValue;
            return attr.InnerText;
        }

        public static string GetValueStr(string simpleElementSource)
        {
            if (string.IsNullOrEmpty(simpleElementSource))
                return string.Empty;
            XmlNode rootNode = LoadXmlDocum(simpleElementSource).FirstChild;
            return GetValueStr(rootNode, rootNode.Name);
        }

        public static bool? GetValueBool(XmlNode parentNode, string elementName, bool defaultValue)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
            {
                return null;
            }
            else
            {
                bool retVal = defaultValue;
                bool success = bool.TryParse(value, out retVal);
                if (success)
                {
                    return retVal;
                }
                else
                {
                    int boolValueInt = -1;
                    success = int.TryParse(value, out boolValueInt);
                    if (success)
                    {
                        if (boolValueInt == 1)
                            return true;
                        else if (boolValueInt == 0)
                            return false;
                    }
                    return defaultValue;
                }
            }
        }

        public static bool? GetValueBool(XmlNode parentNode, string elementName)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
                return null;
            else
                return ConvertHelper.ConvertToBool(value);
        }

        public static bool? GetValueBool(string simpleElementSource)
        {
            if (string.IsNullOrEmpty(simpleElementSource))
                return null;
            XmlNode rootNode = LoadXmlDocum(simpleElementSource).FirstChild;
            return GetValueBool(rootNode, rootNode.Name);
        }

        public static int? GetValueInt(XmlNode parentNode, string elementName, int defaultValue)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
            {
                return null;
            }
            else
            {
                int retVal = defaultValue;
                bool success = int.TryParse(value, out retVal);
                if (success)
                    return retVal;
                else
                    return defaultValue;
            }
        }

        public static int? GetValueInt(string simpleElementSource, int defaultValue)
        {
            if (string.IsNullOrEmpty(simpleElementSource))
                return defaultValue;
            XmlNode rootNode = LoadXmlDocum(simpleElementSource).FirstChild;
            return GetValueInt(rootNode, rootNode.Name, defaultValue);
        }

        public static uint? GetValueUInt(XmlNode parentNode, string elementName, uint defaultValue)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
            {
                return null;
            }
            else
            {
                uint retVal = defaultValue;
                bool success = uint.TryParse(value, out retVal);
                if (success)
                    return retVal;
                else
                    return defaultValue;
            }
        }
        public static Single? GetValueSingle(XmlNode parentNode, string elementName, Single defaultValue)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
            {
                return null;
            }
            else
            {
                Single retVal = defaultValue;
                bool success = Single.TryParse(value, out retVal);
                if (success)
                    return retVal;
                else
                    return defaultValue;
            }
        }

        public static uint? GetValueUInt(string simpleElementSource, uint defaultValue)
        {
            if (string.IsNullOrEmpty(simpleElementSource))
                return defaultValue;
            XmlNode rootNode = LoadXmlDocum(simpleElementSource).FirstChild;
            return GetValueUInt(rootNode, rootNode.Name, defaultValue);
        }

        public static long? GetValueLong(XmlNode parentNode, string elementName, long defaultValue)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
            {
                return null;
            }
            else
            {
                long retVal = defaultValue;
                bool success = long.TryParse(value, out retVal);
                if (success)
                    return retVal;
                else
                    return defaultValue;
            }
        }
        public static short? GetValueShort(XmlNode parentNode, string elementName, short defaultValue)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
            {
                return null;
            }
            else
            {
                short retVal = defaultValue;
                bool success = short.TryParse(value, out retVal);
                if (success)
                    return retVal;
                else
                    return defaultValue;
            }
        }
        public static ushort? GetValueUShort(XmlNode parentNode, string elementName, ushort defaultValue)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
            {
                return null;
            }
            else
            {
                ushort retVal = defaultValue;
                bool success = ushort.TryParse(value, out retVal);
                if (success)
                    return retVal;
                else
                    return defaultValue;
            }
        }
        public static long? GetValueLong(string simpleElementSource, long defaultValue)
        {
            if (string.IsNullOrEmpty(simpleElementSource))
                return null;
            XmlNode rootNode = LoadXmlDocum(simpleElementSource).FirstChild;
            return GetValueLong(rootNode, rootNode.Name, defaultValue);
        }

        public static ulong? GetValueULong(XmlNode parentNode, string elementName, ulong defaultValue)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
            {
                return null;
            }
            else
            {
                ulong retVal = defaultValue;
                bool success = ulong.TryParse(value, out retVal);
                if (success)
                    return retVal;
                else
                    return defaultValue;
            }
        }

        public static ulong? GetValueULong(string simpleElementSource, ulong defaultValue)
        {
            if (string.IsNullOrEmpty(simpleElementSource))
                return null;
            XmlNode rootNode = LoadXmlDocum(simpleElementSource).FirstChild;
            return GetValueULong(rootNode, rootNode.Name, defaultValue);
        }

        public static double? GetValueDouble(XmlNode parentNode, string elementName, double defaultValue)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
                return null;
            else
                return ConvertHelper.ConvertToDouble(value, defaultValue);
        }

        public static double? GetValueDouble(string simpleElementSource, double defaultValue)
        {
            if (string.IsNullOrEmpty(simpleElementSource))
                return defaultValue;
            XmlNode rootNode = LoadXmlDocum(simpleElementSource).FirstChild;
            return GetValueDouble(rootNode, rootNode.Name, defaultValue);
        }

        public static byte[] GetValueByteArray(XmlNode parentNode, string elementName)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
            {
                return null;
            }
            else
            {
                if (string.IsNullOrEmpty(value))
                    return new byte[0];
                byte[] compressedData = BinaryHelper.HexStrToByteArray(value);
                if (compressedData == null || compressedData.Length < 2)
                    return compressedData;
                byte[] uncompressedData = BinaryHelper.Decompress(compressedData);
                return uncompressedData;
            }
        }

        public static byte[] GetValueByteArray(string simpleElementSource)
        {
            if (string.IsNullOrEmpty(simpleElementSource))
                return null;
            XmlNode rootNode = LoadXmlDocum(simpleElementSource).FirstChild;
            return GetValueByteArray(rootNode, rootNode.Name);
        }

        public static DateTime? GetValueDateTime(XmlNode parentNode, string elementName, DateTime defaultValue)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
                return null;
            else
                return ConvertHelper.ConvertToDate(value, defaultValue);
        }

        public static DateTime? GetValueDateTime(string simpleElementSource, DateTime defaultValue)
        {
            if (string.IsNullOrEmpty(simpleElementSource))
                return defaultValue;
            XmlNode rootNode = LoadXmlDocum(simpleElementSource).FirstChild;
            return GetValueDateTime(rootNode, rootNode.Name, defaultValue);
        }

        public static TimeSpan? GetValueTimeSpan(XmlNode parentNode, string elementName)
        {
            return convertToTimeSpan(GetValueLong(parentNode, elementName, 0));
        }

        public static TimeSpan? GetValueTimeSpan(string simpleElementSource)
        {
            return convertToTimeSpan(GetValueLong(simpleElementSource, 0));
        }

        public static Guid? GetValueGuid(XmlNode parentNode, string elementName)
        {
            string value = GetValueStr(parentNode, elementName);
            if (string.Compare(value, C_NULL_STR, true) == 0)
            {
                return null;
            }
            else
            {
                Guid retVal = Guid.Empty;
                bool success = Guid.TryParseExact(value, "D", out retVal);
                if (!success)
                    success = Guid.TryParse(value, out retVal);
                if (success)
                    return retVal;
                else
                    return Guid.Empty;
            }
        }

        public static Guid? GetValueGuid(string simpleElementSource)
        {
            if (string.IsNullOrEmpty(simpleElementSource))
                return Guid.Empty;
            XmlNode rootNode = LoadXmlDocum(simpleElementSource).FirstChild;
            return GetValueGuid(rootNode, rootNode.Name);
        }

        public static CultureInfo GetValueCultureInfo(XmlNode parentNode, string elementName)
        {
            return convertToCulture(GetValueInt(parentNode, elementName, 0));
        }

        public static CultureInfo GetValueCultureInfo(string simpleElementSource)
        {
            return convertToCulture(GetValueInt(simpleElementSource, 0));
        }

        private static CultureInfo convertToCulture(int? LCID)
        {
            CultureInfo retVal = new CultureInfo("en");
            if (LCID != null && LCID.Value > 0)
                retVal = new CultureInfo(LCID.Value);
            return retVal;
        }


        /// <summary>
        /// Возвращает xml сериализованный объект, который ищется в указанной ноде (parentNode) по имени (elementName)
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public static string GetXmlObject(XmlNode parentNode, string elementName)
        {
            if (parentNode == null)
                return string.Empty;
            XmlElement element = FindElement(parentNode, elementName);
            if (element == null)
                return string.Empty;
            return SerializeXml(element);
        }

        public static List<int> GetValuesIntList(XmlNode parentNode, string groupElementName)
        {
            List<int> retVal = new List<int>();
            List<string> xmlStrList = GetListXmlObjects(parentNode, groupElementName);
            foreach (string xmlSource in xmlStrList)
                retVal.Add(Xml_Helper.GetValueInt(xmlSource, 0).Value);
            return retVal;
        }

        public static List<string> GetValuesStringList(XmlNode parentNode, string groupElementName)
        {
            List<string> retVal = new List<string>();
            List<string> xmlStrList = GetListXmlObjects(parentNode, groupElementName);
            foreach (string xmlSource in xmlStrList)
                retVal.Add(Xml_Helper.GetValueStr(xmlSource));
            return retVal;
        }

        /// <summary>
        /// Возвращает список xml сериализованных объектов из указанного "кустового" элемента
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public static List<string> GetListXmlObjects(XmlNode parentNode, string groupElementName)
        {
            int count = Xml_Helper.GetCount(parentNode, groupElementName);
            XmlElement listElement = Xml_Helper.FindElement(parentNode, groupElementName);
            if (count > 0 && listElement != null)
                return Xml_Helper.GetAllObjectsAsXmlStrings(listElement);
            else
                return new List<string>();
        }

        #endregion

        #region Private methods

        private static XmlAttribute FindAttribute(XmlElement element, string attrName)
        {
            if (element == null)
                return null;

            foreach (XmlAttribute attr in element.Attributes)
            {
                if (string.Compare(attr.Name, attrName, true) == 0)
                    return attr;
            }
            return null;
        }

        private static string serializeSimpleValue(string newElementName, string value)
        {
            XmlDocument tmpDocum = new XmlDocument();
            XmlElement newElement = tmpDocum.CreateElement(newElementName);
            XmlAttribute attr1 = tmpDocum.CreateAttribute("Value");
            attr1.InnerText = value;
            newElement.Attributes.Append(attr1);
            return SerializeXml(newElement);
        }

        private static void appendElement(XmlNode node, string newElementName, string value)
        {
            XmlDocument doc = null;
            XmlNode currentNode = null;
            if (node.NodeType == XmlNodeType.Document)
            {
                doc = node as XmlDocument;
                currentNode = doc.FirstChild;
            }
            else
            {
                doc = node.OwnerDocument;
                currentNode = node;
            }
            XmlElement newElement = doc.CreateElement(newElementName);
            XmlAttribute attr1 = doc.CreateAttribute("Value");
            attr1.InnerText = value;
            newElement.Attributes.Append(attr1);
            currentNode.AppendChild(newElement);
        }

        private static List<string> GetAllObjectsAsXmlStrings(XmlNode listNode)
        {
            List<string> retVal = new List<string>();

            if (listNode == null)
                return retVal;

            foreach (XmlNode node in listNode.ChildNodes)
            {
                XmlElement element = node as XmlElement;
                if (element != null)
                    retVal.Add(SerializeXml(element));
            }
            return retVal;
        }

        private static TimeSpan? convertToTimeSpan(long? milliSeconds)
        {
            if (milliSeconds.HasValue)
                return new TimeSpan(TimeSpan.TicksPerMillisecond * milliSeconds.Value);
            else
                return null;
        }

        #endregion

        #region OBSOLETE

        /// <summary>
        /// OBSOLETE - use method XmlHelper.AddValue instead it
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="xmlNode"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        public static void AppendAttribute(XmlDocument xmlDoc, XmlElement xmlNode, string attributeName, object value)
        {
            XmlAttribute attrib = xmlDoc.CreateAttribute(attributeName);
            attrib.Value = value != null ? value.ToString() : string.Empty;
            xmlNode.Attributes.Append(attrib);
        }

        /// <summary>
        /// OBSOLETE - use method XmlHelper.GetValueStr, XmlHelper.GetValueInt and so on
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="attributeName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetAttributeValue(XmlNode xmlNode, string attributeName, string defaultValue)
        {
            string value = defaultValue;
            for (int i = 0; i < xmlNode.Attributes.Count; i++)
            {
                if (xmlNode.Attributes[i].Name == attributeName)
                {
                    value = xmlNode.Attributes[i].Value;
                    break;
                }
            }

            return value;
        }

        #endregion

    }
    public interface ISerializeXml
    {
        string SerializeXml(string title);
        void DeserializeXml(string source);
    }
    public class StringWriterWithEncoding : StringWriter
    {
        #region Constructors
        public StringWriterWithEncoding() : base()
        {
        }

        public StringWriterWithEncoding(Encoding encoding) : base()
        {
            m_encoding = encoding;
        }

        public StringWriterWithEncoding(IFormatProvider formatProvider, Encoding encoding) : base(formatProvider)
        {
            m_encoding = encoding;
        }

        public StringWriterWithEncoding(StringBuilder sb, Encoding encoding) : base(sb)
        {
            m_encoding = encoding;
        }

        public StringWriterWithEncoding(StringBuilder sb, IFormatProvider formatProvider, Encoding encoding) : base(sb, formatProvider)
        {
            m_encoding = encoding;
        }
        #endregion

        #region Encoding property

        private Encoding m_encoding = null;
        public override Encoding Encoding
        {
            get { return (m_encoding == null) ? base.Encoding : m_encoding; }
        }

        #endregion
    }
}

