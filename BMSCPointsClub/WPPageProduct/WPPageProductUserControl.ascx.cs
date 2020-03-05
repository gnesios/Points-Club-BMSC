using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace BMSCPointsClub.WPPageProduct
{
    public partial class WPPageProductUserControl : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                string productId = string.IsNullOrEmpty(Page.Request.QueryString["pid"]) ? "0" : Page.Request.QueryString["pid"];
                string library = string.IsNullOrEmpty(Page.Request.QueryString["lib"]) ? "" : Page.Request.QueryString["lib"];

                this.SetParameters();
                this.SetProductInfo(productId, library);
                this.PopulateRelated(productId, library);
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

        private void PopulateRelated(string prodId, string library)
        {
            int number;
            int productId = Int32.TryParse(prodId, out number) ? number : 0;

            List<CatalogItem> tempCatalog;
            if (library == "catalog")
                tempCatalog = SharePointConnector.GetCatalog(productId, "ID", true);
            else if (library == "suggs")
                tempCatalog = SharePointConnector.GetSuggestions(productId);
            else
                tempCatalog = new List<CatalogItem>();

            int categoryId = tempCatalog.Count > 0 ? tempCatalog[0].CategoryId : 0;
            List<CatalogItem> catalogItems = SharePointConnector.GetCatalog(0, "ID", false);

            int count = 0;
            foreach (CatalogItem item in catalogItems)
            {
                if (item.CategoryId == categoryId && item.Id != productId)
                {
                    litRelatedProducts.Text += string.Format(
                        @"<li><a href=""{0}""><img src=""{1}""/></a><p><span>{2}</span><br/>{3}</p></li>",
                        item.Url(), item.Image, FormatedPoints(item.Points) + " puntos", item.Title);
                    count++;

                    if (count == 8)//eight is the limit
                        break;
                }
            }

            if (string.IsNullOrEmpty(litRelatedProducts.Text))
                litRelatedProducts.Text = "No existen otros productos para esta categoría en este momento.";
        }

        private void SetProductInfo(string prodId, string library)
        {
            int number;
            int productId = Int32.TryParse(prodId, out number) ? number : 0;

            List<CatalogItem> catalogItems;
            if (library == "catalog")
                catalogItems = SharePointConnector.GetCatalog(productId, "ID", true);
            else if (library == "suggs")
                catalogItems = SharePointConnector.GetSuggestions(productId);
            else
                catalogItems = new List<CatalogItem>();

            foreach (CatalogItem item in catalogItems)
            {
                litProductCategory.Text = string.Format(
                    @"<span><a href=""/Paginas/Club%20de%20Puntos/CategoriaClubPuntos.aspx?cat={0}"" title=""{1}""></a></span>" +
                    @"<h1 class=""left"">{2}</h1>",
                    item.CategoryId, item.Category, item.Category);

                litProductName.Text = string.Format(
                    @"<span></span><h1 class=""right"">{0}</h1>",
                    item.Title);

                litProductDetails.Text = string.Format(
                    @"<div><img src=""{0}"" /></div>" +
                    @"<div><span>" +
                    @"<h2>{1}</h2><h3>{2}</h3>" +
                    @"{3}" +
                    @"<ul class=""social"">" +
                    @"<li><a href=""http://www.facebook.com/sharer.php?u={4}"" class=""socialf"" title=""Compártenos en facebook"" target=""_blank""></a></li>" +
                    @"<li><a href=""https://twitter.com/share?url={4}"" class=""socialt"" title=""Compártenos en twitter"" target=""_blank""></a></li>" +
                    @"<li><a href=""https://plus.google.com/share?url={4}"" class=""socialg"" title=""Compártenos en google+"" target=""_blank""></a></li>" +
                    @"</ul>" +
                    @"</span></div>",
                    item.Image, FormatedPoints(item.Points) + " puntos", item.Category, item.Description,
                    System.Net.WebUtility.UrlEncode(Page.Request.Url.AbsoluteUri));
            }
        }

        private void SetParameters()
        {
            List<ParameterItem> paramsDescription = SharePointConnector.GetParameters("DESCRIPCIÓN");

            foreach (ParameterItem item in paramsDescription)
            {
                if (item.Title.Trim().Equals("Cómo canjeo mi premio? - A"))
                {
                    litFaqA.Text = item.Description;
                }
                else if (item.Title.Trim().Equals("Cómo canjeo mi premio? - B"))
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
    }
}
