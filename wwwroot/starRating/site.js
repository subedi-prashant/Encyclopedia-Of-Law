

//Review stars
let star = document.querySelectorAll('.js-star');
let showValue = document.querySelector('#rating-value');

for (let i = 0; i < star.length; i++) {
    star[i].addEventListener('click', function () {
        i = this.value;

        showValue.innerHTML = i + " out of 5";
    });
}


//Review popup 
$(function () {
    var PlaceHoldelerElement = $('#PlaceHoldelerHere');
    $('button[data-bs-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            PlaceHoldelerElement.html(data);
            PlaceHoldelerElement.find('.modal').modal('show');
        })
    })

})




//Edit Request popup 
$(function () {
    var PlaceHoldelerElement = $('#PlaceHoldelerHere');
    $('button[data-bs-toggle="ajax-modal-Request"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            PlaceHoldelerElement.html(data);
            PlaceHoldelerElement.find('.modal').modal('show');
        })
    })

})





//Assign date popup 
$(function () {
    var PlaceHoldelerElement = $('#PlaceHoldelerHere');
    $('button[data-bs-toggle="ajax-modal-Assign"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            PlaceHoldelerElement.html(data);
            PlaceHoldelerElement.find('.modal').modal('show');
        })
    })

})



//Request Details popup 
$(function () {
    var PlaceHoldelerElement = $('#PlaceHoldelerHere');
    $('button[data-bs-toggle="ajax-modal-Details"]').click(function (event) {
        var url = $(this).data('url');
        var decodedUrl = decodeURIComponent(url);
        $.get(decodedUrl).done(function (data) {
            PlaceHoldelerElement.html(data);
            PlaceHoldelerElement.find('.modal').modal('show');
        })
    })

})


//Delete Request
$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);

        bootbox.confirm({
            closeButton: false,
            message: "Are you sure you want to delete this request?",
            buttons: {
                confirm: {
                    label: 'Ok',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-outline-secondary'
                }

            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: '/Request/DeleteByUser/' + btn.data('id'),
                        success: function () {
                            var requestContainer = btn.parents('.js-parent');
                            requestContainer.addClass('animate__animated animate__zoomOut');
                            setTimeout(function () {
                                requestContainer.remove();
                            }, 1000);
                            toastr.success('Request deleted successfully');
                        },
                        error: function () {
                            toastr.error('Something went wrong!');
                        }
                    });
                }
            }
        });


    });
});




//Confirm Date
$(document).ready(function () {
    $('.js-Confirm').on('click', function () {
        var btn = $(this);

        bootbox.confirm({
            closeButton: false,
            message: "Are you sure you want to confirm?",
            buttons: {
                confirm: {
                    label: 'Ok',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-outline-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: '/Request/ConfirmDate/' + btn.data('id'),
                        success: function () {
                            setTimeout(() => { location.reload(true) }, 2000)
                            toastr.success('Date confirmed successfully');
                        },
                        error: function () {
                            toastr.error('Something went wrong!');
                        }
                    });
                }
            }

        });


    });
});



//Cancel Date
$(document).ready(function () {
    $('.js-Cancel').on('click', function () {
        var btn = $(this);

        bootbox.confirm({
            closeButton: false,
            message: "Are you sure you want to Cancel?",
            buttons: {
                confirm: {
                    label: 'Ok',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-outline-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: '/Request/CancelDate/' + btn.data('id'),
                        success: function () {
                            setTimeout(() => { location.reload(true) }, 2000)
                            toastr.success('Date Cancelled successfully');
                        },
                        error: function () {
                            toastr.error('Something went wrong!');
                        }
                    });
                }
            }

        });


    });
});





// Reject Request
$(document).ready(function () {
    $('.js-Reject').on('click', function () {
        var btn = $(this);

        bootbox.confirm({
            closeButton: false,
            message: "Are you sure you want to reject this request?",
            buttons: {
                confirm: {
                    label: 'Ok',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-outline-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: '/Request/RejectRequest/' + btn.data('id'),
                        success: function () {
                            setTimeout(() => { location.reload(true) }, 2000)
                            toastr.success('Request Rejected successfully');
                        },
                        error: function () {
                            toastr.error('Something went wrong!');
                        }
                    });
                }
            }

        });


    });
});



//Complete Request
$(document).ready(function () {
    $('.js-Complete').on('click', function () {
        var btn = $(this);

        bootbox.confirm({
            closeButton: false,
            message: "Are you sure you want to Complete this request?",
            buttons: {
                confirm: {
                    label: 'Ok',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-outline-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: '/Request/CompleteRequest/' + btn.data('id'),
                        success: function () {
                            setTimeout(() => { location.reload(true) }, 2000)
                            toastr.success('Request Completed successfully');
                        },
                        error: function () {
                            toastr.error('Something went wrong!');
                        }
                    });
                }
            }

        });


    });
});