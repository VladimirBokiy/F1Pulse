$(function () {

    var ctx = document.getElementById('race-results-chart').getContext('2d');
    var chart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [],
            datasets: []
        },
        options: {
            plugins: {
                legend: {
                    labels: {
                        hidden: true
                    },
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
                }
            }
        }
    });

    $('#year').on('change', function () {
        var year = $(this).val();
        $.ajax({
            url: 'https://ergast.com/api/f1/' + year + '/driverStandings.json',
            dataType: 'json'
        })
            .then(function(data){
                var rounds = parseInt(data.MRData.StandingsTable.StandingsLists[0].round);
                var drivers = {};
                var promises = [];
                for(var i = 1; i <= rounds; i++){
                    var promise = $.ajax({
                        url: 'https://ergast.com/api/f1/' + year + '/' + i + '/qualifying.json',
                        dataType: 'json'
                    })
                        .then(function(standings){
                            $.each(standings.MRData.RaceTable.Races[0].QualifyingResults, function(k, standing){
                                var driver = standing.Driver.driverId;
                                if (!drivers[driver]) {
                                    drivers[driver] = {
                                        label: driver,
                                        data: [],
                                        fill: false
                                    };
                                }
                                drivers[driver].data.push([parseInt(standings.MRData.RaceTable.round) ,parseInt(standing.position)]);
                            });
                        });
                    promises.push(promise);
                }
                return Promise.all(promises)
                    .then(function(){
                        chart.data.datasets = [];
                        chart.data.labels = [];
                        for(var i = 1; i <= rounds; i++){
                            chart.data.labels.push(i);
                        }
                        $.each(drivers, function (key, value) {
                            value.data = value.data.sort(function(a, b) {
                                return a[0] - b[0];
                            });
                            chart.data.datasets.push({
                                label: value.label,
                                data: value.data,
                                fill: false
                            });
                        });
                        console.log(chart.data.datasets)
                        chart.update();
                    });
            });
    });
});