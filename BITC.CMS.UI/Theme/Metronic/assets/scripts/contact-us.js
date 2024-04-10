var ContactUs = function () {

    return {
        //main function to initiate the module
        init: function () {
            var map;
            $(document).ready(function () {
                if ($("#map").length > 0) {
                    map = new GMaps({
                        div: '#map',
                        lat: 21.063879,
                        lng: 105.883238,
                    });
                    var marker = map.addMarker({
                        lat: 21.063879,
                        lng: 105.883238,
                        title: 'BITC Vietnam',
                        infoWindow: {
                            content: "<b>BITC Vietnam,</b> No. 20, 99/47/40 Duc Giang, Long Bien, Hanoi"
                        }
                    });

                    marker.infoWindow.open(map, marker);
                }
            });
        }
    };
}();