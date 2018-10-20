
var GET_PRODUCTS = "api/products/products/";

function getProducts(htmlId) {

    $.getJSON(GET_PRODUCTS)
        .done(function (data) {
            // On success, 'data' contains a list of products.
            $.each(data, function (key, item) {
                // Add a list item for the product.
                $('<li>', { text: formatItem(item) }).appendTo($('#' + htmlId));
            });
        });
}

var GET_PRODUCT = "api/products/product/";

function getProduct(inputId, outputId) {

    var id = $('#' + inputId).val();
    
    $.getJSON(GET_PRODUCT + id)
        .done(function (data) {
            $('#' + outputId).text(formatItem(data));
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#' + outputId).text('Error: ' + err);
        });
}

function formatItem(item) {
    return item.Name + ': $' + item.Price;
}

var CHANNEL_NAME = "my-channel";
var EVENT_NAME = "my-event";

function subscribe() {

    Pusher.logToConsole = true;

    var pusher = new Pusher('05b6d74f8a0e925376a1', {
        cluster: 'mt1',
        forceTLS: true
    });

    var channel = pusher.subscribe(CHANNEL_NAME);

    channel.bind(EVENT_NAME, function (data) {
        alert(JSON.stringify(data));
    });
}
