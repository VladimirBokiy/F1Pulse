$(function () {

    var ctx = document.getElementById('constructor-standings-chart').getContext('2d');
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
                        text: 'Gran Prix'
                    }
                }
            }
        }
    });

    $('#year').on('change', function () {
        var year = $(this).val();
        $.ajax({
            url: 'https://ergast.com/api/f1/' + year + '/constructorStandings.json',
            dataType: 'json'
        })
            .then(function(data){
                var rounds = parseInt(data.MRData.StandingsTable.StandingsLists[0].round);
                var drivers = {};
                var promises = [];
                for(var i = 1; i <= rounds; i++){
                    var promise = $.ajax({
                        url: 'https://ergast.com/api/f1/' + year + '/' + i + '/constructorStandings.json',
                        dataType: 'json'
                    })
                        .then(function(standings){
                            $.each(standings.MRData.StandingsTable.StandingsLists[0].ConstructorStandings, function(k, standing){
                                var driver = standing.Constructor.constructorId;
                                if (!drivers[driver]) {
                                    drivers[driver] = {
                                        label: driver,
                                        data: [],
                                        fill: false
                                    };
                                }
                                drivers[driver].data.push(parseInt(standing.points));
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
                                return a - b;
                            });
                            chart.data.datasets.push({
                                label: value.label,
                                data: value.data,
                                fill: false
                            });
                        });
                        chart.update();
                    });
            });
    });
});