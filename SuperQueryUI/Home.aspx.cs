using businessLogic;
using businessLogic.Interfaces;
using businessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SuperQueryUI
{
    public partial class Home : System.Web.UI.Page
    {
        ISuperQueryManager manager = new SuperQueryManager();
        List<string> engines = new List<string>();
        List<int> resPerPage = new List<int>();
        List<string> enginesOnResults = new List<string>();
        string query;
        int currPage;
        List<FinalResult> ranking_results;
        List<Button> buttonList = new List<Button>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            query = search.Value;
            if (query.Equals("")) return;
            if (!((checkbox_bing.Checked) || (checkbox_gigablast.Checked) || (checkbox_google.Checked) || (checkbox_HotBot.Checked) || (checkbox_rambler.Checked) || (checkbox_yandex.Checked)))
            {
                pagingDiv.Visible = false;
                return;
            }
            Session["query"] = query;
            //if (checkbox_bing.Checked) engines.Add("Bing");
            //if (checkbox_google.Checked) engines.Add("Google");
            //if (checkbox_yandex.Checked) engines.Add("Yandex");
            if (checkbox_gigablast.Checked) engines.Add("GigaBlast");
            if (checkbox_HotBot.Checked) engines.Add("HotBot");
            if (checkbox_rambler.Checked) engines.Add("Rambler");
            /////////////// add more engines if needed !!!!!!!
            ranking_results = manager.GetQueryResults(engines, query).ToList();
            Session["res"] = ranking_results;
            currPage = 1;
            Session["page"] = currPage;
            Session["flag"] = 1;
            resPerPageFunc();
            createPage();
            initialButtonList();

        }


        protected void resPerPageFunc()
        {
            int numOfRes = ranking_results.Count();
            int numOfPages;
            if (numOfRes % 10 == 0)
            {
                numOfPages = numOfRes / 10;

            }
            else
            {
                numOfPages = (numOfRes / 10) + 1;
            }
            for (int i = 0; i < numOfPages - 1; i++)
            {
                resPerPage.Add(10);
            }
            if (!(numOfRes % 10 == 0)) resPerPage.Add(numOfRes % 10);
            else resPerPage.Add(10);

            Session["resPerPage"] = resPerPage;
        }

        protected void createPage()
        {
            resPerPage = (List<int>)Session["resPerPage"];
            ranking_results = (List<FinalResult>)Session["res"];
            currPage = (int)Session["page"];
            //////////////
            if(ranking_results.Count==0)
            {
                resDiv.Visible = false;
                pagingDiv.Visible = false;
                noResDIv.Visible = true;
                return;
            }
            resDiv.Visible = true;
            pagingDiv.Visible = true;
            noResDIv.Visible = false;
            //////////////
            int startIndex = 10 * (currPage - 1);
            for (int i = 0; i < resPerPage[currPage - 1]; i++)
            {
                makeResDiv(ranking_results[startIndex + i].Title, ranking_results[startIndex + i].DisplayUrl, ranking_results[startIndex + i].Description, ranking_results[startIndex + i].SearchEngines.Keys.ToList());
            }
        }
        protected void makeResDiv(string title, string url, string description, List<string> enginesNames)
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
            hyperLink.NavigateUrl = "http://" + url;
            addTitleDiv.Controls.Add(hyperLink);
            // addTitleDiv.InnerHtml = title;


            System.Web.UI.HtmlControls.HtmlGenericControl searchEnginesLDiv =
            new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            string names = "";
            foreach (var name in enginesNames)
            {
                names = $"{names}{name}, ";
            }
            names = names.TrimEnd(' ');
            names = names.TrimEnd(',');
            searchEnginesLDiv.Style.Add(HtmlTextWriterStyle.Color, "red");
            searchEnginesLDiv.InnerHtml = names;


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
            createResultDiv.Controls.Add(searchEnginesLDiv);
            createResultDiv.Controls.Add(addURLDiv);
            createResultDiv.Controls.Add(addDescriptionDiv);
            createResultDiv.Controls.Add(addBRDiv);
            resDiv.Controls.Add(createResultDiv);
            //this.Controls.Add(createResultDiv);
        }


        protected void initialButtonList()
        {
            currPage = (int)Session["page"];
            page1Button.Style.Add(HtmlTextWriterStyle.Color, "red");
            page2Button.Visible = false;
            page3Button.Visible = false;
            page4Button.Visible = false;
            page5Button.Visible = false;
            page6Button.Visible = false;
            page7Button.Visible = false;
            page8Button.Visible = false;
            page9Button.Visible = false;
            page10Button.Visible = false;

            buttonList.Add(page1Button);
            buttonList.Add(page2Button);
            buttonList.Add(page3Button);
            buttonList.Add(page4Button);
            buttonList.Add(page5Button);
            buttonList.Add(page6Button);
            buttonList.Add(page7Button);
            buttonList.Add(page8Button);
            buttonList.Add(page9Button);
            buttonList.Add(page10Button);
            for (int k = 0; k < resPerPage.Count; k++)
            {
                buttonList[k].Style.Add(HtmlTextWriterStyle.Color, "black");
                buttonList[k].Visible = true;
            }
            page1Button.Style.Add(HtmlTextWriterStyle.Color, "red");
        }

        protected void changePage(object sender, EventArgs e)
        {
            currPage = (int)Session["page"];
            Button b = (Button)sender;
            int newPage = int.Parse(b.Text);
            if (currPage == newPage) return;
            switch (newPage)
            {
                case 1:
                    page1Button.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                case 2:
                    page2Button.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                case 3:
                    page3Button.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                case 4:
                    page4Button.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                case 5:
                    page5Button.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                case 6:
                    page6Button.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                case 7:
                    page7Button.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                case 8:
                    page8Button.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                case 9:
                    page9Button.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
                case 10:
                    page10Button.Style.Add(HtmlTextWriterStyle.Color, "red");
                    break;
            }

            switch (currPage)
            {
                case 1:
                    page1Button.Style.Add(HtmlTextWriterStyle.Color, "black");
                    break;
                case 2:
                    page2Button.Style.Add(HtmlTextWriterStyle.Color, "black");
                    break;
                case 3:
                    page3Button.Style.Add(HtmlTextWriterStyle.Color, "black");
                    break;
                case 4:
                    page4Button.Style.Add(HtmlTextWriterStyle.Color, "black");
                    break;
                case 5:
                    page5Button.Style.Add(HtmlTextWriterStyle.Color, "black");
                    break;
                case 6:
                    page6Button.Style.Add(HtmlTextWriterStyle.Color, "black");
                    break;
                case 7:
                    page7Button.Style.Add(HtmlTextWriterStyle.Color, "black");
                    break;
                case 8:
                    page8Button.Style.Add(HtmlTextWriterStyle.Color, "black");
                    break;
                case 9:
                    page9Button.Style.Add(HtmlTextWriterStyle.Color, "black");
                    break;
                case 10:
                    page10Button.Style.Add(HtmlTextWriterStyle.Color, "black");
                    break;
            }
            Session["page"] = newPage;
            createPage();

        }     
    }
}