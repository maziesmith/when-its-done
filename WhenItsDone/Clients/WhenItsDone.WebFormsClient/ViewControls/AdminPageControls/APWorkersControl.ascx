﻿<%@ Control Language="C#" AutoEventWireup="true"
    CodeBehind="APWorkersControl.ascx.cs"
    Inherits="WhenItsDone.WebFormsClient.ViewControls.AdminPageControls.APWorkersControl" %>

<div id="workers-list">
    <div class="APViewsWrapper" runat="server" id="WorkersTable">

        <table class="centered striped">
            <tr>
                <th data-field="Id" class="padding-5">Id</th>
                <th data-field="FirstName">First Name</th>
                <th data-field="LastName">Last Name</th>
                <th data-field="NumberOfDishes">Number Of Dishes</th>
                <th data-field="buttons"></th>
            </tr>
            <asp:Repeater runat="server"
                ID="WorkersList"
                ItemType="WhenItsDone.DTOs.WorkerVIewsDTOs.WorkerNamesIdDTO"
                DataSource="<%# this.Model.WorkersNamesAndId %>">

                <ItemTemplate>
                    <tr>
                        <td class="padding-5"><%# Item.Id %></td>
                        <td><%# Item.FirstName %></td>
                        <td><%# Item.LastName %></td>
                        <td><%# Item.NumberOfDishes %></td>
                        <td class="padding-5">
                            <asp:Button runat="server" Text="Info" CssClass="light-green waves-effect waves-light btn"
                                ID="RepeaterBtn"
                                CommandName="InfoClick"
                                CommandArgument="<%# Item.Id %>"
                                OnClick="InfoClick" />
                        </td>
                    </tr>

                </ItemTemplate>

            </asp:Repeater>
        </table>

    </div>
</div>
