using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class CardInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnSubmitClicked(object sender, EventArgs e)
        {
            Session["Amount"] = Amount.Text;
            Response.Redirect("/");
        }
    }
}