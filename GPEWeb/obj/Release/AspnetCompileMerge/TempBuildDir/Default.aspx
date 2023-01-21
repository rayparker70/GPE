<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GPEWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Genox Pricing Engine</h1>
        <p class="lead">Build a <strong>Estimate</strong> for a <strong>Customer</strong> based on various pricing <strong>Models</strong> that contain predetermined <strong>Inputs, Revenue, Expenses</strong> and <strong>Rates</strong>.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting Started</h2>
            <p>
                Select the Estimate Tab and Add a new Estimate.&nbsp; Enter the appropriate data in the input fields and Click Calculate.</p>
            <p>
                <a class="btn btn-default" href="Estimate">Get Started &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Create a Pricing Model</h2>
            <p>
                Create a Pricing Model by selecting the Model tab and choose Add. Add the appropriate Inputs, Revenue, Expenses, Rates and Groups to the model.  Set the default values.</p>
            <p>
                <a class="btn btn-default" href="Model">Get Started &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Create Customers</h2>
            <p>
                Create Customers that can be used on a Model.&nbsp;</p>
            <p>
                <a class="btn btn-default" href="Customer">Get Started &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
