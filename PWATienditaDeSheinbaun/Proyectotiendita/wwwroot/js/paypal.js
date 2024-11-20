function initPayPalButton(totalCompra) {    
    paypal.Buttons({
        //De manera forzosa, solo implementa el pago con tarjeta
        fundingSource: paypal.FUNDING.CARD, 
        createOrder: function (data, actions) {
            return actions.order.create({
                purchase_units: [{
                    amount: {
                        value: totalCompra.toFixed(2)
                    }
                }]
            });
        },

        //Captura los detalles del usuario
        onApprove: function (data, actions) {
            return actions.order.capture().then(function (details) {
                alert('Pago realizado con éxito, ' + details.payer.name.given_name);
                window.location.replace("https://localhost:44330/track-vehicle");

            });
        }
    }).render('#paypal-button-container');
}
    