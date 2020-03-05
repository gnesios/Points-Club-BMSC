using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSCPointsClub
{
    class SharePointConnector
    {
        #region Global and constant variables
        const string LIST_IMAGES = "Imágenes Club de Puntos";
        const string LIST_CATEGORIES = "Categorías de Premios";
        const string LIST_CATALOG = "Catálogo de Premios";
        const string LIST_SUGGESTIONS = "Sugerencias del Mes";
        const string LIST_PARAMETERS = "Parámetros Club Puntos";
        #endregion

        #region Get methods
        /// <summary>
        /// Get category's items.
        /// </summary>
        /// <returns></returns>
        internal static List<CategoryItem> GetCategories()
        {
            List<CategoryItem> categories = new List<CategoryItem>();

            #region SharePoint query
            SPListItemCollection queriedCategories = null;

            using (SPSite sps = new SPSite(SPContext.Current.Web.Url))
            using (SPWeb spw = sps.OpenWeb())
            {
                queriedCategories = spw.Lists[LIST_CATEGORIES].Items;
            }
            #endregion

            foreach (SPListItem item in queriedCategories)
            {
                int id = item.ID;
                string title = item.Title;
                string image = FormatedUrl(item["Imagen_x0020_categor_x00ed_a"].ToString());

                categories.Add(new CategoryItem(id, title, image));
            }

            return categories;
        }

        /// <summary>
        /// Get catalog's items. All items when parameter is 0.
        /// </summary>
        /// <param name="itemId">ID of the especific item.</param>
        /// <param name="order">Order type of the results.</param>
        /// <returns></returns>
        internal static List<CatalogItem> GetCatalog(int itemId, string orderBy, bool isAscending)
        {
            List<CatalogItem> catalog = new List<CatalogItem>();

            #region SharePoint query
            SPListItemCollection queriedCatalog = null;
            SPListItem queriedItem = null;

            using (SPSite sps = new SPSite(SPContext.Current.Web.Url))
            using (SPWeb spw = sps.OpenWeb())
            {
                if (itemId == 0)
                {
                    string ascending = isAscending ? "" : "Ascending='FALSE'";

                    SPQuery query = new SPQuery();
                    query.Query = string.Format(
                        "<OrderBy><FieldRef Name='{0}' {1} /></OrderBy>" +
                        "<Where><Eq><FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>0</Value></Eq></Where>",
                        orderBy, ascending);

                    queriedCatalog = spw.Lists[LIST_CATALOG].GetItems(query);
                }
                else
                {
                    queriedItem = spw.Lists[LIST_CATALOG].GetItemById(itemId);
                }
            }
            #endregion

            if (queriedItem != null)
            {
                int id = queriedItem.ID;
                string title = queriedItem.Title;
                string description = queriedItem["Descripci_x00f3_n"].ToString();
                string image = FormatedUrl(queriedItem["Imagen_x0020_referencia"].ToString());
                int points = Convert.ToInt32(queriedItem["Puntos"]);
                bool isNew = Convert.ToBoolean(queriedItem["Nuevo"]);
                string[] categoryArray = StringArrayTwo(queriedItem["Categor_x00ed_a0"]);
                int categoryId = Convert.ToInt32(categoryArray[0]);
                string category = categoryArray[1];
                string brand = queriedItem["Marca"].ToString();
                string region = queriedItem["Sucursal"].ToString();

                catalog.Add(new CatalogItem(id, title, description, image, points, isNew, category, categoryId, brand, region, "catalog"));
            }
            else
            {
                foreach (SPListItem item in queriedCatalog)
                {
                    int id = item.ID;
                    string title = item.Title;
                    string description = item["Descripci_x00f3_n"].ToString();
                    string image = FormatedUrl(item["Imagen_x0020_referencia"].ToString());
                    int points = Convert.ToInt32(item["Puntos"]);
                    bool isNew = Convert.ToBoolean(item["Nuevo"]);
                    string[] categoryArray = StringArrayTwo(item["Categor_x00ed_a0"]);
                    int categoryId = Convert.ToInt32(categoryArray[0]);
                    string category = categoryArray[1];
                    string brand = item["Marca"].ToString();
                    string region = item["Sucursal"].ToString();

                    catalog.Add(new CatalogItem(id, title, description, image, points, isNew, category, categoryId, brand, region, "catalog"));
                }
            }

            return catalog;
        }

        /// <summary>
        /// Get suggested items. All items when parameter is 0.
        /// </summary>
        /// <param name="itemId">ID of the especific item.</param>
        /// <returns></returns>
        internal static List<CatalogItem> GetSuggestions(int itemId)
        {
            List<CatalogItem> suggestions = new List<CatalogItem>();

            #region SharePoint query
            SPListItemCollection queriedSuggestions = null;
            SPListItem queriedItem = null;

            using (SPSite sps = new SPSite(SPContext.Current.Web.Url))
            using (SPWeb spw = sps.OpenWeb())
            {
                if (itemId == 0)
                {
                    SPQuery query = new SPQuery();
                    query.Query =
                        "<OrderBy><FieldRef Name='Id' Ascending='FALSE' /></OrderBy>" +
                        "<Where><Eq><FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>0</Value></Eq></Where>";

                    queriedSuggestions = spw.Lists[LIST_SUGGESTIONS].GetItems(query);
                }
                else
                {
                    queriedItem = spw.Lists[LIST_SUGGESTIONS].GetItemById(itemId);
                }
            }
            #endregion

            if (queriedItem != null)
            {
                int id = queriedItem.ID;
                string title = queriedItem.Title;
                string description = queriedItem["Descripci_x00f3_n"].ToString();
                string image = FormatedUrl(queriedItem["Imagen_x0020_referencia"].ToString());
                int points = Convert.ToInt32(queriedItem["Puntos"]);
                bool isNew = Convert.ToBoolean(queriedItem["Nuevo"]);
                string[] categoryArray = StringArrayTwo(queriedItem["Categor_x00ed_a0"]);
                int categoryId = Convert.ToInt32(categoryArray[0]);
                string category = categoryArray[1];
                string brand = queriedItem["Marca"].ToString();
                string region = queriedItem["Sucursal"].ToString();

                suggestions.Add(new CatalogItem(id, title, description, image, points, isNew, category, categoryId, brand, region, "suggs"));
            }
            else
            {
                foreach (SPListItem item in queriedSuggestions)
                {
                    int id = item.ID;
                    string title = item.Title;
                    string description = item["Descripci_x00f3_n"].ToString();
                    string image = FormatedUrl(item["Imagen_x0020_referencia"].ToString());
                    int points = Convert.ToInt32(item["Puntos"]);
                    bool isNew = Convert.ToBoolean(item["Nuevo"]);
                    string[] categoryArray = StringArrayTwo(item["Categor_x00ed_a0"]);
                    int categoryId = Convert.ToInt32(categoryArray[0]);
                    string category = categoryArray[1];
                    string brand = item["Marca"].ToString();
                    string region = item["Sucursal"].ToString();

                    suggestions.Add(new CatalogItem(id, title, description, image, points, isNew, category, categoryId, brand, region, "suggs"));
                }
            }

            return suggestions;
        }

        /// <summary>
        /// Get parameter items.
        /// </summary>
        /// <param name="filterGroup">Name of the group to filter by</param>
        /// <returns></returns>
        internal static List<ParameterItem> GetParameters(string filterGroup)
        {
            List<ParameterItem> parameters = new List<ParameterItem>();

            #region SharePoint query
            SPListItemCollection queriedParameters = null;

            using (SPSite sps = new SPSite(SPContext.Current.Web.Url))
            using (SPWeb spw = sps.OpenWeb())
            {
                SPQuery query = new SPQuery();
                query.Query = string.Format(
                    "<Where><Eq><FieldRef Name='Grupo' /><Value Type='Text'>{0}</Value></Eq></Where>",
                    filterGroup);

                queriedParameters = spw.Lists[LIST_PARAMETERS].GetItems(query);
            }
            #endregion

            foreach (SPListItem item in queriedParameters)
            {
                string title = item.Title;
                string group = item["Grupo"].ToString();
                string description = (item["Descripci_x00f3_n"] == null) ? "" : item["Descripci_x00f3_n"].ToString();
                string image = (item["Imagen"] == null) ? "" : item["Imagen"].ToString();
                string link = (item["Enlace"] == null) ? "" : item["Enlace"].ToString();

                parameters.Add(new ParameterItem(title, group, description, image, link));
            }

            return parameters;
        }

        /// <summary>
        /// Get brand names from a especific category.
        /// </summary>
        /// <param name="categoryId">ID of the category.</param>
        /// <returns></returns>
        internal static List<string> GetBrands(int categoryId)
        {
            List<string> brands = new List<string>();
            List<CatalogItem> catalogItems = GetCatalog(0, "Marca", true);

            foreach (CatalogItem item in catalogItems)
            {
                if (item.CategoryId == categoryId && !string.IsNullOrWhiteSpace(item.Brand) && !brands.Contains(item.Brand))
                    brands.Add(item.Brand);
            }

            return brands;
        }
        #endregion

        #region Support methods
        /// <summary>
        /// Get the correct url format from SharePoint field.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string FormatedUrl(string url)
        {
            string formatedUrl = "";

            if (url.Contains(","))
                formatedUrl = url.Split(',')[0].Trim();
            else
                formatedUrl = url;

            return formatedUrl;
        }

        /// <summary>
        /// Return a size 2 array like [2][NAME] from '2;#NAME' string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string[] StringArrayTwo(object value)
        {
            string[] array = new string[2];
            string[] stringSeparators = new string[] { ";#" };

            array = value.ToString().Split(stringSeparators, 2, StringSplitOptions.None);

            return array;
        }
        #endregion
    }

    class CategoryItem
    {
        int id;
        string title;
        string image;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        public CategoryItem()
        { }
        public CategoryItem(int id, string title, string image)
        {
            this.Id = id;
            this.Title = title;
            this.Image = image;
        }

        public string Url()
        { 
            return "/Paginas/Club%20de%20Puntos/CategoriaClubPuntos.aspx?cat=" + this.Id;
        }
    }

    class CatalogItem
    {
        int id;
        string title;
        string description;
        string image;
        int points;
        bool isNew;
        string category;
        int categoryId;
        string brand;
        string region;
        string library;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }
        public string Brand
        {
            get { return brand.ToUpper(); }
            set { brand = value; }
        }
        public string Region
        {
            get { return region; }
            set { region = value; }
        }
        public string Library
        {
            get { return library; }
            set { library = value; }
        }

        public CatalogItem()
        { }
        public CatalogItem(int id, string title, string description, string image, int points,
            bool isNew, string category, int categoryId, string brand, string region, string library)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Image = image;
            this.Points = points;
            this.IsNew = isNew;
            this.Category = category;
            this.CategoryId = categoryId;
            this.Brand = brand;
            this.Region = region;
            this.Library = library;
        }

        public string Url()
        {
            string formatedUrl = string.Format(
                "/Paginas/Club%20de%20Puntos/ProductoClubPuntos.aspx?pid={0}&lib={1}",
                this.Id, this.Library);

            return  formatedUrl;
        }
    }

    class ParameterItem
    {
        string title;
        string group;
        string description;
        string image;
        string link;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Group
        {
            get { return group; }
            set { group = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Image
        {
            get { return image; }
            set { image = value; }
        }
        public string Link
        {
            get { return link; }
            set { link = value; }
        }

        public ParameterItem()
        { }

        public ParameterItem(string title, string group, string description, string image, string link)
        {
            this.Title = title;
            this.Group = group;
            this.Description = description;
            this.Image = image;
            this.Link = link;
        }
    }
}
