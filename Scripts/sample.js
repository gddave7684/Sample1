$(document).ready(function () {

    //global
    var refName;

    function reloadPartial(status) {
        if (status == "ok") {
            $("#tblPartial").load("/Task/GetView", { viewName: "_SwitchTable" });
        }
    }

    $(document).on('click', '#tblRaw .dataFromRow', function () {
        //console.log($('tr').index(this)) // uncomment for debugging;

        refName = this.cells[0].textContent; 
        document.getElementById('toModalName').innerHTML = this.cells[0].textContent;
        document.getElementById('toModalDescription').innerHTML = this.cells[1].textContent;
        document.getElementById('toStatus').checked = ( this.cells[2].textContent == "Active") ? true : false;       

    });

    $("#switchCreate").click(function () {
        var data = $("#toInsertData").serialize();

        $.post("/Task/PostNewData", data, function (response) {
            reloadPartial(response.status);
        });

        $(function () {
            $('#exampleModalCenter').modal('toggle');
        });

        return false;
    }); //END PostNewData


    $("#btn_Delete").click(function () {

        var nameTodelete = refName;
        var confirmDelete = confirm("Delete switch " + nameTodelete + " ?");

        var data = $.param({ nameTodelete: nameTodelete });
        if (confirmDelete) {
            $.post("/Task/DeleteSwitch", data, function (response) {
                reloadPartial(response.status);
            });
           
        }
    }); //END DeleteSwitch

    $("#btn_Save").click(function () {
        var updateStatus = $('#toStatus').is(':checked');
        var encodedRecursively = $.param({ updateStatus: updateStatus, refName: refName });

        $.post("/Task/SwitchUpdate", encodedRecursively, function (response) {
            reloadPartial(response.status);
        });
    }); //END SwitchUpdate

});//END document.ready

