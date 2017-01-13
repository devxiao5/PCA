$(document).ready(function () {
    $('.ReportTypes').click(function () {
        var currentReportText = $(this).text();
        $('#ReportSelectText').html("" + currentReportText);

        switch (currentReportText) {
            case "Budget Detail":
                $('#ReportOptions').html(`
                    <p>Status<p>
                    <div class ="checkbox">
                      <label class="active"><input type="checkbox" value="" active>Pending</label>
                    </div>
                    <div class ="checkbox">
                      <label class ="active"><input type="checkbox" value="">Reviewed</label>
                    </div>
                    <div class ="checkbox disabled">
                      <label class ="active"><input type="checkbox" value="">Approved</label>
                    </div>
                    
                    `
                    );
                break;
            case "Invoice Detail":
                $('#ReportOptions').html(`
                    <p>Start Date</p>
                    <div class ="dropdown">
                        <button class ="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            <span>Select Date...</span>
                            <span class ="caret"></span>
                        </button>
                        <ul class ="dropdown-menu dropdownLong">
                            <li><a>Budget Detail</a></li>
                            <li><a>Invoice Detail</a></li>
                            <li><a>Budget Detail</a></li>
                            <li><a>Invoice Detail</a></li>
                        </ul>
                    </div>
                    <br />

                    <p>End Date</p>
                    <div class ="dropdown">
                        <button class ="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            <span>Select Date...</span>
                            <span class ="caret"></span>
                        </button>
                        <ul class ="dropdown-menu dropdownLong">
                            <li><a>Budget Detail</a></li>
                            <li><a>Invoice Detail</a></li>
                            <li><a>Budget Detail</a></li>
                            <li><a>Invoice Detail</a></li>
                        </ul>
                    </div>


                    `);
                break;
        }

        
    })


});