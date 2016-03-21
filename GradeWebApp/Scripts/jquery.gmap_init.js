

$(function () {
    $("#map").gMap({
        markers: [{
            latitude: 52.244266510009766,
            longitude: 20.92386817932129,
            html: 'Fabro sp.j.<br />ul. Babimojska 11 lok. 7A<br />01-466 Warszawa',
            icon: {
                image: '/Images/gmap_pin_mint.png',
                iconsize: [26, 46],
                iconanchor: [12, 46],
                infowindowanchor: [12, 0]
            }
        }],
        zoom: 15
    });


});

