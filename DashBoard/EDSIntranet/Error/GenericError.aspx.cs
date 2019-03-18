using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using EDS.Intranet.Base;
using EDS.Intranet.Common;

namespace EDS.Intranet.Error
{
    public partial class GenericError : ErrorPageBase
    {
        EDS.Intranet.Error.ErrorInfo lastError;

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Master.PageTitle = "An Error Has Occurred";

            this.lastError = Utilities.LastError;

            if (this.lastError == null)
            {
                this.lastError = new EDS.Intranet.Error.ErrorInfo();
                this.lastError.Message = "No error found.";
            }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            Label myLabel;

            try
            {
                //Populate error information details
                myLabel = (Label)this.DetailsView1.FindControl("Application");
                myLabel.Text = this.lastError.Source;

                myLabel = (Label)this.DetailsView1.FindControl("ErrorMessage");
                myLabel.Text = "<b>" + this.lastError.Message + "</b>";

                myLabel = (Label)this.DetailsView1.FindControl("URL");
                myLabel.Text = this.lastError.Url;

                myLabel = (Label)this.DetailsView1.FindControl("ExceptionType");
                myLabel.Text = this.lastError.ExceptionType;

                myLabel = (Label)this.DetailsView1.FindControl("StackTrace");
                myLabel.Text = this.lastError.StackTrace.Replace(" at ", "<br/>at <br/>");
                if (myLabel.Text.Length > 0)
                {
                    myLabel.Text = myLabel.Text.Substring(7);
                }

                myLabel = (Label)this.DetailsView1.FindControl("SourceCode");
                myLabel.Text = this.GetSourceCode();

                myLabel = (Label)this.DetailsView1.FindControl("InnerException");
                myLabel.Text = this.lastError.InnerException;

                myLabel = (Label)this.DetailsView1.FindControl("Exception");
                myLabel.Text = this.lastError.Exception;

                myLabel = (Label)this.DetailsView1.FindControl("BaseException");
                myLabel.Text = this.lastError.BaseException;

                myLabel = (Label)this.DetailsView1.FindControl("Data");
                myLabel.Text = this.lastError.Data;

                //Populate additional information details
                myLabel = (Label)this.DetailsView2.FindControl("UserID");
                myLabel.Text = this.lastError.UserID;

                myLabel = (Label)this.DetailsView2.FindControl("UserName");
                myLabel.Text = this.lastError.UserName;

                myLabel = (Label)this.DetailsView2.FindControl("BrowserType");
                myLabel.Text = this.lastError.BrowserType;

                myLabel = (Label)this.DetailsView2.FindControl("ComputerName");
                myLabel.Text = this.lastError.ComputerName;

                myLabel = (Label)this.DetailsView2.FindControl("OSVersion");
                myLabel.Text = this.lastError.OSVersion;

                myLabel = (Label)this.DetailsView2.FindControl("NetVersion");
                myLabel.Text = this.lastError.NetVersion;

                base.Render(writer);
            }
            catch
            {
               //Do not get in an endless loop of throwing exceptions if something happens here.
            }
        }

        /// <summary>
        /// This procedure returns the relevant source code, if found.
        /// </summary>
        private string GetSourceCode()
        {
            string path = string.Empty;
            Int32 line;
            Int32 currentLine = 0;
            StringBuilder sourceCode;
            StreamReader sourceStream = null;
            string sourceStreamCleaned = string.Empty;
            Match expressionMatch;
            Regex regExpression;

            try
            {
                //find the path and line number of the source of the error 
                //(if it appears in the stack trace). 
                sourceCode = new StringBuilder(300);
                regExpression = new Regex("in (?<path>.+):line (?<line>\\d+)");
                expressionMatch = regExpression.Match(this.lastError.StackTrace);

                if (expressionMatch.Success)
                {
                    path = expressionMatch.Groups["path"].Value;
                    line = Convert.ToInt32(expressionMatch.Groups["line"].Value);

                    //find the source code around the erroring line. 
                    if (File.Exists(path))
                    {
                        try
                        {
                            sourceStream = File.OpenText(path);

                            while (!(sourceStream.Peek() == -1 || currentLine > line - 5))
                            {
                                currentLine += 1;
                                sourceStream.ReadLine();
                            }

                            while (!(sourceStream.Peek() == -1 || currentLine > line + 2))
                            {
                                currentLine += 1;

                                //Clean the string before displaying -- replace opening and 
                                //closing html tag delimeters with appropriate replacements that 
                                //will not break the table layout on the screen. 
                                sourceStreamCleaned = sourceStream.ReadLine();
                                sourceStreamCleaned = sourceStreamCleaned.Replace("<", "&lt;");
                                sourceStreamCleaned = sourceStreamCleaned.Replace(">", "&gt;");

                                sourceCode.Append("Line ");
                                sourceCode.Append(currentLine.ToString());
                                sourceCode.Append(":&nbsp;&nbsp;");
                                sourceCode.Append(sourceStreamCleaned);
                                sourceCode.Append("<br/>");
                            }
                        }
                        finally
                        {
                            sourceStream.Close();
                        }
                    }
                }

                return sourceCode.ToString();
            }
            catch
            {
                return "Error retrieving source code.";
            }
        }
    }
}
