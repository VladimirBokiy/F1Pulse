$(function () {

    var ctx = document.getElementById('lap-times-chart').getContext('2d');
    var chart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [],
            datasets: []
        },
        options: {
            plugins: {
                legend: {
                    position: 'bottom'
                }
            },
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                x:{
                    display: true,
                    title: {
                        display: true,
                        text: 'Lap'
                    }
                },
                y: {
                    type: 'time',
                    time: {
                        parser: 'mm:ss.SSS',
                        unit: "seconds",
                        tooltipFormat: 'mm:ss.SSS',
                        displayFormats: {
                            'seconds': "mm:ss.SSS"
                        },
                    },
                    min: '00:00.000',
                    display: true,
                    title: {
                        display: true,
                        text: 'Time'
                    }
                }
            }
        }
    });

    $('#year').on('change', function () {
        var year = $(this).val();
        $.ajax({
            url: 'https://ergast.com/api/f1/' + year + '.json',
            dataType: 'json',
            success: function (data) {
                var rounds = data.MRData.RaceTable.Races.map(function (race) {
                    return { value: race.round, text: race.raceName }
                });
                $('#round').empty();
                $.each(rounds, function (i, round) {
                    $('#round').append($('<option>', {
                        value: round.value,
                        text: round.text
                    }));
                });
            }
        });
    });

    $('#round').on('change', function () {
        var year = $('#year').val();
        var round = $(this).val();
        var apiUrl = 'https://ergast.com/api/f1/' + year + '/' + round + '/laps.json?limit=10000';
        $.getJSON(apiUrl, function (data) {
            var drivers = {};
            $.each(data.MRData.RaceTable.Races[0].Laps, function (i, lap) {
                $.each(lap.Timings, function (k, timing){
                    var driver = timing.driverId;
                    if (!drivers[driver]) {
                        drivers[driver] = {
                            label: driver,
                            data: [],
                            fill: false
                        };
                    }
                    let timeString = timing.time;
                    drivers[driver].data.push(timeString);
                    if (chart.data.labels.indexOf(lap.number) == -1) {
                        chart.data.labels.push(lap.number);
                    }
                })

            });
            chart.data.datasets = [];
            $.each(drivers, function (i, driver) {
                chart.data.datasets.push(driver);
            });
            console.log(chart.data.datasets)
            chart.update();
        });
    });
});