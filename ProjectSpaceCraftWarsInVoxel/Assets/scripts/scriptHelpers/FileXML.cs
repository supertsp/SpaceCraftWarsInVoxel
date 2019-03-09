using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

/// <license>
/// Copyright (C) 2016-05-02 Tiago Penha Pedroso
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
/// </license>
/// 

// How can I use Resources? See below...
//textAsset = Resources.Load(fileNameWithoutExtensionFromResources) as TextAsset;
//xml.Load(new StringReader(textAsset.text));

public class FileXML
{

    private XmlDocument xml;
    private string fileName;

    private string _filePath;
    public string CurrentFilePath
    {
        get { return _filePath; }
    }

    private string firstElementName;
    private string firstAttributeName;
    private string firstAttributeValue;


    #region Constructors
    public FileXML(string fileNameWithoutExtension)
    {
        xml = new XmlDocument();
        fileName = fileNameWithoutExtension;
        _filePath = Application.persistentDataPath + "/" + fileName + ".xml";

        try
        {
            xml.Load(CurrentFilePath);
        }
        catch (Exception e)
        {
            string m = e.Message;
            XmlDeclaration initialDoc = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(initialDoc);

            XmlElement firstElement = xml.CreateElement("unityapplication");
            firstElement.SetAttribute("identifier", "" + Application.identifier);
            xml.AppendChild(firstElement);

            SaveAndWriteXMLFile();
        }
    }

    public FileXML(string fileNameWithoutExtension, string firstElementName)
    {
        xml = new XmlDocument();
        fileName = fileNameWithoutExtension;
        _filePath = Application.persistentDataPath + "/" + fileName + ".xml";
        this.firstElementName = firstElementName;

        try
        {
            xml.Load(CurrentFilePath);
        }
        catch (Exception e)
        {
            string m = e.Message;
            XmlDeclaration initialDoc = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(initialDoc);

            XmlElement firstElement = xml.CreateElement(firstElementName);
            xml.AppendChild(firstElement);

            SaveAndWriteXMLFile();
        }
    }

    public FileXML(string fileNameWithoutExtension, string firstElementName, string firstAttributeName, string firstAttributeValue)
    {
        xml = new XmlDocument();
        fileName = fileNameWithoutExtension;
        _filePath = Application.persistentDataPath + "/" + fileName + ".xml";
        this.firstElementName = firstElementName;
        this.firstAttributeName = firstAttributeName;
        this.firstAttributeValue = firstAttributeValue;

        try
        {
            xml.Load(CurrentFilePath);
        }
        catch (Exception e)
        {
            string m = e.Message;
            XmlDeclaration initialDoc = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(initialDoc);

            XmlElement firstElement = xml.CreateElement(firstElementName);
            firstElement.SetAttribute(firstAttributeName, firstAttributeValue);
            xml.AppendChild(firstElement);

            SaveAndWriteXMLFile();
        }
    }
    #endregion

    private void SaveAndWriteXMLFile()
    {
        xml.Save(CurrentFilePath);
    }

    public void DeleteAllXMLElements()
    {
        xml = new XmlDocument();
        XmlDeclaration initialDoc = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
        xml.AppendChild(initialDoc);

        if (firstElementName != null && firstAttributeName != null && firstAttributeValue != null)
        {
            XmlElement firstElement = xml.CreateElement(firstElementName);
            firstElement.SetAttribute(firstAttributeName, firstAttributeValue);
            xml.AppendChild(firstElement);
        }
        else if (firstElementName != null)
        {
            XmlElement firstElement = xml.CreateElement(firstElementName);
            xml.AppendChild(firstElement);
        }
        else
        {
            XmlElement firstElement = xml.CreateElement("unityapplication");
            firstElement.SetAttribute("identifier", "" + Application.identifier);
            xml.AppendChild(firstElement);
        }

        SaveAndWriteXMLFile();
    }

    #region Elements Methods: GetElementsByTagName, GetAttributeValuesByElementsTagName, GetInnerTextValuesByElementsTagName, GetElementByTagNameAndIndex, GetElementByID
    public XmlNode[] GetElementsByTagName(string nodeName)
    {
        XmlNodeList nodeList = xml.GetElementsByTagName(nodeName);
        return ConvertXmlNodeListToNodeArray(nodeList);
    }

    public List<string> GetAttributeValuesByElementsTagName(string nodeName, int attributeIndex)
    {
        List<string> values = new List<string>();
        XmlNode[] nodes = GetElementsByTagName(nodeName);


        foreach (var item in nodes)
        {
            try
            {
                values.Add("" + item.Attributes.Item(attributeIndex).Value);
            }
            catch (Exception e)
            {
                string m = e.Message;
            }
        }

        return values;
    }

    public List<string> GetInnerTextValuesByElementsTagName(string nodeName)
    {
        List<string> values = new List<string>();
        XmlNode[] nodes = GetElementsByTagName(nodeName);

        foreach (var item in nodes)
        {
            values.Add("" + item.InnerText);
        }

        return values;
    }

    public XmlNode[] GetElementsByTagName(XmlNode initialNode, string nodeName)
    {
        List<XmlNode> nodeList = ConvertXmlNodeListToList(initialNode.ChildNodes);

        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].LocalName != nodeName)
            {
                nodeList.RemoveAt(i);
            }
        }

        return ConvertListToNodeArray(nodeList);
    }

    public XmlNode GetElementByTagNameAndIndex(string nodeName, int index)
    {
        return GetElementsByTagName(nodeName)[index];
    }

    public XmlNode GetElementByID(string id)
    {
        return xml.GetElementById(id);
    }
    #endregion

    #region Children Methods: GetChildrenByParentTagNameAndIndex, GetChildrenByTagName
    public XmlNode[] GetChildrenByParentTagNameAndIndex(string nodeName, int index)
    {
        XmlNode node = GetElementByTagNameAndIndex(nodeName, index);
        return ConvertXmlNodeListToNodeArray(node.ChildNodes);
    }

    public XmlNode[] GetChildrenByTagName(string parentNodeName, int parentIndex, string childNodeName)
    {
        List<XmlNode> nodeList = ConvertNodeArrayToList(
            GetChildrenByParentTagNameAndIndex(parentNodeName, parentIndex)
        );

        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].LocalName != childNodeName)
            {
                nodeList.RemoveAt(i);
            }
        }

        XmlNode[] cNodes;

        if (nodeList.Count > 0)
        {
            cNodes = new XmlNode[nodeList.Count];

            for (int i = 0; i < nodeList.Count; i++)
            {
                cNodes[i] = nodeList[i];
            }

            return cNodes;
        }

        return default(XmlNode[]);
    }
    #endregion

    #region Auxiliary Methods: ConvertNodeListToNodeArray, ConvertNodeArrayToList
    public XmlNode[] ConvertXmlNodeListToNodeArray(XmlNodeList nodeList)
    {
        XmlNode[] nodes = new XmlNode[nodeList.Count];

        for (int i = 0; i < nodeList.Count; i++)
        {
            nodes[i] = nodeList.Item(i);
        }

        return nodes;
    }

    public List<XmlNode> ConvertXmlNodeListToList(XmlNodeList nodeList)
    {
        List<XmlNode> nodes = new List<XmlNode>();

        foreach (XmlNode node in nodeList)
        {
            nodes.Add(node);
        }

        return nodes;
    }

    public List<XmlNode> ConvertNodeArrayToList(XmlNode[] nodeArray)
    {
        List<XmlNode> nodeList = new List<XmlNode>();

        foreach (XmlNode item in nodeArray)
        {
            nodeList.Add(item);
        }

        return nodeList;
    }

    public XmlNode[] ConvertListToNodeArray(List<XmlNode> nodeList)
    {
        XmlNode[] nodes = new XmlNode[nodeList.Count];

        for (int i = 0; i < nodeList.Count; i++)
        {
            nodes[i] = nodeList[i];
        }

        return nodes;
    }
    #endregion

    #region AddElement()

    public void AddElement(string elementName)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        XmlNode root = xml.DocumentElement;
        root.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }

    #region InnerText
    public void AddElement(string elementName, string innerTextStringValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.InnerText = innerTextStringValue;

        XmlNode root = xml.DocumentElement;
        root.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }

    public void AddElement(string elementName, float innerTextFloatValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.InnerText = "" + innerTextFloatValue;

        XmlNode root = xml.DocumentElement;
        root.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }

    public void AddElement(string elementName, int innerTextIntValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.InnerText = "" + innerTextIntValue;

        XmlNode root = xml.DocumentElement;
        root.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }
    #endregion

    #region Attribute Values
    public void AddElement(string elementName, string attributeName, string attributeValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.SetAttribute(attributeName, attributeValue);

        XmlNode root = xml.DocumentElement;
        root.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }

    public void AddElement(string elementName, string attributeName, float attributeValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.SetAttribute(attributeName, "" + attributeValue);

        XmlNode root = xml.DocumentElement;
        root.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }

    public void AddElement(string elementName, string attributeName, int attributeValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.SetAttribute(attributeName, "" + attributeValue);

        XmlNode root = xml.DocumentElement;
        root.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }
    #endregion

    #endregion

    #region AddElementBefore()

    public void AddElementBefore(string elementName)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        XmlNode root = xml.DocumentElement;
        root.InsertBefore(newElement, root.FirstChild);
        SaveAndWriteXMLFile();
    }

    #region InnerText
    public void AddElementBefore(string elementName, string innerTextStringValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.InnerText = innerTextStringValue;

        XmlNode root = xml.DocumentElement;
        root.InsertBefore(newElement, root.FirstChild);
        SaveAndWriteXMLFile();
    }

    public void AddElementBefore(string elementName, float innerTextFloatValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.InnerText = "" + innerTextFloatValue;

        XmlNode root = xml.DocumentElement;
        root.InsertBefore(newElement, root.FirstChild);
        SaveAndWriteXMLFile();
    }

    public void AddElementBefore(string elementName, int innerTextIntValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.InnerText = "" + innerTextIntValue;

        XmlNode root = xml.DocumentElement;
        root.InsertBefore(newElement, root.FirstChild);
        SaveAndWriteXMLFile();
    }
    #endregion

    #region Attribute Values
    public void AddElementBefore(string elementName, string attributeName, string attributeValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.SetAttribute(attributeName, attributeValue);

        XmlNode root = xml.DocumentElement;
        root.InsertBefore(newElement, root.FirstChild);
        SaveAndWriteXMLFile();
    }

    public void AddElementBefore(string elementName, string attributeName, float attributeValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.SetAttribute(attributeName, "" + attributeValue);

        XmlNode root = xml.DocumentElement;
        root.InsertBefore(newElement, root.FirstChild);
        SaveAndWriteXMLFile();
    }

    public void AddElementBefore(string elementName, string attributeName, int attributeValue)
    {
        XmlElement newElement = xml.CreateElement(elementName);
        newElement.SetAttribute(attributeName, "" + attributeValue);

        XmlNode root = xml.DocumentElement;
        root.InsertBefore(newElement, root.FirstChild);
        SaveAndWriteXMLFile();
    }
    #endregion

    #endregion
    
    #region AddElementAsChildOf()

    #region InnerText
    public void AddElementAsChildOf(string parentElementName, string newElementName, string innerTextStringValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.InnerText = innerTextStringValue;

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }

    public void AddElementAsChildOf(string parentElementName, string newElementName, float innerTextFloatValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.InnerText = "" + innerTextFloatValue;

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }

    public void AddElementAsChildOf(string parentElementName, string newElementName, int innerTextIntValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.InnerText = "" + innerTextIntValue;

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }
    #endregion

    #region Attribute Values
    public void AddElementAsChildOf(string parentElementName, string newElementName, string attributeName, string attributeValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.SetAttribute(attributeName, attributeValue);

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }

    public void AddElementAsChildOf(string parentElementName, string newElementName, string attributeName, float attributeValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.SetAttribute(attributeName, "" + attributeValue);

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }

    public void AddElementAsChildOf(string parentElementName, string newElementName, string attributeName, int attributeValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.SetAttribute(attributeName, "" + attributeValue);

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.AppendChild(newElement);
        SaveAndWriteXMLFile();
    }
    #endregion

    #endregion

    #region AddElementBeforeAsChildOf()

    #region InnerText
    public void AddElementBeforeAsChildOf(string parentElementName, string newElementName, string innerTextStringValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.InnerText = innerTextStringValue;

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.InsertBefore(newElement, parent.FirstChild);
        SaveAndWriteXMLFile();
    }

    public void AddElementBeforeAsChildOf(string parentElementName, string newElementName, float innerTextFloatValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.InnerText = "" + innerTextFloatValue;

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.InsertBefore(newElement, parent.FirstChild);
        SaveAndWriteXMLFile();
    }

    public void AddElementBeforeAsChildOf(string parentElementName, string newElementName, int innerTextIntValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.InnerText = "" + innerTextIntValue;

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.InsertBefore(newElement, parent.FirstChild);
        SaveAndWriteXMLFile();
    }
    #endregion

    #region Attribute Values
    public void AddElementBeforeAsChildOf(string parentElementName, string newElementName, string attributeName, string attributeValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.SetAttribute(attributeName, attributeValue);

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.InsertBefore(newElement, parent.FirstChild);
        SaveAndWriteXMLFile();
    }

    public void AddElementBeforeAsChildOf(string parentElementName, string newElementName, string attributeName, float attributeValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.SetAttribute(attributeName, "" + attributeValue);

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.InsertBefore(newElement, parent.FirstChild);
        SaveAndWriteXMLFile();
    }

    public void AddElementBeforeAsChildOf(string parentElementName, string newElementName, string attributeName, int attributeValue)
    {
        XmlElement newElement = xml.CreateElement(newElementName);
        newElement.SetAttribute(attributeName, "" + attributeValue);

        XmlNode parent = GetElementByTagNameAndIndex(parentElementName, 0);
        parent.InsertBefore(newElement, parent.FirstChild);
        SaveAndWriteXMLFile();
    }
    #endregion

    #endregion

}