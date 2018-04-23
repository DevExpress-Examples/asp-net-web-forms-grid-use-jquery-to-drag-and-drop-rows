var rowKey = -1;

function OnControlsInitialized(s, e) {
    $('.draggableRow').draggable({ //http://api.jqueryui.com/draggable/
        helper: 'clone',
        start: function (ev, ui) {
            var $sourceElement = $(ui.helper.context);
            var $draggingElement = $(ui.helper);
            var sourceGrid = ASPxClientGridView.Cast($draggingElement.hasClass("left") ? "gridFrom" : "gridTo");

            //style elements
            $sourceElement.addClass("draggingStyle");
            $draggingElement.addClass("draggingStyle");
            $draggingElement.width(sourceGrid.GetWidth());

            //find key
            rowKey = sourceGrid.GetRowKey(sourceGrid.GetTopVisibleIndex() + $sourceElement.index() - 1);
        },
        stop: function (e, ui) {
            $(".draggingStyle").removeClass("draggingStyle");
        }
    });

    var settings = function (className) {
        return {
            tolerance: "intersect",
            accept: className,
            drop: function (ev, ui) {
                $(".targetGrid").removeClass("targetGrid");
                var leftToRight = ui.helper.hasClass("left");
                cbPanel.PerformCallback(rowKey + "|" + leftToRight);
            },
            over: function (ev, ui) {
                $(this).addClass("targetGrid");
            },
            out: function (ev, ui) {
                $(".targetGrid").removeClass("targetGrid");
            }
        };
    };

    //http://api.jqueryui.com/droppable/
    $(".droppableLeft").droppable(settings(".right"));
    $(".droppableRight").droppable(settings(".left"));

}