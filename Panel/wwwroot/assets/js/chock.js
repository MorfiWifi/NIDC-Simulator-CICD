// var chart;
var datasetsVisibility = [true, true, true, true];

function toggleDataset(datasetIndex) {
    chart = chartDict['chokeChart'];
    datasetsVisibility[datasetIndex] = !datasetsVisibility[datasetIndex];

    updateChartVisibility();
}

function updateChartVisibility() {
    if (chart) {
        chart.data.datasets.forEach((dataset, index) => {
            dataset.hidden = !datasetsVisibility[index];
        });
        chart.update();
    }
}
function initialize_chockjs() {

    let isDragging = false;
    let initialX;
    let initialRotation = 0;
    let nearestRotation;
    const chokeHandle = document.getElementById('chokeHandle');

    // Mouse events
    chokeHandle.addEventListener('mousedown', startDrag);
    document.addEventListener('mousemove', drag);
    document.addEventListener('mouseup', endDrag);

    // Touch events
    chokeHandle.addEventListener('touchstart', startDrag);
    document.addEventListener('touchmove', drag);
    document.addEventListener('touchend', endDrag);

    function startDrag(e) {
        isDragging = true;
        initialX = e.clientX || e.touches[0].clientX;
        initialRotation = getRotation();
        chokeHandle.style.transition = 'transform 0.2s'; // Disable transition during dragging
    }

    function drag(e) {
        if (!isDragging) return;

        const currentX = e.clientX || e.touches[0].clientX;
        const deltaX = currentX - initialX;
        const newRotation = initialRotation + deltaX;

        nearestRotation = Math.round(newRotation / 45) * 45;
        if (newRotation <= 45 && newRotation >= -45)
            chokeHandle.style.transform = `translate(-50%,-50%) rotate(${newRotation}deg)`;

        e.preventDefault(); // Prevent scrolling while dragging on touch devices
    }

    function endDrag() {
        if (!isDragging) return;

        isDragging = false;
        if (nearestRotation == 90)
            nearestRotation = 45;
        if (nearestRotation == -90)
            nearestRotation = -45;
        chokeHandle.style.transform = `translate(-50%,-50%) rotate(${nearestRotation}deg)`;

        let num = nearestRotation < 0 ? -1 : (nearestRotation == 0 ? 0 : 1);
        DotNet.invokeMethodAsync('Panel16', 'SetChockHandlerStatus', num);

        chokeHandle.style.transition = 'transform 0.3s ease-in-out';

        // Reset to initial rotation after 1 second
        setTimeout(() => {
            chokeHandle.style.transition = 'transform 1s ease-in-out';
            chokeHandle.style.transform = `translate(-50%,-50%) rotate(0deg)`; // Initial rotation
        }, 1000); // 1000 milliseconds (1 second)
    }

    function getRotation() {
        const transformValue = window.getComputedStyle(chokeHandle).getPropertyValue('transform');
        const matrix = new DOMMatrix(transformValue);
        return Math.round(Math.atan2(matrix.b, matrix.a) * (180 / Math.PI));
    }
}



// let chartDict = {}
// let chartCanvasDict = {}

function initChart(identity, type) {
    chartCanvasDict[identity] = document.getElementById(identity).getContext(type);
}


// arr = [0,1,2,3] of [] , labels = []
const updateChokeChart = (identity, arr, labels, contextType = '2d') => {

    console.log("updateChokeChart called from dotnet ");

    let ch = chartDict[identity];

    // Check if chart instance exists
    if (!ch) {
        initChart(identity, contextType);
        let canvas = chartCanvasDict[identity];

        const data = {
            labels: labels, // Labels from 0 to 100
            datasets: [
                {
                    label: 'Pump Press.',
                    data: arr[0],
                    borderColor: 'rgb(108, 108, 245)',
                    backgroundColor: 'rgb(108, 108, 245)',
                    borderWidth: 2,
                    yAxisID: 'y'
                },
                {
                    label: 'CSG Press.',
                    data: arr[1],
                    borderColor: 'rgb(255, 55, 0)',
                    backgroundColor: 'rgb(255, 55, 0)',
                    borderWidth: 2,
                    yAxisID: 'y'
                },
                {
                    label: 'SPM',
                    data: arr[2],
                    borderColor: 'rgb(85, 232, 87)',
                    backgroundColor: 'rgb(85, 232, 87)',
                    borderWidth: 2,
                    yAxisID: 'y'
                },
                {
                    label: 'Pit Gain',
                    data: arr[3],
                    borderColor: 'rgb(244, 219, 1)',
                    backgroundColor: 'rgb(244, 219, 1)',
                    borderWidth: 2,
                    yAxisID: 'y1'
                }]
        };

        // Define chart options
        const options = {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                x: {
                    grid: {
                        color: 'rgba(255, 255, 255, 0.3)'
                    },
                    ticks: {
                        color: 'rgb(255,255,255,0.5)'
                    }
                },
                y: {
                    position: 'left',
                    grid: {
                        color: 'rgba(255, 255, 255, 0.3)'
                    },
                    ticks: {
                        color: 'rgb(255,255,255,0.5)'
                    }
                },
                y1: {
                    position: 'right',
                    grid: {
                        color: 'rgba(255, 255, 255, 0.3)'
                    },
                    ticks: {
                        color: 'rgb(255,255,255,0.5)'
                    }
                }
            },
            plugins: {
                legend: {
                    display: false
                }
            }
        };

        // Create a new line chart instance
        ch = new Chart(canvas, {
            type: 'line',
            data: data,
            options: options
        });

        chartDict[identity] = ch;
    } else {
        // console.log("updateChokeChart called updating dataset from dotnet ");

        // Update existing chart with new data
        ch.data.datasets[0].data = arr[0];
        ch.data.datasets[1].data = arr[1];
        ch.data.datasets[2].data = arr[2];
        ch.data.datasets[3].data = arr[3];

        ch.data.labels = labels;

        // console.log(ch.data);

        ch.update(); // Update the chart
    }
}


document.addEventListener("DOMContentLoaded", function () {
    var valveElements = document.querySelectorAll('.valve');
    valveElements.forEach(function (valve) {
        valve.addEventListener('click', function () {
            var currentTransform = window.getComputedStyle(valve).getPropertyValue('transform');
            var currentRotation = getRotation(currentTransform);
            var newRotation = currentRotation + 30;
            if (valve.classList.contains('rotate')) {
                newRotation = currentRotation - 30;
                valve.classList.remove('rotate');
            } else {
                valve.classList.add('rotate');
            }
            valve.style.transform = currentTransform + ' rotate(' + newRotation + 'deg)';
        });
    });

    function getRotation(matrix) {
        var values = matrix.split('(')[1].split(')')[0].split(',');
        var a = values[0];
        var b = values[1];
        var angle = Math.round(Math.atan2(b, a) * (180 / Math.PI));
        return angle < 0 ? angle + 360 : angle;
    }
});


window.InitilaizeChockJs = initialize_chockjs;
window.ToggleBopPanelDataset = toggleDataset;
window.UpdateChokeChart = updateChokeChart;