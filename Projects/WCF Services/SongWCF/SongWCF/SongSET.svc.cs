using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.ServiceModel.Activation;
using SongDAO;
using LocalAppLog;

namespace SongWCF
{
    
    /// <summary>
    /// Song SET WCF Services
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SongSET : ISongSET
    {

        private LogApp TheLog = new LogApp();
        private string ApplicationName = "SongWCF";
        /// <summary>
        /// Add a new song into data source
        /// </summary>
        /// <param name="TheArtist">Artist Name</param>
        /// <param name="TheAlbum">Album Title</param>
        /// <param name="TheTitle">Song Title</param>
        /// <param name="TheLength">Song Length</param>
        /// <returns>
        /// FunctionReturnObject
        /// </returns>
        public FunctionReturnObject AddSongInfo(string TheArtist, string TheAlbum, string TheTitle, string TheLength)
        {
            FunctionReturnObject theReturnObject = new FunctionReturnObject();

            SongApp TheSongApp = new SongApp();
            TheLog.Addlog("Application=" + ApplicationName + " ||Function=AddSongInfo ||TheArtist=" + TheArtist);
            TheLength=TheLength.Replace(".", ":");
            try
            {
                ConcurrentControl_Entry();
                theReturnObject = TheSongApp.AddSong(TheArtist, TheAlbum, TheTitle, TheLength);
                ConcurrentControl_Exit();
            }
            catch (Exception ex)
            {
                theReturnObject.ReturnFlag = false;
                theReturnObject.ReturnMessage = ex.Message;
                TheLog.Addlog("Application=" + ApplicationName + "||Error=" + ex.Message);
            }

            return theReturnObject;
        }
        /// <summary>
        /// Update a song's information (By Song's ID)
        /// </summary>
        /// <param name="TheArtist">Artist Name</param>
        /// <param name="TheAlbum">Album Title</param>
        /// <param name="TheSongId">Song ID</param>
        /// <param name="TheTitle">Song Title</param>
        /// <param name="TheLength">Song Length</param>
        /// <returns>
        /// FunctionReturnObject
        /// </returns>
        public FunctionReturnObject UpdateSongInfo(string TheArtist, string TheAlbum, string TheSongId, string TheTitle, string TheLength)
        {
            FunctionReturnObject theReturnObject = new FunctionReturnObject();

            SongApp TheSongApp = new SongApp();
            TheLog.Addlog("Application=" + ApplicationName + " ||Function=AddSongInfo ||TheArtist=" + TheArtist + "||TheSongId=" + TheSongId);
            TheLength=TheLength.Replace(".", ":");
            try
            {
                ConcurrentControl_Entry();
                theReturnObject = TheSongApp.UpdateSong(TheArtist, TheAlbum, TheSongId, TheTitle, TheLength);
                ConcurrentControl_Exit();
            }
            catch (Exception ex)
            {
                theReturnObject.ReturnFlag = false;
                theReturnObject.ReturnMessage = ex.Message;
                TheLog.Addlog("Application=" + ApplicationName + "||Error=" + ex.Message);
            }
            
            return theReturnObject;
        }
        /// <summary>
        /// Concurrent control for XML source access - Entry Point Control
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// (Mutual exclusion - Dekker's algorithm)
        /// </remarks>
        private FunctionReturnObject ConcurrentControl_Entry()
        {
            FunctionReturnObject theReturnObject = new FunctionReturnObject();
            TheLog.Addlog("Application=" + ApplicationName + " ||Function=ConcurrentControl_Entry ||ConcurrentFlag01=" + HttpContext.Current.Application["ConcurrentFlag01"] + "||ConcurrentFlag02=" + HttpContext.Current.Application["ConcurrentFlag02"]);

            if ((bool)HttpContext.Current.Application["ConcurrentTurnFlag"])
            {
                HttpContext.Current.Application["ConcurrentFlag02"] = true;
                while ((bool)HttpContext.Current.Application["ConcurrentFlag01"] == true)
                {
                    if (!(bool)HttpContext.Current.Application["ConcurrentTurnFlag"])
                    {
                        HttpContext.Current.Application["ConcurrentFlag02"] = false;
                        while (!(bool)HttpContext.Current.Application["ConcurrentTurnFlag"])
                        {
                            System.Threading.Thread.Sleep(500); //Wait for 0.5 second
                        }
                    }
                }
            }
            else
            {
                HttpContext.Current.Application["ConcurrentFlag01"] = true;
                while ((bool)HttpContext.Current.Application["ConcurrentFlag02"] == true)
                {
                    if ((bool)HttpContext.Current.Application["ConcurrentTurnFlag"])
                    {
                        HttpContext.Current.Application["ConcurrentFlag01"] = false;
                        while ((bool)HttpContext.Current.Application["ConcurrentTurnFlag"])
                        {
                            System.Threading.Thread.Sleep(500);
                        }
                    }
                }
            }

            theReturnObject.ReturnMessage = "ConcurrentFlag01=" + HttpContext.Current.Application["ConcurrentFlag01"] + "||ConcurrentFlag02=" + HttpContext.Current.Application["ConcurrentFlag02"];
            theReturnObject.ReturnFlag = true;
            TheLog.Addlog("Application=" + ApplicationName + " ||Function=ConcurrentControl_Entry ||" + theReturnObject.ReturnMessage);
            return theReturnObject;
        }
        /// <summary>
        /// Concurrent control for XML source access - Exit Point Control
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// (Mutual exclusion - Dekker's algorithm)
        /// </remarks>
        private FunctionReturnObject ConcurrentControl_Exit()
        {
            FunctionReturnObject theReturnObject = new FunctionReturnObject();

            if ((bool)HttpContext.Current.Application["ConcurrentTurnFlag"])
            {
                HttpContext.Current.Application["ConcurrentTurnFlag"] = false;
                HttpContext.Current.Application["ConcurrentFlag02"] = false;
            }
            else
            {
                HttpContext.Current.Application["ConcurrentTurnFlag"] = true;
                HttpContext.Current.Application["ConcurrentFlag01"] = false;
            }

            theReturnObject.ReturnMessage = "ConcurrentFlag01=" + HttpContext.Current.Application["ConcurrentFlag01"] + "||ConcurrentFlag02=" + HttpContext.Current.Application["ConcurrentFlag02"];
            theReturnObject.ReturnFlag = true;
            TheLog.Addlog("Application=" + ApplicationName + " ||Function=ConcurrentControl_Exit ||" + theReturnObject.ReturnMessage);
            return theReturnObject;
        }
    }
}
