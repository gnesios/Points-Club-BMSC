<%@ Assembly Name="BMSCPointsClub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2842c8834f7c81b0" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WPPageWelcomeUserControl.ascx.cs" Inherits="BMSCPointsClub.WPPageWelcome.WPPageWelcomeUserControl" %>

<asp:Literal runat="server" ID="ltrScriptLoader" Text=""></asp:Literal>

<div class="content_area">
  <div class="content">
    <div class="cp-logo">
      <span></span>
      <img src="/_catalogs/masterpage/bmsc/pointsclub/images/text_clubpuntos.png" alt="" />
    </div>
    <div class="cp-header">
      <div class="hleft">
        <p class="title">Tus Puntos Acumulados</p>
        <p>Ingresa tu documento de identidad:</p>
        <asp:Label ID="lblValidation" runat="server" class="error-message" Text="* debes llenar ambos campos"></asp:Label>
        <asp:TextBox ID="txbId" runat="server" MaxLength="8" placeholder="(doc. de identidad)"></asp:TextBox>
        <asp:DropDownList ID="ddlCity" runat="server">
          <asp:ListItem Value="">(extensión)</asp:ListItem>
          <asp:ListItem Value="LP">La Paz</asp:ListItem>
          <asp:ListItem Value="CB">Cochabamba</asp:ListItem>
          <asp:ListItem Value="SC">Santa Cruz</asp:ListItem>
          <asp:ListItem Value="OR">Oruro</asp:ListItem>
          <asp:ListItem Value="PO">Potosí</asp:ListItem>
          <asp:ListItem Value="BE">Beni</asp:ListItem>
          <asp:ListItem Value="PA">Pando</asp:ListItem>
          <asp:ListItem Value="TJ">Tarija</asp:ListItem>
          <asp:ListItem Value="SU">Sucre</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnConfirm" runat="server" Text="Consultar" OnClick="btnConfirm_Click" />
        <span class="shadow"></span>
      </div>
      <div class="hcenter"></div>
      <div class="hright">
        <p class="title">Nuestro Catálogo de Premios</p>
        <asp:Literal ID="litCatalogLinks" runat="server"></asp:Literal>
        <a href="http://www.lasbrisas.com.bo/pages/club" class="link1" title="Las Brisas (enlace)"><img src="/_catalogs/masterpage/bmsc/pointsclub/images/icon_lasbrisas.png" alt="" /></a>
        <a href="#cats" class="link2" title="Catálogo (enlace)"><img src="/_catalogs/masterpage/bmsc/pointsclub/images/icon_calalog.png" alt="" /></a>
        <span class="shadow one"></span>
        <span class="shadow two"></span>
      </div>
    </div>
    <div class="cp-circle">
      <span></span>
      <h1 class="left">¿qué es el club de puntos?</h1>
    </div>
    <div class="cp-faq">
      <div>
        <span><asp:Literal ID="litFaqA" runat="server"></asp:Literal></span>
      </div>
      <div>
        <span><asp:Literal ID="litFaqB" runat="server"></asp:Literal></span>
      </div>
    </div>
    <div class="cp-circle" id="cats">
      <span></span>
      <h1 class="right">categorías</h1>
    </div>
    <div class="cp-categories">
      <asp:Literal ID="litCategories" runat="server"></asp:Literal>
    </div>
    <div class="cp-circle" id="suggs">
      <span></span>
      <h1 class="left forfilter">sugerencias del mes</h1>
    </div>
    <div class="cp-filters main">
      <ul>
        <li><a id="lbs" href="BienvenidoClubPuntos.aspx?reg=LBS#suggs">LAS BRISAS</a></li>
        <li><a id="lpz" href="BienvenidoClubPuntos.aspx?reg=LPZ#suggs">LA PAZ</a></li>
        <li><a id="cbb" href="BienvenidoClubPuntos.aspx?reg=CBB#suggs">COCHABAMBA</a></li>
        <li><a id="scz" href="BienvenidoClubPuntos.aspx?reg=SCZ#suggs">SANTA CRUZ</a></li>
        <li><a id="scr" href="BienvenidoClubPuntos.aspx?reg=SCR#suggs">SUCRE</a></li>
        <li><a id="ncl" href="BienvenidoClubPuntos.aspx?reg=NCL#suggs">NACIONAL</a></li>
      </ul>
    </div>
    <div class="cp-catalog">
      <ul class="grid effect-2" id="grid">
        <asp:Literal ID="litSuggestions" runat="server"></asp:Literal>
      </ul>
    </div>
  </div>
</div>
