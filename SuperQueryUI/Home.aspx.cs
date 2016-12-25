using businessLogic;
using businessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SuperQueryUI
{
    public partial class Home : System.Web.UI.Page
    {
        ISuperQueryManager manager = new SuperQueryManager();
        List<string> engines = new List<string>();
        string query;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            query = search.Value;
            if (checkbox_google.Checked) engines.Add("Google");
            if (checkbox_bing.Checked) engines.Add("Bing");
            if (checkbox_yandex.Checked) engines.Add("Yandex");
            /////////////// add more engines if needed !!!!!!!
            manager.GetQueryResults(engines, query);




        }
    }
}