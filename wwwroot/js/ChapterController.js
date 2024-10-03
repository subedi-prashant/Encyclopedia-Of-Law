function showHideRow(n) {
    var i;
    $("#" + 'hidden_row_' + n);
    for (i = 1; i < 20; i++) {
        $("#" + 'hidden_row' + i + '_' + n).toggle("active");
    }
} 