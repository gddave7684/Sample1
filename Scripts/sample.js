$(document).ready(function () {

    var refName;

    $(document).on('click', '.dataFromRow', function () {
        //alert($('tr').index(this));
        document.getElementById('toModalName').innerHTML = this.cells[0].textContent;
        refName = this.cells[0].textContent; 
    });

    $("#switchCreate").click(function () {
        var data = $("#toInsertData").serialize();
        $.post({
            method: "POST",
            url: "/Task/PostNewData",
            data: data,
            dataType: "json",
            cache: false,
            success: function (data) {
                if (data.status == "ok") {
                    $("#tblPartial").load("/Task/GetView", { viewName: "_SwitchTable" });
                }
            },
        });

        $(function () {
            $('#exampleModalCenter').modal('toggle');
        });
        return false;
    }); //END


    $("#btn_Delete").click(function () {

        var nameTodelete = refName;
        var confirmDelete = confirm("Delete switch?");

        var obj = { nameTodelete: nameTodelete};
        var data = $.param(obj);
        if (confirmDelete) {
            $.ajax({
                url: "/Task/DeleteSwitch",
                data: data,
                success: function (data) {
                    if (data.status == "ok") {
                        $("#tblPartial").load("/Task/GetView", { viewName: "_SwitchTable" });
                    }
                }
            });
        }
    }); //END


    $("#btn_Save").click(function () {
        var updateStatus = $('#status').is(':checked');
        var obj = { updateStatus: updateStatus, refName: refName };
        var encodedRecursively = $.param(obj);

        //debugger
        $.ajax({
            url:"/Task/SwitchUpdate",
            data: encodedRecursively,
            success: function (data) {
                if (data.status == "ok") {
                    $("#tblPartial").load("/Task/GetView", { viewName: "_SwitchTable" });                
                }
            },
            error: function (error) {

            }
        });
    }); //END

});

