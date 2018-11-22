(function (hangfire) {

    hangfire.RecurringJobExtensions = (function () {
        function RecurringJobExtensions() {
            this._initialize();
        }

        RecurringJobExtensions.prototype._initialize = function () {
            var config = window.Hangfire.recurringJobExtensionsConfiguration;
            if (!config) return;
            if (!config.NeedAddRecurringJobButton) return;


            var recurring_templete = "{\"JobId\":\"\",\"Type\":\"\",\"MethodName\":\"\",\"Cron\":\"" + config.DefaultCron + "\",\"Queue\":\"" + config.DefaultQueue + "\",\"TimeZone\":\"" + config.DefaultTimeZone + "\"}";

            var button = '<button class="btn btn-sm btn-primary"  id="addRecurringJob"  data-toggle="modal" data-target="#recurringJobModal">' +
                '<span class="glyphicon glyphicon-plus"></span>' +
                config.AddRecurringJobButtonName +
                '</button>';
            var divModel =
                '<div class="modal inmodal" id="recurringJobModal" tabindex="-1" role="dialog" aria-hidden="true">' +
                '<div class="modal-dialog">' +
                '<div class="modal-content">' +
                '<div class="modal-header">' +
                '<h4 class="modal-title">' + config.AddRecurringJobButtonName + '</h4>' +
                '</div>' +
                '<div class="modal-body">' +
                '<div class="editor_holder" style="height: 250px;"></div>' +
                '</div>' +
                '<div class="modal-footer">' +
                ' <button type="button" class="btn btn-white" data-dismiss="modal">' + config.CloseButtonName + '</button>' +
                '<button type="button" class="btn btn-primary" id="addRecurringJob_save-model" data-url="' + config.AddRecurringJobUrl + '">' + config.SumbitButtonName + '</button>' +
                '</div>' +
                ' </div>' +
                ' </div>' +
                ' </div>';
            if (!button || !divModel) return;


            $('.page-header').append(button);
            $(document.body).append(divModel);

            var container = $('.editor_holder')[0];

            var options = {
                schema: {},
                mode: 'code'
            };
            var jsonEditor;
            try {
                jsonEditor = new JSONEditor(container, options);
            } catch (e) {
            }

            $('#addRecurringJob').click(function () {
                jsonEditor.setText(recurring_templete);
                jsonEditor.format();
            });


            $('#addRecurringJob_save-model').click(function () {
                var url = $(this).data("url");
                if (!url) return;
                var settings = {
                    "url": url,
                    "method": "POST",
                    "data": { json: jsonEditor.getText() }
                };
                $.ajax(settings).done(function (response) {
                    $('#recurringJobModal').modal('hide');
                    alert(response);
                    location.reload();
                }).fail(function (response) {
                    alert(response.responseText);
                });
            });


            $('.jsoneditor-menu').hide();
        };

        return RecurringJobExtensions;

    })();

})(window.Hangfire = window.Hangfire || {});

function loadHttpJobModule() {
    new Hangfire.RecurringJobExtensions();
}

if (window.attachEvent) {
    window.attachEvent('onload', loadHttpJobModule);
} else {
    if (window.onload) {
        var curronload = window.onload;
        var newonload = function (evt) {
            curronload(evt);
            loadHttpJobModule(evt);
        };
        window.onload = newonload;
    } else {
        window.onload = loadHttpJobModule;
    }
}