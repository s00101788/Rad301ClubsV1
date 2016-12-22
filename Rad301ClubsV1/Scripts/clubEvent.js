$(function () {
    $("a[data-modal]").on("click", function () {
        $("#clubEventModalContent").load(this.href, function () {
            $("#clubEventModal").modal({
                keyboard: true
            }, "show");

        });

        $("#clubevent").submit(function () {
            if ($("#clubevent").valid()) {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $("#clubEventModal").modal("hide");
                            location.reload();
                        } else {
                            $("#MessageToClient").text("The Club Event was not updated.");
                        }
                    },
                    error: function () {
                        $("#MessageToClient").text("The web server had an error.");
                    }
                });
                return false;
            }
        });

        return false;
    });

});