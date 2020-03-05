using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace BMSCPointsClub.WPPageCategory
{
    public partial class WPPageCategoryUserControl : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                hddRangeValueMin.Value = "5000";
                hddRangeValueMax.Value = "50000";
                string categoryId = string.IsNullOrEmpty(Page.Request.QueryString["cat"]) ? "0" : Page.Request.QueryString["cat"];

                this.SetParameters(categoryId);
                this.PopulateSuggestions(categoryId);
                this.PopulateAllItems(categoryId);
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
            litSliderScript.Text = string.Format(
                "<script>" +
                "var theslider = document.getElementById('theslider');" +
                "noUiSlider.create(theslider, {{" +
                "start: [{0}, {1}], connect: true, step: 150, orientation: 'horizontal', tooltips: true," +
                "range: {{ 'min': 50, 'max': 100000 }}," +
                "format: wNumb({{ decimals: 0, thousand: '.' }})" +
                "}});" +
                "theslider.noUiSlider.on('update', function (values, handle) {{" +
                "var hddRangeValueMin = document.getElementById('hddRangeValueMin');" +
                "var hddRangeValueMax = document.getElementById('hddRangeValueMax');" +
                "hddRangeValueMin.value = theslider.noUiSlider.get()[0];" +
                "hddRangeValueMax.value = theslider.noUiSlider.get()[1];" +
                "}});" +
                "</script>",
                hddRangeValueMin.Value.Replace(".", ""), hddRangeValueMax.Value.Replace(".", ""));
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string categoryId = string.IsNullOrEmpty(Page.Request.QueryString["cat"]) ? "0" : Page.Request.QueryString["cat"];
                int rangeValueMin = Convert.ToInt32(hddRangeValueMin.Value.Replace(".", ""));
                int rangeValueMax = Convert.ToInt32(hddRangeValueMax.Value.Replace(".", ""));

                string region = ddlRegion.SelectedItem.Value;
                string brand = ddlBrand.SelectedItem.Value;
                string orderBy = ddlOrder.SelectedItem.Value;
                int[] range = new int[] { rangeValueMin, rangeValueMax };

                this.PopulateFilteredItems(categoryId, region, brand, orderBy, range);
            }
            catch (Exception ex)
            {
                Literal errorMessage = new Literal();
                errorMessage.Text = ex.Message;

                this.Controls.Clear();
                this.Controls.Add(errorMessage);
            }
        }

        private void SetParameters(string catId)
        {
            int number;
            int categoryId = Int32.TryParse(catId, out number) ? number : 0;

            #region Set category image
            List<CategoryItem> categories = SharePointConnector.GetCategories();

            foreach (CategoryItem item in categories)
            {
                if (item.Id == categoryId)
                {
                    litCategoryImage.Text = string.Format(
                        @"<span style=""background:url({0}) no-repeat 50% 50%""><span>{1}</span></span>",
                        item.Image, item.Title);
                    break;
                }
            }
            #endregion

            #region Set filter options
            List<string> brands = SharePointConnector.GetBrands(categoryId);

            ddlBrand.DataSource = brands;
            ddlBrand.DataBind();
            #endregion
        }

        private void PopulateSuggestions(string catId)
        {
            int number;
            int categoryId = Int32.TryParse(catId, out number) ? number : 0;
            List<CatalogItem> suggestions = SharePointConnector.GetSuggestions(0);

            int count = 0;
            foreach (CatalogItem item in suggestions)
            {
                if (item.CategoryId == categoryId)
                {
                    litSuggestions.Text += string.Format(
                        @"<li><a href=""{0}""><img src=""{1}""/></a><p><span>{2}</span><br/>{3}</p></li>",
                        item.Url(), item.Image, FormatedPoints(item.Points) + " puntos", item.Title);
                    count++;

                    if (count == 6)//six is the limit
                        break;
                }
            }

            if (string.IsNullOrEmpty(litSuggestions.Text))
                litSuggestions.Text = "No existen productos sugeridos en este momento.";
        }

        private void PopulateAllItems(string catId)
        {
            int number;
            int categoryId = Int32.TryParse(catId, out number) ? number : 0;
            List<CatalogItem> catalogItems = SharePointConnector.GetCatalog(0, "ID", true);

            foreach (CatalogItem item in catalogItems)
            {
                if (item.CategoryId == categoryId)
                {
                    litCategoryItems.Text += string.Format(
                        @"<li><a href=""{0}""><img src=""{1}""/></a><p><span>{2}</span><br/>{3}</p></li>",
                        item.Url(), item.Image, FormatedPoints(item.Points) + " puntos", item.Title);
                }
            }

            if (string.IsNullOrEmpty(litCategoryItems.Text))
                litCategoryItems.Text = "No existen productos para esta categoría en este momento.";
        }

        private void PopulateFilteredItems(string catId, string region, string brand, string orderBy, int[] range)
        {
            litCategoryItems.Text = "";

            int number;
            int categoryId = Int32.TryParse(catId, out number) ? number : 0;
            List<CatalogItem> catalogItems = new List<CatalogItem>();

            switch (orderBy)
            {
                case "PUNTOS_ASC":
                    catalogItems = SharePointConnector.GetCatalog(0, "Puntos", true);
                    break;
                case "PUNTOS_DES":
                    catalogItems = SharePointConnector.GetCatalog(0, "Puntos", false);
                    break;
                case "PRODUCTO":
                    catalogItems = SharePointConnector.GetCatalog(0, "Title", true);
                    break;
            }

            foreach (CatalogItem item in catalogItems)
            {
                if (item.CategoryId == categoryId && item.Region.Contains(region) && item.Brand == brand)
                {
                    if (item.Points >= range[0] && item.Points <= range[1])
                    {
                        litCategoryItems.Text += string.Format(
                            @"<li><a href=""{0}""><img src=""{1}""/></a><p><span>{2}</span><br/>{3}</p></li>",
                            item.Url(), item.Image, FormatedPoints(item.Points) + " puntos", item.Title);
                    }
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
