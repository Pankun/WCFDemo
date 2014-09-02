using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using LocalAppLog;

namespace SongDAO
{
    /// <summary>
    /// Album Data Access Object
    /// </summary>
    public class AlbumApp
    {
        
        private AppSettings theAppSettings = new AppSettings();
        private LogApp TheLog = new LogApp();
        private string ApplicationName = "SongDAO";
        DAOUtl TheUtility = new DAOUtl();
        public FunctionReturnObject AlbumAppGetInfoByTitle(string TheTitle)
        {
            FunctionReturnObject theReturnObject = new FunctionReturnObject();
            string xmlString = "";
            TheLog.Addlog("Application=" + ApplicationName + " ||Function=AlbumAppGetInfoByTitle ||TheTitle=" + TheTitle);
            try
            {
                XElement xelement = XElement.Load(theAppSettings.theXMLSourceFile);
                IEnumerable<XElement> album = from el in xelement.Descendants("album")
                                              where (string)el.Attribute("title") == TheUtility.Escape(ref TheTitle)
                                              select el;
                foreach (XElement el in album)
                    xmlString = xmlString + el.ToString();



                XmlSerializer serializer = new XmlSerializer(typeof(album));
                album output;

                using (StringReader reader = new StringReader(xmlString))
                {
                    output = (album)serializer.Deserialize(reader);
                }

                theReturnObject.ReturnObject = output;
                //theReturnObject.ReturnMessage = xmlString;
                theReturnObject.ReturnFlag = true;                
            }
            catch (Exception ex)
            {
                theReturnObject.ReturnFlag = false;
                theReturnObject.ReturnMessage = ex.Message;
                TheLog.Addlog("Application=" + ApplicationName + " ||Function=AlbumAppGetInfoByTitle ||TheTitle=" + TheTitle + "||Error=" + ex.Message);
            }            

            return theReturnObject;
        }


        
    }
}
