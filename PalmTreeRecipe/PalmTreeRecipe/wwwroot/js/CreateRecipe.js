$(document).ready(function () {

    $("#ingredientDialog").hide();

    //add the modal for adding an ingredient to a recipe
    $("#btnAddIngredient").click(function () {
        $("#ingredientDialog").dialog({
            buttons: [
                {
                    text: "Add",
                    click: function () {
                        $(this).dialog("close");
                    }
                },
                {
                    text: "Cancel",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ],
            modal: true
        });
    });

    //if they click to add a step we want to show the add step modal
    $("#btnAddStep").click(function () {

    });
});