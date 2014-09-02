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
    /// 
    /// </summary>
    public class SongApp
    {
        AppSettings theAppSettings = new AppSettings();
        LogApp TheLog = new LogApp();
        string ApplicationName = "SongDAO";
        DAOUtl TheUtility = new DAOUtl();
        /// <summary>
        /// Add a song into XML data source
        /// </summary>
        /// <param name="TheArtist">Artist Name</param>
        /// <param name="TheAlbum">Album Title</param>
        /// <param name="TheTitle">Song Title</param>
        /// <param name="TheLength">Song Length</param>
        /// <returns>
        /// FunctionReturnObject. 
        /// (System message will be stored in ReturnMessage property.)
        /// </returns>
        /// <remarks>
        /// When artist/album are new, the artist/album will also be created in XML data source.
        /// </remarks>
        ///
        public FunctionReturnObject AddSong(string TheArtist, string TheAlbum,string TheTitle,string TheLength)
        {
            FunctionReturnObject theReturnObject = new FunctionReturnObject();
            TheLog.Addlog("Application=" + ApplicationName + " ||Function=AddSong ||TheTitle=" + TheTitle);
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(theAppSettings.theXMLSourceFile);
                Boolean NewParentFlag = false;                
                string TheXPath;
                string TheXPath01;
                XmlElement xNewChild;
                XmlNode xElt;
                XmlNode xElt01;
                string AlbumId="";
                string SongId="";

                //Validate the strings 
                TheUtility.Escape(ref TheArtist);
                TheUtility.Escape(ref TheAlbum);
                TheUtility.Escape(ref TheTitle);
                TheUtility.Escape(ref TheLength);

                //XmlNode commonParent;
                // Start to check ---
                // Check artist tag -----
                TheXPath = "//artist[@name=\"" + TheArtist + "\"]";
                xElt = xDoc.SelectSingleNode(TheXPath);
                if (xElt == null)
                {
                    NewParentFlag = true;
                    xNewChild = xDoc.CreateElement("artist");
                    xNewChild.SetAttribute("name", TheArtist);
                    xNewChild.InnerText = "";
                    //commonParent = xElt.ParentNode;
                    //commonParent.InsertAfter(xNewChild, xElt);
                    xDoc.DocumentElement.InsertAfter(xNewChild, xElt);
                    xDoc.Save(theAppSettings.theXMLSourceFile);
                    xElt = xDoc.SelectSingleNode(TheXPath);
                }
                // check Album -----

                TheXPath01 = "//artist[@name=\"" + TheArtist + "\"]//album[@title=\"" + TheAlbum + "\"]";
                xElt01 = xDoc.SelectSingleNode(TheXPath01);
                if (xElt01 == null)
                {
                    do
                    {
                        AlbumId = TheUtility.Gen_Key(6);
                        TheXPath01 = "//artist[@name=\"" + TheArtist + "\"]//album[@Id=\"" + AlbumId + "\"]";
                        xElt01 = xDoc.SelectSingleNode(TheXPath01);
                    } while (xElt01 != null);

                    xNewChild = xDoc.CreateElement("album");
                    xNewChild.SetAttribute("title", TheAlbum);
                    xNewChild.SetAttribute("Id", AlbumId);
                    xNewChild.InnerText = "";

                    xElt.AppendChild(xNewChild);
                    xDoc.Save(theAppSettings.theXMLSourceFile);
                    NewParentFlag = true;
                }
                if (!NewParentFlag)
                {
                    TheXPath = "//artist[@name=\"" + TheArtist + "\"]//album[@title=\"" + TheAlbum + "\"]//song[@title=\"" + TheTitle + "\"]";
                    xElt = xDoc.SelectSingleNode(TheXPath);
                    if (xElt != null)
                    {
                        throw new Exception("The song exists. ||TheXPath=" + TheXPath);
                    }
                }

                //===========================================
                TheXPath = "//artist[@name=\"" + TheArtist + "\"]//album[@title=\"" + TheAlbum + "\"]";
                
                xElt = xDoc.SelectSingleNode(TheXPath);
                if (xElt == null)
                {
                    throw new Exception("XPath not found. ||TheXPath=" + TheXPath);
                }

                do
                {
                    SongId = TheUtility.Gen_Key(6);
                    TheXPath01 = "//artist[@name=\"" + TheArtist + "\"]//album[@title=\"" + TheAlbum + "\"]//song[@SongId=\"" + SongId + "\"]";
                    xElt01 = xDoc.SelectSingleNode(TheXPath01);
                } while (xElt01 != null);

                xNewChild = xDoc.CreateElement("song");
                xNewChild.SetAttribute("title", TheTitle);
                xNewChild.SetAttribute("length", TheLength);
                xNewChild.SetAttribute("SongId", SongId);
                xElt.AppendChild(xNewChild);
                xDoc.Save(theAppSettings.theXMLSourceFile);
                //XElement xEle = XElement.Load(theAppSettings.theXMLSourceFile);

                //xEle.Add(new XElement("artist", new XAttribute("name", TheArtist),
                //    new XElement("album", new XAttribute("title", TheAlbum), new XElement("song", new XAttribute("title", TheTitle), new XAttribute("length", TheLength)))));


                //xEle.Save(theAppSettings.theXMLSourceFile);
                theReturnObject.ReturnMessage = "Added successfully! ||AlbumId=" + AlbumId + "||SongId=" + SongId;
                theReturnObject.ReturnFlag = true;
            }
            catch (Exception ex)
            {
                theReturnObject.ReturnFlag = false;
                theReturnObject.ReturnMessage = ex.Message;
                TheLog.Addlog("Application=" + ApplicationName + " ||Function=AddSong ||TheArtist=" + TheArtist + "||Error=" + ex.Message);
            }

            return theReturnObject;
        }
        /// <summary>
        /// Update Song in XML data source
        /// </summary>
        /// <param name="TheArtist">Artist Name</param>
        /// <param name="TheAlbum">Album Title</param>
        /// <param name="TheSongId">Song ID</param>
        /// <param name="TheTitle">Song Title</param>
        /// <param name="TheLength">Song Length</param>
        /// <returns>
        /// FunctionReturnObject
        /// </returns>
        public FunctionReturnObject UpdateSong(string TheArtist, string TheAlbum, string TheSongId,string TheTitle,string TheLength)
        {
            FunctionReturnObject theReturnObject = new FunctionReturnObject();
            TheLog.Addlog("Application=" + ApplicationName + " ||Function=UpdateSong ||TheSongId=" + TheSongId);
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(theAppSettings.theXMLSourceFile);
                string TheXPath;
                XmlNode xElt;

                //Validate the strings 
                TheUtility.Escape(ref TheArtist);
                TheUtility.Escape(ref TheAlbum);                
                TheUtility.Escape(ref TheSongId);
                TheUtility.Escape(ref TheTitle); 
                TheUtility.Escape(ref TheLength); 

                TheXPath = "//artist[@name=\"" + TheArtist + "\"]//album[@title=\"" + TheAlbum + "\"]//song[@SongId=\"" + TheSongId + "\"]";
                xElt = xDoc.SelectSingleNode(TheXPath);
                if (xElt == null)
                {
                    throw new Exception("Record not found. ||TheXPath=" + TheXPath);
                }
                xElt.Attributes.GetNamedItem("title").Value = TheTitle;
                xElt.Attributes.GetNamedItem("length").Value = TheLength;
                xDoc.Save(theAppSettings.theXMLSourceFile);
                theReturnObject.ReturnFlag = true;
                theReturnObject.ReturnMessage = "Updated successfully! ||TheAlbum=" + TheAlbum + "||TheSongId=" + TheSongId;
            }
            catch (Exception ex)
            {
                theReturnObject.ReturnFlag = false;
                theReturnObject.ReturnMessage = ex.Message;
                TheLog.Addlog("Application=" + ApplicationName + " ||Function=UpdateSong ||TheSongId=" + TheSongId + "||Error=" + ex.Message);
            }
            return theReturnObject;
        }       
    }
}
