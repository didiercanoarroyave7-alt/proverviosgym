<%@ Page Title="Planes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Planes.aspx.cs" Inherits="GYMFIT.Pages.Planes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Planes de membresía</h2>

    <div class="row">
        <div class="col-md-4">
            <div class="card shadow-sm mb-4">
                <div class="card-header bg-success text-white">Nuevo plan</div>
                <div class="card-body">
                    <asp:Label ID="lblMsg" runat="server" CssClass="alert d-block" Visible="false" />
                    <div class="mb-2">
                        <label class="form-label">Nombre *</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Mensual" />
                    </div>
                    <div class="mb-2">
                        <label class="form-label">Duración (días) *</label>
                        <asp:TextBox ID="txtDias" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="mb-2">
                        <label class="form-label">Precio *</label>
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number" step="1000" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Descripción</label>
                        <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                    </div>
                    <asp:Button ID="btnGuardar" runat="server" Text="Crear plan" CssClass="btn btn-success w-100" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
        <div class="col-md-8">
            <div class="card shadow-sm">
                <div class="card-header">Planes disponibles</div>
                <div class="card-body p-0">
                    <asp:GridView ID="gvPlanes" runat="server" CssClass="table table-striped mb-0" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="NombrePlan" HeaderText="Plan" />
                            <asp:BoundField DataField="DuracionDias" HeaderText="Días" />
                            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C0}" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
