$(document).ready(function () {

    $("#ingredientDialog").hide();
    $("#stepDialog").hide();

    //add the modal for adding an ingredient to a recipe
    $("#btnAddIngredient").click(function () {
        $("#ingredientDialog").dialog({
            buttons: [
                {
                    text: "Add",
                    click: function () {
                        var ingredient = $("#txtIngredientName").val();
                        var quantity = $("#txtQuantity").val();
                        //build out the table row to add to the current ingredients table
                        var ingredientTR = "<tr>";
                        ingredientTR += "<td>" + ingredient + "</td>";
                        ingredientTR += "<td>" + quantity + "</td>";
                        ingredientTR += "<td><button type='button' class='removeIngredient'>X</button></td>";
                        ingredientTR += "</tr>";
                        $("#tblCurrentIngredients tbody").append(ingredientTR);
                        $("#txtIngredientName").val("");
                        $("#txtQuantity").val("");
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
        $("#stepDialog").dialog({
            buttons: [
                {
                    text: "Add",
                    click: function () {
                        //build out the table row, add it, and reset the step fields
                        var stepText = $("#txtStepValue").val();
                        var stepTR = "<tr>";
                        stepTR += "<td>" + stepText + "</td>";
                        stepTR += "<td><button type='button' class='removeStep'>X</button></td>";
                        stepTR += "</tr>";
                        $("#tblCurrentSteps tbody").append(stepTR);
                        $("#txtStepValue").val("");
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
        })
    });

    //if they want to remove a step remove the TR
    $(document).on("click", ".removeStep, .removeIngredient", function () {
        var parentTR = $(this).closest("tr");
        $(parentTR).remove();
    })
});