<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WPPageCategoryUserControl.ascx.cs" Inherits="BMSCPointsClub.WPPageCategory.WPPageCategoryUserControl" %>

<asp:HiddenField ID="hddRangeValueMin" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hddRangeValueMax" runat="server" ClientIDMode="Static" />

<div class="content_area">
  <div class="content">
    <div class="cp-logo">
      <span></span>
      <img src="/_catalogs/masterpage/bmsc/pointsclub/images/text_clubpuntos.png" alt="" />
    </div>
    <div class="cp-circle nobefore noafter category">
      <asp:Literal ID="litCategoryImage" runat="server"></asp:Literal>
    </div>
    <div class="cp-circle filled">
      <span><a href="/Paginas/Club%20de%20Puntos/BienvenidoClubPuntos.aspx?reg=LBS#cats" title="categorías"></a></span>
      <h1 class="left">categorías</h1>
    </div>
    <div class="cp-circle">
      <span></span>
      <h1 class="right">sugerencias</h1>
    </div>
    <div class="cp-catalog suggestions">
      <ul class="grid small">
        <asp:Literal ID="litSuggestions" runat="server"></asp:Literal>
      </ul>
    </div>
    <div class="cp-circle">
      <span></span>
      <h1 class="left forfilter">todos los resultados</h1>
    </div>
    <div class="cp-filters category">
      <ul>
        <li>SUCURSAL
          <asp:DropDownList ID="ddlRegion" runat="server">
            <asp:ListItem Value="" Text="(ELIGE UNA SUCURSAL)"></asp:ListItem>
            <asp:ListItem Value="LBS" Text="LAS BRISAS"></asp:ListItem>
            <asp:ListItem Value="LPZ" Text="LA PAZ"></asp:ListItem>
            <asp:ListItem Value="CBB" Text="COCHABAMBA"></asp:ListItem>
            <asp:ListItem Value="SCZ" Text="SANTA CRUZ"></asp:ListItem>
            <asp:ListItem Value="SCR" Text="SUCRE"></asp:ListItem>
            <asp:ListItem Value="NCL" Text="NACIONAL"></asp:ListItem>
          </asp:DropDownList>
        </li>
        <asp:Literal ID="litBrand" runat="server"></asp:Literal>
        <li>MARCA
          <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>
        </li>
        <li>ORDEN
          <asp:DropDownList ID="ddlOrder" runat="server">
            <asp:ListItem Value="PUNTOS_ASC" Text="PUNTOS DE MENOR A MAYOR"></asp:ListItem>
            <asp:ListItem Value="PUNTOS_DES" Text="PUNTOS DE MAYOR A MENOR"></asp:ListItem>
            <asp:ListItem Value="PRODUCTO" Text="NOMBRE DEL PRODUCTO"></asp:ListItem>
          </asp:DropDownList>
        </li>
        <li>RANGO DE PUNTOS
          <div id="theslider"></div>
        </li>
        <li><asp:Button ID="btnConfirm" runat="server" Text="aplicar filtros" OnClick="btnConfirm_Click" /></li>
      </ul>
    </div>
    <div class="cp-catalog category">
      <ul class="grid effect-2" id="grid">
        <asp:Literal ID="litCategoryItems" runat="server"></asp:Literal>
      </ul>
    </div>
  </div>
</div>

<asp:Literal ID="litSliderScript" runat="server"></asp:Literal>