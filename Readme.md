<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1810)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# How to use jQuery to drag and drop items from one ASPxGridView to another


<p>The example demonstrates how to use the jQuery framework to drag an item from one grid to another.</p>
<p>- Use jQuery UI <a href="http://jqueryui.com/draggable/">Draggable</a> and <a href="http://jqueryui.com/droppable/">Droppable</a> plug-ins;<br />- Define "draggable" and "droppable" items:</p>


```aspx
<Styles>
    <Table CssClass="droppableLeft"></Table>
    <Row CssClass="draggableRow left"></Row>
</Styles>

```


<p> - Use the invisible <strong>ASPxGlobalEvents</strong> control and handle its client-side ControlsInitialized/EndCallback events;<br />- Initialize the defined draggable/droppable items via the corresponding jQuery selectors. The "start" event handler can be used to obtain the key of the dragged row and apply conditional styling to it:</p>


```js
start: function (ev, ui) {
    var $sourceElement = $(ui.helper.context);
    var $draggingElement = $(ui.helper);
     //style elements
    $sourceElement.addClass("draggingStyle");
    $draggingElement.addClass("draggingStyle");
     //find key
    var sourceGrid = ASPxClientGridView.Cast($draggingElement.hasClass("left") ? "gridFrom" : "gridTo");
    rowKey = sourceGrid.GetRowKey($sourceElement.index() - 1);
}

```


<p>- Handle the "drop" event of the "droppable" items and perform a callback to the callback panel that has both grids nested inside to perform the data editing functionality.</p>
<p>Select the "script.js" source file and review the comments to find an illustration of the above steps.</p>
<br />
<p><strong>See </strong><strong>A</strong><strong>lso:<br /></strong><a href="https://www.devexpress.com/Support/Center/p/T116869">T116869: GridView - How to drag and drop items from one grid to another</a><strong><br /> </strong><a href="https://www.devexpress.com/Support/Center/p/E4582">E4582: How to reorder ASPxGridView rows using buttons or drag-and-drop</a></p>

<br/>


<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=asp-net-web-forms-grid-use-jquery-to-drag-and-drop-rows&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=asp-net-web-forms-grid-use-jquery-to-drag-and-drop-rows&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
