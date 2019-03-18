using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EDS.Intranet.ServerControls
{
    /// <summary>
    /// This control inheirits the ASP.NET ValidationSummary control but adds
    /// the ability to dynamically add error messages without requiring 
    /// validation controls.
    /// </summary>
    public class edsValidationSummary : System.Web.UI.WebControls.ValidationSummary
    {
        /// <summary>
        /// This method allows the caller to place custom text messages inside the 
        /// validation summary control.
        /// </summary>
        /// <param name="msg">The message you want to appear in the summary</param>
        public void AddErrorMessage(string msg)
        {
            this.Page.Validators.Add(new DummyValidator(msg));
        }
    }

    /// <summary>
    /// The validation summary control works by iterating over the Page.Validators
    /// collection and displaying the ErrorMessage property of each validator
    /// that return false for the IsValid() property.  This class will act 
    /// like all the other validators except it always is invalid and thus the ErrorMessage
    /// property will always be displayed.
    /// </summary>
    internal class DummyValidator : IValidator
    {

        private string errorMsg;

        public DummyValidator(string msg)
        {
            errorMsg = msg;
        }

        public string ErrorMessage
        {
            get { return errorMsg; }
            set { errorMsg = value; }
        }

        public bool IsValid
        {
            get { return false; }
            set { }
        }

        public void Validate()
        {
        }
    }
}
