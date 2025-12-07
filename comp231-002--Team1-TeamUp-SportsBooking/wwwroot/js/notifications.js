$(document).ready(function () {

    // Load notifications only if dropdown exists
    if ($("#notificationDropdownContent").length === 0)
        return;

    loadNotifications();

    function loadNotifications() {

        $.ajax({
            url: "/api/notifications/list",
            method: "GET",
            success: function (notifications) {

                let html = "";

                if (!notifications || notifications.length === 0) {

                    html = "<div class='p-2 text-center text-muted'>No notifications</div>";

                    // No notifications → bell goes white
                    $("#notificationBell i").removeClass("bell-alert");

                } else {

                    notifications.forEach(n => {
                        html += `
                            <div class="notification-item p-2 border-bottom">
                                <strong>${n.message}</strong>
                                <br/>
                                <small class="text-muted">${n.createdAt}</small>
                            </div>
                        `;
                    });

                    // Notifications exist → bell turns RED
                    $("#notificationBell i").addClass("bell-alert");
                }

                $("#notificationDropdownContent").html(html);
            },
            error: function (err) {
                console.log("Notification load error:", err);
            }
        });
    }

    // When the bell is clicked → mark as "seen" (turn white)
    $("#notificationBell").on("click", function () {
        $("#notificationBell i").removeClass("bell-alert");
    });
});
