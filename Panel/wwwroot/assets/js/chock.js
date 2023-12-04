var chart;
var datasetsVisibility = [true, true, true, true];

function toggleDataset(datasetIndex) {
    chart =  chartDict['chokeChart'];
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

function initialize_chockjs(){


    const pump = document.querySelector('.chart-item.item-1');
    const csco = document.querySelector('.chart-item.item-2');
    const press = document.querySelector('.chart-item.item-3');
    const gain = document.querySelector('.chart-item.item-4');
    pump.addEventListener('click', () => {
        console.log('hello')
        pump.classList.toggle('show')
    })
    csco.addEventListener('click', () => {
        console.log('hello')
        csco.classList.toggle('show')
    })
    press.addEventListener('click', () => {
        console.log('hello')
        press.classList.toggle('show')
    })
    gain.addEventListener('click', () => {
        console.log('hello')
        gain.classList.toggle('show')
    })

    let isDragging = false;
    let initialMouseX;
    let initialRotation = 0;
    let nearestRotation
    const chokeHandle = document.getElementById('chokeHandle');

    chokeHandle.addEventListener('mousedown', (e) => {
        isDragging = true;
        initialMouseX = e.clientX;
        initialRotation = getRotation();
        chokeHandle.style.transition = 'transform 0.2s'; // Disable transition during dragging
    });

    document.addEventListener('mousemove', (e) => {
        if (!isDragging) return;

        const deltaX = e.clientX - initialMouseX;
        const newRotation = initialRotation + deltaX;

        // Ensure the rotation is set to -45deg, 0, or 45deg
        nearestRotation = Math.round(newRotation / 45) * 45;
        if (newRotation <= 45 && newRotation >= -45)
            chokeHandle.style.transform = `translate(-50%,-50%) rotate(${newRotation}deg)`;
    });

    document.addEventListener('mouseup', () => {
        console.log('mouse up')
        if (!isDragging) return;
        console.log('not grabbing')
        isDragging = false;
        if (nearestRotation == 90)
            nearestRotation = 45;
        if (nearestRotation == -90)
            nearestRotation = -45;
        chokeHandle.style.transform = `translate(-50%,-50%) rotate(${nearestRotation}deg)`;
        
        
        // todo for morteza
        let num = nearestRotation < 0 ? -1 : (nearestRotation == 0 ? 0 : 1);
        DotNet.invokeMethodAsync('Panel16', 'SetChockHandlerStatus', num);
        
        chokeHandle.style.transition = 'transform 0.3s ease-in-out';
    });

    function getRotation() {
        const transformValue = window.getComputedStyle(chokeHandle).getPropertyValue('transform');
        const matrix = new DOMMatrix(transformValue);
        return Math.round(Math.atan2(matrix.b, matrix.a) * (180 / Math.PI));
    }


}



let chartDict = {}
let chartCanvasDict = {}

function initChart(identity , type) {
    chartCanvasDict[identity] = document.getElementById(identity).getContext(type);
}


// arr = [0,1,2,3] of [] , labels = []
const updateChokeChart = (identity , arr , labels , contextType = '2d' ) => {

    // console.log("updateChokeChart called from dotnet ");
    
    let ch =  chartDict[identity];
    
    // Check if chart instance exists
    if (!ch) {
        initChart(identity , contextType);
        let canvas = chartCanvasDict[identity];

        const data = {
            labels: labels, // Labels from 0 to 100
            datasets: [
                {
                    label: 'Pump Press.',
                    data: arr[0],
                    borderColor: 'rgb(108, 108, 245)',
                    backgroundColor: 'rgb(108, 108, 245)',
                    borderWidth: 2
                },
                {
                    label: 'CSG Press.',
                    data: arr[1],
                    borderColor: 'rgb(255, 55, 0)',
                    backgroundColor: 'rgb(255, 55, 0)',
                    borderWidth: 2
                },
                {
                    label: 'SPM',
                    data: arr[2],
                    borderColor: 'rgb(85, 232, 87)',
                    backgroundColor: 'rgb(85, 232, 87)',
                    borderWidth: 2
                },
                {
                    label: 'Pit Gain',
                    data: arr[3],
                    borderColor: 'rgb(244, 219, 1)',
                    backgroundColor: 'rgb(244, 219, 1)',
                    borderWidth: 2
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

        console.log(ch.data);
        
        ch.update(); // Update the chart
    }
}



window.InitilaizeChockJs = initialize_chockjs;
window.ToggleBopPanelDataset = toggleDataset;
window.UpdateChokeChart = updateChokeChart;