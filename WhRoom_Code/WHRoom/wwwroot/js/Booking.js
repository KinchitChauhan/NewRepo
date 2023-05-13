
$('#submit_Booknow').click(function () {
            var form = $("#BookingForm");
        var url = form.attr("action");
        var formData = form.serialize();
    $.post(url, formData, function (data) {
       
            $("#msg").html(data.Success);  
            });  
        })
        $(function () {
            $('#CarSegmentsId').change(function () {
                var value = $(this).val();
                if (value == '1') {
                    $('#s_hatchback').show();
                    $('#HatchbacksId').show();
                    $('#s_seden').hide();
                    $('#s_SUV').hide();

                    $('#SUVId').hide();
                    $('#SedanId').hide();
                }
                if (value == '2') {
                    $('#s_hatchback').hide();
                    $('#HatchbacksId').hide();
                    $('#s_seden').hide();
                    $('#SedanId').hide();
                    $('#SUVId').show();
                    $('#s_SUV').show();

                }

                if (value == '3') {
                    $('#s_hatchback').hide();
                    $('#HatchbacksId').hide();
                    $('#s_SUV').hide();
                    $('#SUVId').hide();
                    $('#s_seden').show();
                    $('#SedanId').show();


                }
            });
        });
   
