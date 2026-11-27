<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GYMFIT.Pages.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Bienvenido a GYMFIT</h2>
    <p class="lead">Sistema de gestión de membresías y control de acceso del gimnasio.</p>

    <div class="row mt-4">
        <div class="col-md-3">
            <div class="card text-bg-primary mb-3">
                <div class="card-body">
                    <h5 class="card-title">Control de acceso</h5>
                    <p class="card-text">Valida el ingreso de clientes por documento.</p>
                    <a href="ControlAcceso.aspx" class="btn btn-light btn-sm">Ir →</a>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card text-bg-success mb-3">
                <div class="card-body">
                    <h5 class="card-title">Clientes</h5>
                    <p class="card-text">Registrar y consultar afiliados.</p>
                    <a href="Clientes.aspx" class="btn btn-light btn-sm">Ir →</a>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card text-bg-warning mb-3">
                <div class="card-body">
                    <h5 class="card-title">Afiliar</h5>
                    <p class="card-text">Asignar plan y registrar pago.</p>
                    <a href="Afiliar.aspx" class="btn btn-light btn-sm">Ir →</a>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card text-bg-info mb-3">
                <div class="card-body">
                    <h5 class="card-title">Planes</h5>
                    <p class="card-text">Administrar tipos de membresía.</p>
                    <a href="Planes.aspx" class="btn btn-light btn-sm">Ir →</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
