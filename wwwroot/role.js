// wwwroot/js/scripts.js

$(document).ready(function() {
    // Fonction pour gérer la visibilité en fonction de l'argument
    window.manageVisibility = function(role) {
        // Cache tous les éléments avec la classe 'subscriber'
        if (role === "SUBSCRIBER") {
            $(".subscriber").css("visibility", "visible");
            $(".admin").css("visibility", "hidden");
        }
        // Cache tous les éléments avec la classe 'admin'
        else if (role === "ADMIN") {
            $(".subscriber").css("visibility", "visible");
            $(".admin").css("visibility", "visible");
        }
    };
});
