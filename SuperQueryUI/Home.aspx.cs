using businessLogic;
using businessLogic.Interfaces;
using businessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SuperQueryUI
{
    public partial class Home : System.Web.UI.Page
    {
        ISuperQueryManager manager = new SuperQueryManager();
        List<string> engines = new List<string>();
        List<int> resPerPage = new List<int>();
        string query;
        List<FinalResult> ranking_results;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
              //  createResultDiv();
            }
            else
            {
            //    Button clickedButton = (Button)sender;
            //    paging(int.Parse(clickedButton.Text));
               // clickedButton.Text

            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            query = search.Value;
            if (checkbox_google.Checked) engines.Add("Google");
            if (checkbox_bing.Checked) engines.Add("Bing");
            if (checkbox_yandex.Checked) engines.Add("Yandex");
            if (checkbox_gigablast.Checked) engines.Add("GigaBlast");
            /////////////// add more engines if needed !!!!!!!
            ranking_results=manager.GetQueryResults(engines, query).ToList();
            createFirstpage();

        }

        protected void createResultDiv()
        {            
            for (int i = 0; i < 10; i++)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl createResultDiv =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                createResultDiv.ID = "resultDivNum" + i;

                System.Web.UI.HtmlControls.HtmlGenericControl addTitleDiv =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                addTitleDiv.ID = "titleDiv";
                addTitleDiv.Style.Add(HtmlTextWriterStyle.Color, "Blue");
                addTitleDiv.InnerHtml = "Title: ";


                System.Web.UI.HtmlControls.HtmlGenericControl addURLDiv =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                addURLDiv.ID = "urlDiv";
                addURLDiv.Style.Add(HtmlTextWriterStyle.Color, "green");
                addURLDiv.InnerHtml = "URL: ";

                System.Web.UI.HtmlControls.HtmlGenericControl addDescriptionDiv =
                new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                addDescriptionDiv.ID = "DescriptionDiv";
                addDescriptionDiv.Style.Add(HtmlTextWriterStyle.Color, "black");
                addDescriptionDiv.InnerHtml = "Description: ";

                System.Web.UI.HtmlControls.HtmlGenericControl addBRDiv =
                new System.Web.UI.HtmlControls.HtmlGenericControl("BR");

                createResultDiv.Controls.Add(addTitleDiv);
                createResultDiv.Controls.Add(addURLDiv);
                createResultDiv.Controls.Add(addDescriptionDiv);
                createResultDiv.Controls.Add(addBRDiv);
                this.Controls.Add(createResultDiv);

                //////////////
                               
            }
            List<Button> l = new List<Button>();
            System.Web.UI.HtmlControls.HtmlGenericControl addButtonDiv =
            new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            int res;
            for (int k = 0; k < 50; k++)
            {
                Button b = new Button();
                b.Text = k.ToString();
                //b.CausesValidation = false;
                
                //b.Attributes.Add("AutoPostBack", "return false;");
                //b.Click += paging;
                addButtonDiv.Controls.Add(b);
            }
            //this.Controls.Add(addButtonDiv);
            form1.Controls.Add(addButtonDiv);
        }

        protected void paging(int numOfButton)
        {

        }

        protected void resPerPageFunc()
        {
            int numOfRes = ranking_results.Count();
            int numOfPages;
            if(numOfRes%10 ==0)
            {
                numOfPages = numOfRes / 10;

            }
            else
            {
                numOfPages = (numOfRes / 10) + 1;
            }
            for(int i = 0; i < numOfPages - 1; i++)
            {
                resPerPage.Add(10);
            }
            resPerPage.Add(numOfRes % 10);
        }

        protected void createFirstpage()
        {
            resPerPageFunc();
            for(int i = 0; i < resPerPage[0]; i++)
            {
                makeResDiv(ranking_results[i].Title, ranking_results[i].DisplayUrl, ranking_results[i].Description);
            }
        }
        protected void makeResDiv(string title,string url,string description)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl addBRDiv =
            new System.Web.UI.HtmlControls.HtmlGenericControl("BR");

            System.Web.UI.HtmlControls.HtmlGenericControl createResultDiv =
            new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");


            byte[] bytes = Encoding.Default.GetBytes(title);
            string title_utf8 = Encoding.UTF8.GetString(bytes);
            System.Web.UI.HtmlControls.HtmlGenericControl addTitleDiv =
            new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            addTitleDiv.Style.Add(HtmlTextWriterStyle.Color, "Blue");
            addTitleDiv.Style.Add(HtmlTextWriterStyle.FontSize, "Large");
            HyperLink hyperLink = new HyperLink();
            hyperLink.Text = title_utf8;
            hyperLink.NavigateUrl = "http://"+url;
            addTitleDiv.Controls.Add(hyperLink);
            // addTitleDiv.InnerHtml = title;
            


            System.Web.UI.HtmlControls.HtmlGenericControl addURLDiv =
            new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            addURLDiv.Style.Add(HtmlTextWriterStyle.Color, "green");
            addURLDiv.InnerHtml = url;

            bytes = Encoding.Default.GetBytes(description);
            string description_utf8 = Encoding.UTF8.GetString(bytes);
            System.Web.UI.HtmlControls.HtmlGenericControl addDescriptionDiv =
            new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            addDescriptionDiv.Style.Add(HtmlTextWriterStyle.Color, "black");
            addDescriptionDiv.InnerHtml = description_utf8;

            
            createResultDiv.Controls.Add(addTitleDiv);
            createResultDiv.Controls.Add(addURLDiv);
            createResultDiv.Controls.Add(addDescriptionDiv);
            createResultDiv.Controls.Add(addBRDiv);
            this.Controls.Add(createResultDiv);
        }

    }
}