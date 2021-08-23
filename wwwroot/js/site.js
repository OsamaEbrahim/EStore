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
        error: function () {
            $("#Fail").text("Unable To Add To Cart").show().delay(2000).hide("slow");
        },

    });
}

