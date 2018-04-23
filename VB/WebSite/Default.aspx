<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

	<script src="jquery-1.6.1.min.js" type="text/javascript"></script>

	<script src="jquery-ui-1.8.13.custom.min.js" type="text/javascript"></script>

	<title>Untitled Page</title>

	<script language="javascript" type="text/javascript">

	$(document).ready(function() {
		$('.draggable').draggable({ helper: 'clone' });
		$('.droppable').droppable(
		{
			//            accept: "#droppable",
			drop: function(ev, ui) {
				var box = document.getElementById('<%=imgBox.ClientID%>');
				if (box != null) {
					box.src = 'Images/boxfilled.gif';
				}
				//================================================================
				//make a clone of the dragged item -
				//the last column in the grid that also contains the image:
				//================================================================
				var x = (ui.draggable).clone();
				x.appendTo(".droppable");
				//================================================================
				//Set the visibility of the area that holds
				//items to visible:
				//================================================================
				var y = x.get('var');
				x.css("visiblity", "visible");
				//Get the Item Code:

				var ItemCode = $(x).text();
				//alert(ItemCode);
				//================================================================
				//Add rows to the panel (it's actually a table)
				//to display the selected item numbers:
				//================================================================
				var roundpanel = document.getElementById("pnKitSummary");
				var lastRow = roundpanel.rows.length;
				var row = roundpanel.insertRow(lastRow);
				var cellLeft = row.insertCell(0);
				var textNode = document.createTextNode(ItemCode);
				cellLeft.appendChild(textNode);
				var hid = document.getElementById('<%=hid.ClientID%>');
				//================================================================
				// Add the item code to a hidden field used with split on postback
				//================================================================
				hid.value = hid.value + ItemCode + ';';
				//Add the item code to a temporary holder
				var draghide = document.getElementById('<%=draghidden.ClientID%>');
				draghide.value = ItemCode;
				//Place the grid in Edit mode, add a new row
				grid.AddNewRow();
			}
		}
	  );
	});

	function getHidden() {
		var hid = document.getElementById('<%=draghidden.ClientID%>');
		return hid.value;
	}    
	</script>

</head>
<body>
	<form id="form1" runat="server">
		<div>
			<asp:HiddenField ID="hid" runat="server" />
			<asp:HiddenField ID="draghidden" runat="server" />
			<div id="mywrapper" style="width: 100%; overflow: hidden;">
				<div id="dleft" style="float: left; width: 28%; overflow: hidden;">
					<dxrp:ASPxRoundPanel ID="pnKitSummary" runat="server" Width="100%" HeaderText="Selected Items:"
						HorizontalAlign="Center" Style="z-index: 1;" ClientInstanceName="pnKitSummary">
						<PanelCollection>
							<dxp:PanelContent runat="server">
								<div class="droppable" id="var" style="height: 100%">
									<asp:Image ID="imgBox" runat="server" ImageUrl="~/Images/box.jpg" />
								</div>
								<dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid"
									Width="100%" runat="server" KeyFieldName="ID" AutoGenerateColumns="False" OnRowInserting="ASPxGridView1_RowInserting">
									<ClientSideEvents EndCallback="function(s, e) {
					grid.SetEditValue('ItemCode',getHidden());
		}" />
									<Columns>
										<dxwgv:GridViewDataTextColumn FieldName="ID" ReadOnly="True" VisibleIndex="0">
										</dxwgv:GridViewDataTextColumn>
										<dxwgv:GridViewDataColumn FieldName="ItemCode" VisibleIndex="1">
											<EditFormSettings Visible="True" />
										</dxwgv:GridViewDataColumn>
										<dxwgv:GridViewDataColumn FieldName="Qty" VisibleIndex="2">
										</dxwgv:GridViewDataColumn>
									</Columns>


								</dxwgv:ASPxGridView>
							</dxp:PanelContent>
						</PanelCollection>
					</dxrp:ASPxRoundPanel>
				</div>
				<div id="dright" style="float: right; overflow: hidden; width: 69%">
					<dxrp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" Width="100%" HeaderText="Product Selection:">
						<PanelCollection>
							<dxp:PanelContent>
								<asp:Panel ID="pnContent" runat="server">
									<dxwgv:ASPxGridView ID="gvMaster" EnableCallBacks="false" ClientInstanceName="gridMaster"
										EnableCallbackCompression="true" DataSourceID="gvDS" EnableRowsCache="true" Width="100%"
										runat="server" KeyFieldName="CategoryID" AutoGenerateColumns="False">
										<Columns>
											<dxwgv:GridViewCommandColumn VisibleIndex="0">
												<ClearFilterButton Visible="True">
												</ClearFilterButton>
											</dxwgv:GridViewCommandColumn>
											<dxwgv:GridViewDataColumn FieldName="CategoryID" Caption="CategoryID">
												<Settings AutoFilterCondition="Contains" />
											</dxwgv:GridViewDataColumn>
											<dxwgv:GridViewDataColumn FieldName="CategoryName" Caption="CategoryName">
												<Settings AutoFilterCondition="Contains" />
											</dxwgv:GridViewDataColumn>
											<dxwgv:GridViewDataHyperLinkColumn FieldName="CategoryName" Caption="Image" ReadOnly="True">
												<DataItemTemplate>
													<div class="draggable">
														<a href="#" title="Image Viewer">
															<asp:Image ID="img" runat="server" ImageUrl='~/Images/drag.jpg' />
														</a>
														<div class="dropped" id="hidItemCode" style="visibility: hidden;">
															<%#Eval("CategoryID")%>
														</div>
													</div>
												</DataItemTemplate>
											</dxwgv:GridViewDataHyperLinkColumn>
										</Columns>
									</dxwgv:ASPxGridView>
									<asp:AccessDataSource ID="gvDS" runat="server" DataFile="~/App_Data/nwind.mdb" SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]">
									</asp:AccessDataSource>
								</asp:Panel>
							</dxp:PanelContent>
						</PanelCollection>
					</dxrp:ASPxRoundPanel>
				</div>
			</div>
		</div>
	</form>
</body>
</html>