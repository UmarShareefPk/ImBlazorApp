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