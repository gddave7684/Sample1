var table = document.getElementById('tblRaw'), rIndex;
for (var i = 0; i < table.rows.length; i++) {
    table.rows[i].onclick = function () {
        rIndex = this.rowIndex;
        document.getElementById('toModalName').innerHTML = this.cells[0].textContent;
        document.getElementById('toModalDescription').innerHTML = this.cells[1].textContent;
        var status = this.cells[2].textContent;
        if (status == "Active") {
            document.getElementById('status').checked = true;
        }
        else if (status == "Inactive") {
            document.getElementById('status').checked = false;
        }

    };
}