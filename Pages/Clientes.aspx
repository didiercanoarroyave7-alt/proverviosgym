<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="GYMFIT.Pages.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Gestión de clientes</h2>

    <div class="row">
        <div class="col-md-4">
            <div class="card shadow-sm mb-4">
                <div class="card-header bg-primary text-white">Nuevo cliente</div>
                <div class="card-body">
                    <asp:Label ID="lblMsg" runat="server" CssClass="alert d-block" Visible="false" />
                    <div class="mb-2">
                        <label class="form-label">Documento *</label>
                        <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-2">
                        <label class="form-label">Nombre completo *</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-2">
                        <label class="form-label">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-2">
                        <label class="form-label">Contacto emergencia</label>
                        <asp:TextBox ID="txtContacto" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Tel. emergencia</label>
                        <asp:TextBox ID="txtTelEmergencia" runat="server" CssClass="form-control" />
                    </div>
                    <asp:Button ID="btnGuardar" runat="server" Text="Registrar cliente" CssClass="btn btn-primary w-100" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
        <div class="col-md-8">
            <div class="card shadow-sm">
                <div class="card-header">Clientes registrados</div>
                <div class="card-body p-0">
                    <asp:GridView ID="gvClientes" runat="server" CssClass="table table-striped table-hover mb-0" 
                                  AutoGenerateColumns="false" EmptyDataText="No hay clientes.">
                        <Columns>
                            <asp:BoundField DataField="Documento" HeaderText="Documento" />
                            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre" />
                            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="FechaRegistro" HeaderText="Registro" DataFormatString="{0:dd/MM/yyyy}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
