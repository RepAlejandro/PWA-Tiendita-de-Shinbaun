// wwwroot/js/paypal.js
window.initPayPalButton = function (createOrderUrl, captureOrderUrl) {
    // Renderizar el botón de PayPal en el contenedor especificado
    // Asegúrate de tener la librería de PayPal incluida
    paypal.Buttons({
        createOrder: function (data, actions) {
            return actions.order.create({
                purchase_units: [{
                    amount: {
                        value: '0.01' // Cambia este valor por el monto de la compra
                    }
                }]
            }).then(function (orderId) {
                // Guarda el orderId para usarlo más adelante
                console.log('Order ID:', orderId);
                return orderId;
            });
        },
        onApprove: function (data, actions) {
            return actions.order.capture().then(function (details) {
                alert('Transaction completed by ' + details.payer.name.given_name);

                // Redirigir al usuario después de la transacción
                window.location.replace("https://localhost:7044/Compra"); // Cambia la URL a donde quieres redirigir
            });
        }
    }).render('#paypal-button-container');
};
