function initialize_chockjs(){
    var chart;
    var datasetsVisibility = [true, true, true, true];

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



    function toggleDataset(datasetIndex) {
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

// Function to generate random data
    function generateRandomData() {
        return Array.from({ length: 20 }, () => Math.floor(Math.random() * 100));
    }

// Get the canvas element
    var ctx = document.getElementById('myChart').getContext('2d');

// Create a chart with dark theme
    chart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
            datasets: [
                {
                    label: 'Pump Press.',
                    data: generateRandomData(),
                    borderColor: 'rgb(108, 108, 245)',
                    backgroundColor: 'rgb(108, 108, 245)',
                    borderWidth: 2
                },
                {
                    label: 'CSG Press.',
                    data: generateRandomData(),
                    borderColor: 'rgb(255, 55, 0)',
                    backgroundColor: 'rgb(255, 55, 0)',
                    borderWidth: 2
                },
                {
                    label: 'SPM',
                    data: generateRandomData(),
                    borderColor: 'rgb(85, 232, 87)',
                    backgroundColor: 'rgb(85, 232, 87)',
                    borderWidth: 2
                },
                {
                    label: 'Pit Gain',
                    data: generateRandomData(),
                    borderColor: 'rgb(244, 219, 1)',
                    backgroundColor: 'rgb(244, 219, 1)',
                    borderWidth: 2
                }
            ]
        },
        options: {
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
        }
    });



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
        chokeHandle.style.transition = 'transform 0.3s ease-in-out';
    });

    function getRotation() {
        const transformValue = window.getComputedStyle(chokeHandle).getPropertyValue('transform');
        const matrix = new DOMMatrix(transformValue);
        return Math.round(Math.atan2(matrix.b, matrix.a) * (180 / Math.PI));
    }


}

window.InitilaizeChockJs = initialize_chockjs;