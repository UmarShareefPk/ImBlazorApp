$(document).ready(function () {
    $('.dropdown-toggle').dropdown();
});


window.getMoment = (date) => {
    console.log("hi from script file");
    return moment(date).fromNow();
};

window.drawOverallChart = (data) => {
    console.log(data);
   // var s = document.getElementById("overallChart");
   // console.log(s);

    Highcharts.chart('overallChart', {
        chart: {
            type: 'pie',
            height: (70) + '%',
        },
        title: {
            text: ''
        },
        credits:
        {
            enabled: false
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.y} ({point.percentage:.1f}%)</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: false
                },
                showInLegend: true
            }
        },
        series: [{
            name: 'Share',
            data: [
                //{ name: 'New', y: overallWidgetData.New, color: '#F57C00' },
                {
                    name: 'New', y: data.new, color: {
                        linearGradient: [0, 0, 0, 300],
                        stops: [
                            [0, 'rgba(245,124,0,1)'],
                            [1, 'rgba(255,226,0,1)']
                        ]
                    }
                },
                //{ name: 'In Progress', y: overallWidgetData.InProgress, color: '#1976D2' },
                {
                    name: 'In Progress', y: data.inProgress, color: {
                        linearGradient: [0, 0, 0, 300],
                        stops: [
                            [0, 'rgba(25,118,210,1)'],
                            [1, 'rgba(0,237,255,1)']
                        ]
                    }
                },
                {
                    name: 'Closed', y: data.closed, color: {
                        linearGradient: [0, 0, 0, 300],
                        stops: [
                            [0, 'rgba(67,160,71,1)'],
                            [1, 'rgba(0,255,187,1)']
                        ]
                    }
                },
                // { name: 'Closed', y: overallWidgetData.Closed, color: '#43A047' },
                {
                    name: 'Approved', y: data.approved, color: {
                        linearGradient: [0, 0, 0, 300],
                        stops: [
                            [0, 'rgba(27,94,32,1)'],
                            [1, 'rgba(19,255,0,1)']
                        ]
                    }
                },
                {
                    name: 'Late', y: data.late, color: {
                        linearGradient: [0, 0, 0, 300],
                        stops: [
                            [0, 'rgba(183,28,28,1)'],
                            [1, 'rgba(255,140,0,1)']
                        ]
                    }
                }
                
            ]
        }]
    });

    return "okay";
}

window.drawMostAssignedToUserChart = (data) => {
    console.log(data);
    // var s = document.getElementById("overallChart");
    // console.log(s);

    data = [
        // { name: MostAssignedIncidentsData[0].Name, y: parseInt(MostAssignedIncidentsData[0].Count), color:'#B71C1C' },
        // { name: MostAssignedIncidentsData[1].Name, y: parseInt(MostAssignedIncidentsData[1].Count), color:'#E53935' },
        // { name: MostAssignedIncidentsData[2].Name, y: parseInt(MostAssignedIncidentsData[2].Count), color:'#EF5350' },
        // { name: MostAssignedIncidentsData[3].Name, y: parseInt(MostAssignedIncidentsData[3].Count), color:'#E57373' },
        // { name: MostAssignedIncidentsData[4].Name, y: parseInt(MostAssignedIncidentsData[4].Count), color:'#FFCDD2' }  

        {
            name: data[0].Name, y: parseInt(data[0].Count), color: {
                linearGradient: [0, 0, 0, 300],
                stops: [
                    [0, 'rgba(255,114,0,1)'],
                    [1, 'rgba(183,28,28,1)']
                ]
            }
        },
        {
            name: data[1].Name, y: parseInt(data[1].Count), color: {
                linearGradient: [0, 0, 0, 300],
                stops: [
                    [0, 'rgba(255,127,0,1)'],
                    [1, 'rgba(229,57,53,1)']
                ]
            }
        },
        {
            name: data[2].Name, y: parseInt(data[2].Count), color: {
                linearGradient: [0, 0, 0, 300],
                stops: [
                    [0, 'rgba(255,127,0,1)'],
                    [1, 'rgba(239,83,80,1)']
                ]
            }
        },
        {
            name: data[3].Name, y: parseInt(data[3].Count), color: {
                linearGradient: [0, 0, 0, 300],
                stops: [
                    [0, 'rgba(230,255,46,1)'],
                    [1, 'rgba(160,157,53,1)']
                ]
            }
        },
        {
            name: data[4].Name, y: parseInt(data[4].Count), color: {
                linearGradient: [0, 0, 0, 300],
                stops: [
                    [0, 'rgba(104,255,38,1)'],
                    [1, 'rgba(46,139,44,1)']
                ]
            }
        }
    ];

    Highcharts.chart('mostAssignedtoUsersChart', {
        title: {
            text: 'My chart'
        },
        chart: {
            type: 'bar',
            height: (70) + '%',
            //width: (100) + '%',
        },
        title: {
            text: ''
        },
        credits:
        {
            enabled: false
        },
        legend: {
            enabled: false
        },
        yAxis: {
            title: {
                text: ''
            }
        },

        xAxis: {
            type: 'category',
            min: 0,
            labels: {
                animate: true
            }
        },

        series: [{
            name: '',
            dataSorting: {
                enabled: true,
                sortKey: 'y'
            },
            data: data
        }]
    });

    return "okay";
}