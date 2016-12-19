


$(document).ready(function () {

    $("#btnSubmit").click(function (e) {
        debugger;
        e.preventDefault();
        var dropValue = $('#BatchId :selected').val();

        var url = "/Report/ViewReport/";

        if (dropValue == "") {

            return false;
        }
        else {
            $.ajax({

                url: url,
                type: "GET",
                data: { batchId: dropValue },
                dataType: "html",
                success: function (data) {

                    $("#reportPartial").html(data);
                    return false;

                },
                error: function () {
                    alert('Error');
                }

            });
        }

    });

});