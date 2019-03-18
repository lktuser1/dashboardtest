using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace EDS.Intranet.Error
{
    public class ErrorInfo
    {
        private string _baseException = string.Empty;
        private string _browserType = string.Empty;
        private string _computerName = string.Empty;
        private string _data = string.Empty;
        private string _exception = string.Empty;
        private string _exceptionType = string.Empty;
        private string _hResult = string.Empty;
        private string _innerException = string.Empty;
        private string _message = string.Empty;
        private string _netVersion = string.Empty;
        private string _osVersion = string.Empty;
        private string _source = string.Empty;
        private string _stackTrace = string.Empty;
        private string _target = string.Empty;
        private string _url = string.Empty;
        private string _userID = string.Empty;
        private string _userName = string.Empty;

        public string BaseException
        {
            get { return _baseException; }
            set { _baseException = value; }
        }

        public string BrowserType
        {
            get { return _browserType; }
            set { _browserType = value; }
        }

        public string ComputerName
        {
            get { return _computerName; }
            set { _computerName = value; }
        }

        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public string Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }

        public string ExceptionType
        {
            get { return _exceptionType; }
            set { _exceptionType = value; }
        }

        public string HResult
        {
            get { return _hResult; }
            set { _hResult = value; }
        }

        public string InnerException
        {
            get { return _innerException; }
            set { _innerException = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string NetVersion
        {
            get { return _netVersion; }
            set { _netVersion = value; }
        }

        public string OSVersion
        {
            get { return _osVersion; }
            set { _osVersion = value; }
        }

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public string StackTrace
        {
            get { return _stackTrace; }
            set { _stackTrace = value; }
        }

        public string Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
    }
}
