// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function AddToCart(id){
    jQuery.ajax({
        type: 'POST',
        url: '/Cart/AddToCart/' + id,
        dataType: 'json',
        success: function (response) {
            $("#Success").text(response).show().delay(2000).hide("slow");
        },
        error: function (xhr) {
            if (xhr.status == 403)
            {
                $("#Fail").text("Only customers can perform this action").show().delay(2000).hide("slow");
            }
            else
            {
                $("#Fail").text("Please login to perfrom this action").show().delay(2000).hide("slow");
            }
        },

    });
}

