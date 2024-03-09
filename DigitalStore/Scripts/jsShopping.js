$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tQuantity = $('#quantity_value').text();
        if (tQuantity != '') {
            quantity = parseInt(tQuantity);
            location.reload();
        }
        location.reload();

        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.success) {
                    $('#checkout_items').html(rs.Count);
                    location.reload();
                }
                location.reload();
            }
        });
    });

    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = $('#Quantity_' + id).val();
        Update(id, quantity);
        location.reload();
    });


    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var conf = confirm('Bạn muốn xóa hết sản phẩm trong giỏ hàng?');
        if (conf == true) {
            DeleteAll();
            location.reload();
        }

    });


    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm(" Bạn có chắc chắn xóa không ?");
        if (conf === true) {
            $.ajax({
                url: "/shoppingcart/delete",
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.success) {
                        $('#trow_' + id).remove();
                        location.reload();
                    }
                }
            });
        }
        location.reload();
    });
});

function ShowCount() {
    $.ajax({
        url: '/ShoppingCart/ShowCount',
        type: 'GET',
        success: function (rs) {         
            if (rs.Success) {
                $('#checkout_items').html(rs.Count);
                $('.checkout_items').html(rs.Count);
            }
            $('#checkout_items').html(rs.Count);
            $('.checkout_items').html(rs.Count);
        }
        
    });
}

function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.success) {
                LoadCart();
            }
        }
    });
}

function Update(id, quantity) {
    $.ajax({
        url: '/shoppingcart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.success) {
                LoadCart();
            }
        }
    });
}

function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partical_Item_Cart',
        type: 'GET',
        success: function (rs) {
            if (rs.success) {
                $('#load_data').html(rs);
            }
        }
    });
}
