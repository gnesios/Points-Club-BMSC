using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace BMSCPointsClub.WPPageWelcome
{
    public partial class WPPageWelcomeUserControl : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                this.SetParameters();
                this.PopulateCategories();
                this.PopulateSuggestions();
            }
            catch (Exception ex)
            {
                Literal errorMessage = new Literal();
                errorMessage.Text = ex.Message;

                this.Controls.Clear();
                this.Controls.Add(errorMessage);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txbId.Text) && ddlCity.SelectedIndex != 0)
                {
                    string ci = this.StripTagsCharArray(txbId.Text.Trim() + ddlCity.SelectedItem.Value);
                    string url = string.Format("/_layouts/BmscWebSite2_ImportedSolutions/ConsultaPuntosResultado.aspx?ci={0}", ci);

                    this.ShowBasicDialog(url, "puntos acumulados");

                    txbId.Text = "";
                    ddlCity.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Literal errorMessage = new Literal();
                errorMessage.Text = ex.Message;

                this.Controls.Clear();
                this.Controls.Add(errorMessage);
            }
        }

        protected void PopulateCategories()
        {
            List<CategoryItem> allCategories = SharePointConnector.GetCategories();

            foreach (CategoryItem item in allCategories)
            {
                litCategories.Text += string.Format(
                    @"<a href=""{0}""><p>{1}</p><img src=""{2}"" alt="""" /></a>",
                    item.Url(), item.Title, item.Image);
            }
        }

        protected void PopulateSuggestions()
        {
            string qsRegion = string.IsNullOrEmpty(Page.Request.QueryString["reg"]) ? "" : Page.Request.QueryString["reg"];
            List<CatalogItem> suggestions = SharePointConnector.GetSuggestions(0);

            foreach (CatalogItem item in suggestions)
            {
                if (item.Region.Contains(qsRegion))
                {
                    litSuggestions.Text += string.Format(
                        @"<li><a href=""{0}""><img src=""{1}""/></a><p><span>{2}</span><br/>{3}</p></li>",
                        item.Url(), item.Image, FormatedPoints(item.Points) + " puntos", item.Title);
                }
            }

            if (string.IsNullOrEmpty(litSuggestions.Text))
                litSuggestions.Text = "No existen productos sugeridos en este momento.";
        }

        protected void SetParameters()
        {
            List<ParameterItem> paramsLink = SharePointConnector.GetParameters("ENLACE");
            List<ParameterItem> paramsDescription = SharePointConnector.GetParameters("DESCRIPCIÓN");

            foreach (ParameterItem item in paramsLink)
            {
                litCatalogLinks.Text += string.Format(
                    @"<a href=""{0}"" class=""doc"" title=""""><p>{1}</p><img src=""{2}"" alt="""" /></a>",
                    item.Link, item.Title, item.Image);
            }

            foreach (ParameterItem item in paramsDescription)
            {
                if (item.Title.Trim().Equals("Qué es el Club de Puntos? - A"))
                {
                    litFaqA.Text = item.Description;
                }
                else if (item.Title.Trim().Equals("Qué es el Club de Puntos? - B"))
                {
                    litFaqB.Text = item.Description;
                }
            }
        }

        /// <summary>
        /// Format integer number
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private string FormatedPoints(int points)
        {
            string one = string.Format("{0:N0}", points);
            string two = one.Replace(',', '.');

            return two;
        }

        /// <summary>
        /// Show SharePoint pop-up dialog
        /// </summary>
        /// <param name="url"></param>
        /// <param name="title"></param>
        private void ShowBasicDialog(string url, string title)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine(@"<script type=""text/ecmascript"" language=""ecmascript"">");
            sb.AppendLine(@"SP.SOD.executeFunc('sp.js', 'SP.ClientContext', openBasicServerDialog);");
            sb.AppendLine(@"function openBasicServerDialog() {");
            sb.AppendLine(@"  var options = {");
            sb.AppendLine(string.Format(@"url: '{0}',", url));
            sb.AppendLine(string.Format(@"title: '{0}',", title));
            sb.AppendLine(@"width: 600, height: 500, allowMaximize: false, showClose: true");
            sb.AppendLine(@"};");
            sb.AppendLine(@"SP.UI.ModalDialog.showModalDialog(options);");
            sb.AppendLine(@"}");
            sb.AppendLine(@"</script>");
            ltrScriptLoader.Text = sb.ToString();
        }

        /// <summary>
        /// Remove html characters from string
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns></returns>
        private string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }

            string theResult = new string(array, 0, arrayIndex);

            if (theResult.Contains("&") || theResult.Contains("'"))
                theResult = System.Text.RegularExpressions.Regex.Replace(theResult, "[&#;/']", "");

            return theResult;
        }
    }
}
