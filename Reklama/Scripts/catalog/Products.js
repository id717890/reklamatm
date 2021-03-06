$(document).ready(function () {
	
	$('#CategoryId').change(function () {
	    var groupId = $.attr(this, 'value');
		var shopId = $('#ShopId').val();
		var url = "/Shops/GetShopCategories";

        $.post(
            url,
            { shopId: shopId, groupId: groupId },
            function (data) {
                $.fn.loadSecondCategoryBox(data);
                var categoryId = data[0].Id;
				$.post(
					"/Shops/GetShopManufacturers",
					{ shopId: shopId, categoryId: categoryId },
					function (data) {
						$.fn.loadThirdCategoryBox(data);
					}
				);
            }
        );
    });
	
	$('#SecondCategoryId').change(function() {
	    var categoryId = $.attr(this, 'value');
	    var shopId = $('#ShopId').val();
		
		$.post(
			"/Shops/GetShopManufacturers",
			{ shopId: shopId, categoryId: categoryId },
			function (data) {
				$.fn.loadThirdCategoryBox(data);
			}
		);
	});
	
    $.fn.loadSecondCategoryBox = function (data) {
        $("#SecondCategoryId").html("");

        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                $("#SecondCategoryId").append("<option value='" + data[i].Id + "'>" + data[i].Name + "</option>");
            }
        }
    };
	
	$.fn.loadThirdCategoryBox = function (data) {		
		$(".persProdSection").html("");
		if (data.length > 0) {
			$(".persProdSection").append('<span>Производитель:</span> <select id="ThirdCategoryId" name="ThirdCategoryId">');
			$("#ThirdCategoryId").html("");
			for(var i = 0; i < data.length; i++) {
				$("#ThirdCategoryId").append("<option value='" + data[i].Id + "'>" + data[i].Name + "</option>");
			}
			$(".persProdSection").append('</select>');
		}
	};
	
	
	$('.changeButton').bind('click', function() {
	    var productId = $(this).attr('Id');
		var price = $(this).parent().parent().children('.ppInfo').children('input').val();
		var shopId = $('#ShopId').val();
		
		$.post(
		"/Shops/UpdateShopProduct",
		{ shopId: shopId, productId: productId, price: price },
		function (data) {
			if(data.status == 'success') {
				alert('Цена успешно изменена');
			}
			else {
				alert('Возникла ошибка при измении цены товара');
			}
		},
		"json"
		);
	});
	
	$('.removeButton').bind('click', function () {
	    var t = $(this);
 	    var productId = t.attr('ProductId');
 	    var shopId = $('#ShopId').attr('value');

 	    var _confirm = confirm('Удалить товар?');
 	    if (_confirm) {
 	        $.post(
            "/Shops/RemoveProductFromShop",
            { productId: productId, shopId: shopId },
            function (data) {
                if (data.status == 'success') {
                    t.closest("li").remove();
                }
                else {
                    alert('Возникла ошибка при удалении товара из магазина');
                }
            },
            "json"
            );
 	    }
	}); 
});