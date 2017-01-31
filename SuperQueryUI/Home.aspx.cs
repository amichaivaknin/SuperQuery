using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using businessLogic;
using businessLogic.Interfaces;
using businessLogic.Models;

namespace SuperQueryUI
{
    public partial class Home : Page
    {
        private readonly List<Button> _buttonList = new List<Button>();
        private int _currPage;
        private readonly List<string> _engines = new List<string>();
        private readonly ISuperQueryManager _manager = new SuperQueryManager();
        private string _query;
        private List<FinalResult> _rankingResults;
        private List<int> _resPerPage = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            _query = search.Value;
            if (_query.Equals("")) return;
            if (
                !(checkbox_bing.Checked || checkbox_gigablast.Checked || checkbox_google.Checked ||
                  checkbox_HotBot.Checked || checkbox_rambler.Checked || checkbox_yandex.Checked))
            {
                //  alert.Visible = true;
                pagingDiv.Visible = false;
                return;
            }
            Session["query"] = _query;
            //if (checkbox_bing.Checked) _engines.Add("Bing");
            if (checkbox_google.Checked) _engines.Add("Google");
            if (checkbox_yandex.Checked) _engines.Add("Yandex");
            if (checkbox_gigablast.Checked) _engines.Add("GigaBlast");
            if (checkbox_HotBot.Checked) _engines.Add("HotBot");
            if (checkbox_rambler.Checked) _engines.Add("Rambler");
            /////////////// add more engines if needed !!!!!!!
            _rankingResults = _manager.GetQueryResults(_engines, _query).ToList();
            Session["res"] = _rankingResults;
            _currPage = 1;
            Session["page"] = _currPage;
            Session["flag"] = 1;
            ResPerPageFunc();
            CreatePage();
            InitialButtonList();
        }


        protected void ResPerPageFunc()
        {
            var numOfRes = _rankingResults.Count();
            int numOfPages;
            if (numOfRes % 10 == 0)
                numOfPages = numOfRes / 10;
            else
                numOfPages = numOfRes / 10 + 1;
            for (var i = 0; i < numOfPages - 1; i++)
                _resPerPage.Add(10);
            if (numOfRes % 10 != 0) _resPerPage.Add(numOfRes % 10);
            else _resPerPage.Add(10);

            Session["resPerPage"] = _resPerPage;
        }

        protected void CreatePage()
        {
            _resPerPage = (List<int>) Session["resPerPage"];
            _rankingResults = (List<FinalResult>) Session["res"];
            _currPage = (int) Session["page"];
            //////////////
            if (_rankingResults.Count == 0)
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
            var startIndex = 10 * (_currPage - 1);
            for (var i = 0; i < _resPerPage[_currPage - 1]; i++)
                MakeResDiv(_rankingResults[startIndex + i].Title, _rankingResults[startIndex + i].DisplayUrl,
                    _rankingResults[startIndex + i].Description,
                    _rankingResults[startIndex + i].SearchEngines.Keys.ToList());
        }

        protected void MakeResDiv(string title, string url, string description, List<string> enginesNames)
        {
            var addBrDiv =
                new HtmlGenericControl("BR");

            var createResultDiv =
                new HtmlGenericControl("DIV");


            var bytes = Encoding.Default.GetBytes(title);
            var titleUtf8 = Encoding.UTF8.GetString(bytes);
            var addTitleDiv =
                new HtmlGenericControl("DIV");
            addTitleDiv.Style.Add(HtmlTextWriterStyle.Color, "Blue");
            addTitleDiv.Style.Add(HtmlTextWriterStyle.FontSize, "Large");
            var hyperLink = new HyperLink();
            hyperLink.Text = title;
            hyperLink.NavigateUrl = "http://" + url;
            addTitleDiv.Controls.Add(hyperLink);
            // addTitleDiv.InnerHtml = title;


            var searchEnginesLDiv =
                new HtmlGenericControl("DIV");
            var names = "";
            foreach (var name in enginesNames)
                names = $"{names}{name}, ";
            names = names.TrimEnd(' ');
            names = names.TrimEnd(',');
            searchEnginesLDiv.Style.Add(HtmlTextWriterStyle.Color, "red");
            searchEnginesLDiv.InnerHtml = names;


            var addUrlDiv =
                new HtmlGenericControl("DIV");
            addUrlDiv.Style.Add(HtmlTextWriterStyle.Color, "green");
            addUrlDiv.InnerHtml = url;

            bytes = Encoding.Default.GetBytes(description);
            var descriptionUtf8 = Encoding.UTF8.GetString(bytes);
            var addDescriptionDiv =
                new HtmlGenericControl("DIV");
            addDescriptionDiv.Style.Add(HtmlTextWriterStyle.Color, "black");
            addDescriptionDiv.InnerHtml = description;


            createResultDiv.Controls.Add(addTitleDiv);
            createResultDiv.Controls.Add(searchEnginesLDiv);
            createResultDiv.Controls.Add(addUrlDiv);
            createResultDiv.Controls.Add(addDescriptionDiv);
            createResultDiv.Controls.Add(addBrDiv);
            resDiv.Controls.Add(createResultDiv);
            //this.Controls.Add(createResultDiv);
        }


        protected void InitialButtonList()
        {
            _currPage = (int) Session["page"];
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

            _buttonList.Add(page1Button);
            _buttonList.Add(page2Button);
            _buttonList.Add(page3Button);
            _buttonList.Add(page4Button);
            _buttonList.Add(page5Button);
            _buttonList.Add(page6Button);
            _buttonList.Add(page7Button);
            _buttonList.Add(page8Button);
            _buttonList.Add(page9Button);
            _buttonList.Add(page10Button);
            for (var k = 0; k < _resPerPage.Count; k++)
            {
                _buttonList[k].Style.Add(HtmlTextWriterStyle.Color, "black");
                _buttonList[k].Visible = true;
            }
            page1Button.Style.Add(HtmlTextWriterStyle.Color, "red");
        }

        protected void ChangePage(object sender, EventArgs e)
        {
            _currPage = (int) Session["page"];
            var b = (Button) sender;
            var newPage = int.Parse(b.Text);
            if (_currPage == newPage) return;
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

            switch (_currPage)
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
            CreatePage();
        }
    }
}