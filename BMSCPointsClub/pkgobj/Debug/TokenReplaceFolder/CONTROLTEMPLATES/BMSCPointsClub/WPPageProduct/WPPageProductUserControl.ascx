<%@ Assembly Name="BMSCPointsClub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2842c8834f7c81b0" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WPPageProductUserControl.ascx.cs" Inherits="BMSCPointsClub.WPPageProduct.WPPageProductUserControl" %>

<div class="content_area">
  <div class="content">
    <div class="cp-logo">
      <span></span>
      <img src="/_catalogs/masterpage/bmsc/pointsclub/images/text_clubpuntos.png" alt="" />
    </div>
    <div class="cp-circle nobefore filled">
      <asp:Literal ID="litProductCategory" runat="server"></asp:Literal>
    </div>
    <div class="cp-circle">
      <asp:Literal ID="litProductName" runat="server"></asp:Literal>
    </div>
    <div class="cp-details">
      <asp:Literal ID="litProductDetails" runat="server"></asp:Literal>
    </div>
    <div class="cp-circle">
      <span></span>
      <h1 class="left"></h1>
    </div>
    <div class="cp-faq">
      <div>
        <span><asp:Literal ID="litFaqA" runat="server"></asp:Literal></span>
      </div>
      <div>
        <span><asp:Literal ID="litFaqB" runat="server"></asp:Literal></span>
      </div>
    </div>
    <div class="cp-circle">
      <span></span>
      <h1 class="left">otros productos de la misma categoría</h1>
    </div>
    <div class="cp-catalog related">
      <ul class="grid small">
        <asp:Literal ID="litRelatedProducts" runat="server"></asp:Literal>
      </ul>
    </div>
  </div>
</div>
