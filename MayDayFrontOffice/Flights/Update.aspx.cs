﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ////store the data in the session object
        //Session["FlightNo"] = FlightNo;
        ////redirect to the edit page
        //Response.Redirect("AddFlight.aspx");
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("DefaultFlight.aspx");
    }
}