
$(document).ready(function () {


    window.manageVisibility = function (role) {
        if (role === "SUBSCRIBER") {
            $(".subscriber").show();
            $(".admin").hide();
        }

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
        $("#NomSubscriber").text(NomSubscriber);

        $("#BudgetSubscriber").text(BudgetSubscriber);
    };
    window.ChangingPasswordVisibility = function (state) {
        var element = document.getElementById("reset-password");
        if (state) {
            element.style.display = 'block';
        }
        else {
            element.style.display = 'none';
        }
    }
});
