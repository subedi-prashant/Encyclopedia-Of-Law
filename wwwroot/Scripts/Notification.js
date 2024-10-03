$(document).ready(function () {
    var PostBackURL = '/LawyerInfo/GetNotifications';

    $.ajax({
        type: "GET",
        url: PostBackURL,
        datatype: "json",
        success: function (data) {

            

            $(data).each(function (index, emp) {
                var x = new Date(emp.requestDate);
                $("#ulNoti").append('<li id="ss" class="notification-item"><div id="name"><i class="bi bi-exclamation-circle text-warning"></i><h4 id="dd" class="notifcation_Subject">' + emp.subject + '</h4><p class="notifcatio_massage_date">' + emp.message + '</p><p class="notifcatio_massage_date">' + x.toLocaleDateString() + '</p></div></li>');
                console.log(emp);

                $("#dd").click(function () {
                    console.log(5);
                    window.location.href = 'Request/GetDetailsByUser/4';
                    
                });
               
            });

        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });


    

});