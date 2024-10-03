function validate() {
    var i;
    var list_of_required_fields = $("input[id*='required']");
    var select_of_required_fields = $("select[id*='required']");
  
    var no_of_required_fields = $("input[id*='required']").length;
    var no_of_select_of_required_fields = $("select[id*='required']").length;
    for (i = 0; i < no_of_required_fields; i++) {
        if (list_of_required_fields[i].value == "") {
            list_of_required_fields[i].style.borderColor = 'red';

        }
    }
    for (i = 0; i < no_of_select_of_required_fields; i++) {
        if (select_of_required_fields[i].value == "") {
            select_of_required_fields[i].style.borderColor = 'red';

        }
    }

}