// wwwroot/js/scripts.js

$(document).ready(function () {
    // Fonction pour gérer la visibilité en fonction de l'argument
    window.manageVisibility = function (role) {
        // Cache tous les éléments avec la classe 'subscriber'
        if (role === "SUBSCRIBER") {
            $(".subscriber").show();
            $(".admin").hide();
        }
        // Cache tous les éléments avec la classe 'admin'
        else if (role === "ADMIN") {
            $(".subscriber").hide()
            $(".admin").show();
        }
        else {
            $(".subscriber").hide()
            $(".admin").hide();
        }
    };
    window.updateUserState = function (isLoggedIn, username) {
        if (isLoggedIn) {
            $("#loginlink").hide();
            $("#logoutlink").show();
            $("#profil").show();
            if (username) {
                $("#profil").text(username);
            }
        } else {
            $("#loginlink").show();
            $("#logoutlink").hide();
            $("#profil").hide();
        }
    };
    window.setSubscriber = function (NomSubscriber, BudgetSubscriber) {
        // Met à jour le texte de l'élément avec l'ID 'NomSubscriber'
        $("#NomSubscriber").text(NomSubscriber);

        // Met à jour le texte de l'élément avec l'ID 'SoldeSubscriber'
        $("#BudgetSubscriber").text(BudgetSubscriber);
    };
    window.hello = function (Nom) {
        alert("Hello!" + Nom);
    };
});
